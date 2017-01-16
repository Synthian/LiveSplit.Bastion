using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Bastion.Memory;
using LiveSplit.Bastion.Settings;
namespace LiveSplit.Bastion {
	public class BastionComponent : IComponent {
		public string ComponentName { get { return "Bastion Autosplitter"; } }
		public TimerModel Model { get; set; }
		public IDictionary<string, Action> ContextMenuControls { get { return null; } }
		internal static string[] keys = { "CurrentSplit", "AllowInput", "NextMap", "PlayerUnit" };
		private BastionMemory mem;
		private int currentSplit = 0, lastLogCheck = 0;
		private bool hasLog = false, oldAllowInput = false;
		private string oldMap = "ProtoIntro01.map";
        private bool noModel = true;
		private Dictionary<string, string> currentValues = new Dictionary<string, string>();
		private BastionSettings settings;
		public BastionComponent() {
			mem = new BastionMemory();
			settings = new BastionSettings(this);
			foreach (string key in keys) {
				currentValues[key] = "";
			}
		}

		public void GetValues() {
			if (!mem.HookProcess()) { return; }

			if (Model != null) {
				HandleBastion();
			}

			LogValues();
		}
        private void HandleBastion()
        {
            bool shouldSplit = false;
            bool allowInput = mem.AllowInput();
            string nextMap = mem.NextMapName() ?? oldMap;

            if (currentSplit == 0 && settings.SplitStart)
            {
                shouldSplit = oldMap == "ProtoIntro01.map" && !oldAllowInput && allowInput;
            }
            else if (Model.CurrentState.CurrentPhase == TimerPhase.Running)
            {
                if (settings.SplitEnd && oldMap == "End01.map" && oldAllowInput && !allowInput)
                {
                    shouldSplit = true;
                }
                else if (nextMap == "ProtoIntro01.map" && oldMap != "ProtoIntro01.map")
                {
                    shouldSplit = true;
                }
                else if (nextMap == "ProtoIntro01b.map" && oldMap != "ProtoIntro01b.map" && settings.RockInSky)
                {
                    if (settings.RockInSky)
                    {
                        shouldSplit = true;
                    }
                }
                else if (nextMap == "ProtoTown03.map" && oldMap != "ProtoTown03.map")
                {
                    if (oldMap != "Attack01.map" && oldMap != "" && settings.Town)
                    {
                        shouldSplit = true;
                    }
                }
                else if (nextMap == "Survivor02.map" && oldMap != "Survivor02.map")
                {
                    if (oldMap != "ProtoTown03.map")
                    {
                        shouldSplit = true;
                    }
                }
                else if (nextMap == "FinalRam01.map" && oldMap != "FinalRam01.map")
                {
                    if (settings.Tazal)
                    {
                        shouldSplit = true;
                    }
                }
                else if (nextMap == "Attack01.map" && oldMap != "Attack01.map")
                {
                    shouldSplit = true;
                }

            }

			HandleSplit(shouldSplit, nextMap == "ProtoIntro01.map" && noModel);

            if (mem.PlayerUnit() == 0) {
                noModel = true;
            }
            else {
                noModel = false;
            }
            oldMap = nextMap;
			oldAllowInput = allowInput;
		}
		private void HandleSplit(bool shouldSplit, bool shouldReset = false) {
			if (currentSplit > 0 && shouldReset && settings.Reset) {
				Model.Reset();
			} else if (shouldSplit) {
				if (currentSplit == 0) {
					Model.Start();
				} else {
					Model.Split();
				}
			}
		}
		private void LogValues() {
			if (lastLogCheck == 0) {
				hasLog = File.Exists("_Bastion.log");
				lastLogCheck = 300;
			}
			lastLogCheck--;

			if (hasLog || !Console.IsOutputRedirected) {
				string prev = "", curr = "";
				foreach (string key in keys) {
					prev = currentValues[key];

					switch (key) {
						case "CurrentSplit": curr = currentSplit.ToString(); break;
						case "AllowInput": curr = mem.AllowInput().ToString(); break;
						case "NextMap": curr = mem.NextMapName() ?? ""; break;
						case "PlayerUnit": curr = mem.PlayerUnit().ToString(); break;
						default: curr = ""; break;
					}

					if (!prev.Equals(curr)) {
						WriteLog(DateTime.Now.ToString(@"HH\:mm\:ss.fff") + (Model != null ? " | " + Model.CurrentState.CurrentTime.RealTime.Value.ToString("G").Substring(3, 11) : "") + ": " + key + ": ".PadRight(16 - key.Length, ' ') + prev.PadLeft(25, ' ') + " -> " + curr);

						currentValues[key] = curr;
					}
				}
			}
		}

