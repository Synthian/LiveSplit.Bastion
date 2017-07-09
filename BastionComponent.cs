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
		internal static string[] keys = { "CurrentSplit", "AllowInput", "MapPointer", "NextMap", "PlayerUnit", "TargetX", "TargetY" };
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
        private void HandleBastion() {
            //update variables
            bool shouldSplit = false;
            bool allowInput = mem.AllowInput();
            string nextMap = mem.NextMapName() ?? oldMap;
            double playerX = (double) mem.PlayerX();
            double playerY = (double) mem.PlayerY();

            if (settings.IL)
            {
                if (currentSplit == 0)
                {
                    //check start conditions
                    shouldSplit = ilStarted(nextMap, allowInput);

                }
                else if (Model.CurrentState.CurrentPhase == TimerPhase.Running)
                {
                    //check general level end conditions
                    shouldSplit = generalLevelEnded(nextMap, playerX, playerY, allowInput);
                }

                //handle split
                HandleSplit(shouldSplit, false);
            }
            else
            {
                if (currentSplit == 0 && settings.Start)
                {
                    //check start conditions
                    shouldSplit = oldMap == "ProtoIntro01.map" && !oldAllowInput && allowInput;
                }
                else if (Model.CurrentState.CurrentPhase == TimerPhase.Running)
                {
                    //check End of Game
                    if (settings.End && nextMap == "End01.map" && oldAllowInput && !allowInput && inRange(2404, 2366, playerX, playerY))
                    {
                        shouldSplit = true;
                    }
                    else if (settings.Split)
                    {
                        //check run specific split conditions
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
                        //check general level end conditions
                        else
                        {
                            shouldSplit = generalLevelEnded(nextMap, playerX, playerY, allowInput);
                        }
                    }
                }

                //handle split
                HandleSplit(shouldSplit, nextMap == "ProtoIntro01.map" && noModel);

            }

            //update variables
			if (mem.PlayerUnit() == 0) {
				noModel = true;
			} else {
				noModel = false;
			}
			oldMap = nextMap;
			oldAllowInput = allowInput;
		}

        private bool ilStarted(string nextMap, bool allowInput)
        {
            if (oldMap == "ProtoIntro01.map" && !oldAllowInput && allowInput)
            {
                return true;
            }
            else if (oldMap == "ProtoTown03.map")
            {
                switch (nextMap)
                {
                    case "Crossroads01.map":
                        return true;
                    case "Holdout01.map":
                        return true;
                    case "Falling01.map":
                        return true;
                    case "Survivor01.map":
                        return true;
                    case "Siege01.map":
                        return true;
                    case "Shrine01.map":
                        return true;
                    case "Moving01.map":
                        return true;
                    case "Survivor02.map":
                        return true;
                    case "Crossroads02.map":
                        return true;
                    case "Scenes02.map":
                        return true;
                    case "Hunt01.map":
                        return true;
                    case "Platforms01.map":
                        return true;
                    case "Scorched01.map":
                        return true;
                    case "Fortress01.map":
                        return true;
                    case "Gauntlet01.map":
                        return true;
                    case "Voyage01.map":
                        return true;
                    case "Rescue01.map":
                        return true;
                    case "FinalArena01.map":
                        return true;
                    case "Onslaught01.map":
                        return true;
                    case "Onslaught02.map":
                        return true;
                    case "Onslaught03.map":
                        return true;
                    case "Onslaught04.map":
                        return true;
                    default:
                        return false;
                }
            } else return false;
        }
            

        private bool generalLevelEnded(string nextMap, double playerX, double playerY, bool allowInput)
        {
            if (oldAllowInput && !allowInput)
            {
                switch (nextMap)
                {
                    case "ProtoIntro01b.map":
                        if (playerX > 17070 && playerY < 7930)
                            return true;
                        break;
                    case "Crossroads01.map":
                        if (inRange(9540, 4898, playerX, playerY))
                            return true;
                        break;
                    case "Holdout01.map":
                        if (playerX > 5650 && playerY > 3380)
                            return true;
                        break;
                    case "Falling01.map":
                        if (inRange(7658, 10508, playerX, playerY))
                            return true;
                        break;
                    case "Survivor01.map":
                        if (inRange(15277, 7691, playerX, playerY))
                            return true;
                        break;
                    case "Siege01.map":
                        if (inRange(14918, 6605, playerX, playerY))
                            return true;
                        break;
                    case "Shrine01.map":
                        if (inRange(4386, 6999, playerX, playerY))
                            return true;
                        break;
                    case "Moving01.map":
                        if (inRange(34843, 2992, playerX, playerY))
                            return true;
                        break;
                    case "Survivor02.map":
                        if (inRange(4378, 5291, playerX, playerY))
                            return true;
                        break;
                    case "Crossroads02.map":
                        if (inRange(10593, 10677, playerX, playerY))
                            return true;
                        break;
                    case "Scenes02.map":
                        if (inRange(4465, 5397, playerX, playerY))
                            return true;
                        break;
                    case "Hunt01.map":
                        if (inRange(10177, 11452, playerX, playerY))
                            return true;
                        break;
                    case "Platforms01.map":
                        if (inRange(10167, 9883, playerX, playerY))
                            return true;
                        break;
                    case "Scorched01.map":
                        if (inRange(423, 2633, playerX, playerY))
                            return true;
                        break;
                    case "Fortress01.map":
                        if (inRange(7915, 1759, playerX, playerY))
                            return true;
                        break;
                    case "Gauntlet01.map":
                        if (inRange(14331, 6965, playerX, playerY))
                            return true;
                        break;
                    case "Voyage01.map":
                        if (inRange(19339, 5754, playerX, playerY))
                            return true;
                        break;
                    case "Rescue01.map":
                        if (inRange(5154, 3755, playerX, playerY))
                            return true;
                        break;
                    case "Challenge01.map":
                        if (inRange(5021, 6801, playerX, playerY))
                            return true;
                        break;
                    case "Challenge02.map":
                        if (inRange(5021, 6799, playerX, playerY))
                            return true;
                        break;
                    case "Challenge03.map":
                        if (inRange(8731, 2403, playerX, playerY))
                            return true;
                        break;
                    case "Challenge04.map":
                        if (inRange(6034, 5846, playerX, playerY))
                            return true;
                        break;
                    case "Challenge05.map":
                        if (inRange(5474, 6895, playerX, playerY))
                            return true;
                        break;
                    case "Challenge06.map":
                        if (inRange(5165, 4396, playerX, playerY))
                            return true;
                        break;
                    case "Challenge07.map":
                        if (inRange(5573, 5946, playerX, playerY))
                            return true;
                        break;
                    case "Challenge08.map":
                        if (inRange(5500, 6112, playerX, playerY))
                            return true;
                        break;
                    case "Challenge09.map":
                        if (inRange(5569, 3626, playerX, playerY))
                            return true;
                        break;
                    case "Challenge10.map":
                        if (inRange(5586, 6422, playerX, playerY))
                            return true;
                        break;
                    case "Challenge11.map":
                        if (inRange(5101, 5780, playerX, playerY))
                            return true;
                        break;
                    case "Challenge12.map":
                        if (inRange(3565, 5570, playerX, playerY))
                            return true;
                        break;
                }
            }
            if (nextMap == "ProtoTown03.map" && (oldMap == "FinalZulf01.map" || oldMap.Contains("Onslaught")))
                return true;

            return false;
        }

		private bool inRange(double ThingX, double ThingY, double posX, double posY) {
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
						case "MapPointer": curr = mem.MapPointer().ToString(); break;
						case "NextMap": curr = mem.NextMapName() ?? ""; break;
						case "PlayerUnit": curr = mem.PlayerUnit().ToString(); break;
						case "PosX": curr = mem.PlayerX().ToString("0"); break;
						case "PosY": curr = mem.PlayerY().ToString("0"); break;
                        case "TargetX": curr = mem.targetX().ToString("0"); break;
                        case "TargetY": curr = mem.targetY().ToString("0"); break;
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