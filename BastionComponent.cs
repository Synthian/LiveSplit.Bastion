using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Numerics;
using LiveSplit.Bastion.Memory;
using LiveSplit.Bastion.Settings;
namespace LiveSplit.Bastion {
	public class BastionComponent : IComponent {
		public string ComponentName { get { return "Bastion Autosplitter"; } }
		public TimerModel Model { get; set; }
		public IDictionary<string, Action> ContextMenuControls { get { return null; } }
		internal static string[] keys = { "CurrentSplit", "AllowInput", "NextMap", "PlayerUnit", "posX", "posY" };
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
            double playerX = (double)mem.PlayerX();
            double playerY = (double)mem.PlayerY();

            if (currentSplit == 0 && settings.Start)
            {
                shouldSplit = oldMap == "ProtoIntro01.map" && !oldAllowInput && allowInput;
            }
            else if (Model.CurrentState.CurrentPhase == TimerPhase.Running)
            {
                if (settings.End && oldMap == "End01.map" && oldAllowInput && !allowInput)
                {
                    shouldSplit = true;
                }
                if (settings.Split)
                {
                    if (settings.Ram && oldAllowInput && !allowInput && nextMap == "FinalRam01.map" && inRange(3747, 2541, playerX, playerY))
                    {
                        shouldSplit = true;
                    }
                    else if (settings.Tazal && nextMap == "FinalRam01.map" && oldMap != "FinalRam01.map")
                    {
                        shouldSplit = true;
                    }
                    else if (settings.SoleRegret && nextMap == "ProtoIntro01b.map" && oldMap != "ProtoIntro01b.map")
                    {
                        shouldSplit = true;
                    }
                    else if (settings.Classic)
                    {
                        if (oldAllowInput && !allowInput)
                        {
                            switch (nextMap)
                            {
                                case "ProtoIntro01b.map":
                                    if (playerX > 17070 && playerY < 7930)
                                        shouldSplit = true;
                                    break;
                                case "Crossroads01.map":
                                    if (inRange(9540, 4898, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Holdout01.map":
                                    if (playerX > 5650 && playerY > 3380)
                                        shouldSplit = true;
                                    break;
                                case "Falling01.map":
                                    if (inRange(7658, 10508, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Survivor01.map":
                                    if (inRange(15277, 7691, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Siege01.map":
                                    if (inRange(14918, 6605, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Shrine01.map":
                                    if (inRange(4386, 6999, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Moving01.map":
                                    if (inRange(34843, 2992, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Survivor02.map":
                                    if (inRange(4378, 5291, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Crossroads02.map":
                                    if (inRange(10593, 10677, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Scenes02.map":
                                    if (inRange(4465, 5397, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Hunt01.map":
                                    if (inRange(10177, 11452, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Platforms01.map":
                                    if (inRange(10167, 9883, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Scorched01.map":
                                    if (inRange(423, 2633, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Fortress01.map":
                                    if (inRange(7915, 1759, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Gauntlet01.map":
                                    if (inRange(14331, 6965, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Voyage01.map":
                                    if (inRange(19339, 5754, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                case "Rescue01.map":
                                    if (inRange(5154, 3755, playerX, playerY))
                                        shouldSplit = true;
                                    break;
                                //For the end of Tazal
                                case "ProtoTown03.map":
                                    if (oldMap == "FinalZulf01.map")
                                        shouldSplit = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (nextMap == "ProtoTown03.map" && oldMap != "ProtoTown03.map")
                        {
                            if (oldMap != "Attack01.map" && oldMap != "")
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
                        else if (nextMap == "Attack01.map" && oldMap != "Attack01.map")
                        {
                            shouldSplit = true;
                        }
                    }
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

        private bool inRange(double ThingX, double ThingY, double posX, double posY)
        {
            double dist = Math.Sqrt(Math.Pow((ThingX - posX), 2) + Math.Pow((ThingY - posY), 2));
            if (dist < 180)
                return true;
            return false;
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
                        case "posX": curr = mem.PlayerX().ToString(); break;
                        case "posY": curr = mem.PlayerY().ToString(); break;
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