		public void Update(IInvalidator invalidator, LiveSplitState lvstate, float width, float height, LayoutMode mode) {
			if (Model == null) {
				Model = new TimerModel() { CurrentState = lvstate };
				Model.InitializeGameTime();
				Model.CurrentState.IsGameTimePaused = true;
				lvstate.OnReset += OnReset;
				lvstate.OnPause += OnPause;
				lvstate.OnResume += OnResume;
				lvstate.OnStart += OnStart;
				lvstate.OnSplit += OnSplit;
				lvstate.OnUndoSplit += OnUndoSplit;
				lvstate.OnSkipSplit += OnSkipSplit;
			}

			GetValues();
		}

		public void OnReset(object sender, TimerPhase e) {
			currentSplit = 0;
			oldMap = "";
			oldAllowInput = true;
			Model.CurrentState.IsGameTimePaused = true;
			WriteLog("---------Reset----------------------------------");
		}
		public void OnResume(object sender, EventArgs e) {
			WriteLog("---------Resumed--------------------------------");
		}
		public void OnPause(object sender, EventArgs e) {
			WriteLog("---------Paused---------------------------------");
		}
		public void OnStart(object sender, EventArgs e) {
			currentSplit++;
			Model.CurrentState.IsGameTimePaused = true;
			WriteLog("---------New Game-------------------------------");
		}
		public void OnUndoSplit(object sender, EventArgs e) {
			currentSplit--;
			WriteLog(DateTime.Now.ToString(@"HH\:mm\:ss.fff") + " | " + Model.CurrentState.CurrentTime.RealTime.Value.ToString("G").Substring(3, 11) + ": CurrentSplit: " + currentSplit.ToString().PadLeft(24, ' '));
		}
		public void OnSkipSplit(object sender, EventArgs e) {
			currentSplit++;
			WriteLog(DateTime.Now.ToString(@"HH\:mm\:ss.fff") + " | " + Model.CurrentState.CurrentTime.RealTime.Value.ToString("G").Substring(3, 11) + ": CurrentSplit: " + currentSplit.ToString().PadLeft(24, ' '));
		}
		public void OnSplit(object sender, EventArgs e) {
			currentSplit++;
			Model.CurrentState.IsGameTimePaused = true;
			WriteLog(DateTime.Now.ToString(@"HH\:mm\:ss.fff") + " | " + Model.CurrentState.CurrentTime.RealTime.Value.ToString("G").Substring(3, 11) + ": CurrentSplit: " + currentSplit.ToString().PadLeft(24, ' '));
		}
		private void WriteLog(string data) {
			if (hasLog || !Console.IsOutputRedirected) {
				if (Console.IsOutputRedirected) {
					using (StreamWriter wr = new StreamWriter("_Bastion.log", true)) {
						wr.WriteLine(data);
					}
				} else {
					Console.WriteLine(data);
				}
			}
		}

		public Control GetSettingsControl(LayoutMode mode) { return settings; }
		public void SetSettings(XmlNode document) { settings.SetSettings(document); }
		public XmlNode GetSettings(XmlDocument document) { return settings.UpdateSettings(document); }
		public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion) { }
		public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion) { }
		public float HorizontalWidth { get { return 0; } }
		public float MinimumHeight { get { return 0; } }
		public float MinimumWidth { get { return 0; } }
		public float PaddingBottom { get { return 0; } }
		public float PaddingLeft { get { return 0; } }
		public float PaddingRight { get { return 0; } }
		public float PaddingTop { get { return 0; } }
		public float VerticalHeight { get { return 0; } }
		public void Dispose() { }
	}
}