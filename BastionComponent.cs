// Main logic component of the splitter

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
        private const int SPLIT_DIST = 180;

        public string ComponentName { get { return "Bastion Autosplitter"; } }
		public TimerModel Model { get; set; }
		public IDictionary<string, Action> ContextMenuControls { get { return null; } }
		private BastionMemory mem;
        private BastionSettings settings;

        // Used for debugging
        internal static string[] keys = { "CurrentSplit", "AllowInput", "MapPointer", "NextMap", "PlayerUnit", "TargetX", "TargetY" };
        private Dictionary<string, string> currentValues = new Dictionary<string, string>();

        private int currentSplit = 0, lastLogCheck = 0;
		private bool hasLog = false, oldAllowInput = false;
        private string oldMap = "";
		private bool noModel = true;
		
        // Initialize memory and settings
		public BastionComponent() {
			mem = new BastionMemory();
			settings = new BastionSettings(this);
			foreach (string key in keys) {
				currentValues[key] = "";
			}
		}

        // Main "driver" function internally
        // When this is called, the "state machine" will pull new values and evaulate them, if possible
        // Data is also logged.
		public void GetValues() {
			if (!mem.HookProcess()) { return; }

			if (Model != null) {
				HandleBastion();
			}

			LogValues();
		}

        private void HandleBastion() {
            // Update transition variables

            bool shouldSplit = false;
            bool allowInput = mem.AllowInput();
            // The nextMap variable defaults to the previous map if there is no valid map
            // Therefore the variable shows you what the user is CURRENTLY loading or LAST LOADED (if not currently loading)
            string nextMap = mem.NextMapName() ?? oldMap;
            double playerX = mem.PlayerX();
            double playerY = mem.PlayerY();

            if (settings.IL)
            {
                if (currentSplit == 0) {
                    // Check IL level start transitions
                    shouldSplit = ilStarted(nextMap, allowInput);
                }
                else if (Model.CurrentState.CurrentPhase == TimerPhase.Running) {
                    // Check general level end transitions
                    shouldSplit = generalLevelEnded(nextMap, playerX, playerY, allowInput);
                }

                // Handle split decision
                // false because we never reset in IL mode
                HandleSplit(shouldSplit, false);
            }
            else
            {
                if (currentSplit == 0 && settings.Start) {
                    // Standard run start condition
                    // Split if (in Rippling Walls) AND (transitioning from no-control to control)
                    shouldSplit = ((nextMap == "ProtoIntro01.map") && !oldAllowInput && allowInput);
                }
                else if (Model.CurrentState.CurrentPhase == TimerPhase.Running) {
                    // End of game
                    // Split if (Settings allow) AND (in Heart of the Bastion) AND (transitioning from control to no-control) AND (in range of Monument)
                    if (settings.Split && (nextMap == "End01.map") && oldAllowInput && !allowInput && inRange(2404, 2366, playerX, playerY)) {
                        shouldSplit = true;
                    }
                    else if (settings.Split) {
                        // Ram Pickup Split
                        // Split if (Settings allow) AND (transitioning from control to no-control) AND (we're on the ram map) AND (in range of Ram)
                        if (settings.Ram && oldAllowInput && !allowInput && (nextMap == "FinalRam01.map") && inRange(3747, 2541, playerX, playerY)) {
                            shouldSplit = true;
                        }
                        // Tazal I Split
                        // Split if (Settings allow) AND (starting to load FinalRam01)
                        else if (settings.Tazal && (nextMap == "FinalRam01.map") && (oldMap != "FinalRam01.map")) {
                            shouldSplit = true;
                        }
                        // Sole Regret Split
                        // Split if (Settings allow) AND (starting to load Wharf District)
                        else if (settings.SoleRegret && nextMap == "ProtoIntro01b.map" && oldMap != "ProtoIntro01b.map") {
                            shouldSplit = true;
                        }
                        // General level end conditions
                        else {
                            shouldSplit = generalLevelEnded(nextMap, playerX, playerY, allowInput);
                        }
                    }
                }

                // Handle split decision
                // Send reset if (loading or in Rippling Walls) AND (Player model is not found)
                HandleSplit(shouldSplit, (nextMap == "ProtoIntro01.map") && noModel);
            }

            // Update variables for next cycle
            // If our PlayerUnit is gone, we are either loading or in the main menu
			if (mem.PlayerUnit() == 0) {
				noModel = true;
			} else {
				noModel = false;
			}
            // Save last cycle's transition variables
			oldMap = nextMap;
			oldAllowInput = allowInput;
		}

        private bool ilStarted(string nextMap, bool allowInput) {
            if ((oldMap == "ProtoIntro01.map") && !oldAllowInput && allowInput) {
                return true;
            }
            else if (oldMap == "ProtoTown03.map") {
                switch (nextMap)
                {
                    case "Crossroads01.map":
                    case "Holdout01.map":
                    case "Falling01.map":
                    case "Survivor01.map":
                    case "Siege01.map":
                    case "Shrine01.map":
                    case "Moving01.map":
                    case "Survivor02.map":
                    case "Crossroads02.map":
                    case "Scenes02.map":
                    case "Hunt01.map":
                    case "Platforms01.map":
                    case "Scorched01.map":
                    case "Fortress01.map":
                    case "Gauntlet01.map":
                    case "Voyage01.map":
                    case "Rescue01.map":
                    case "FinalArena01.map":
                    case "Onslaught01.map":
                    case "Onslaught02.map":
                    case "Onslaught03.map":
                    case "Onslaught04.map":
                    default:
                        return false;
                }
            } else return false;
        }
            
        private bool generalLevelEnded(string nextMap, double playerX, double playerY, bool allowInput) {
            // Split if we lose control at a certain location in particular levels
            if (oldAllowInput && !allowInput) {
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
            // We also need to split upon loading the Bastion when we leave a dream or end Tazal (because we don't lose control)
            if ((nextMap == "ProtoTown03.map") && ((oldMap == "FinalZulf01.map") || (oldMap.Contains("Onslaught"))))
                return true;

            return false;
        }

        // Test if Thing is within SPLIT_DIST of pos
		private bool inRange(double ThingX, double ThingY, double posX, double posY) {
			double dist = Math.Sqrt(Math.Pow((ThingX - posX), 2) + Math.Pow((ThingY - posY), 2));
			if (dist < SPLIT_DIST)
				return true;
			return false;
		}

        // Communicate split decisions with LiveSplit
		private void HandleSplit(bool shouldSplit, bool shouldReset) {
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

        // Logging of values
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

        // Called by LiveSplit
		public void Update(IInvalidator invalidator, LiveSplitState lvstate, float width, float height, LayoutMode mode) {
			if (Model == null) {
				Model = new TimerModel() { CurrentState = lvstate };
				Model.InitializeGameTime();
				Model.CurrentState.IsGameTimePaused = true;
                // Set/add event handlers
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

        // Called by LiveSplit
		public void OnReset(object sender, TimerPhase e) {
			currentSplit = 0;
			oldMap = "";
			oldAllowInput = true;
			Model.CurrentState.IsGameTimePaused = true;
			WriteLog("---------Reset----------------------------------");
		}

        // Called by LiveSplit
		public void OnResume(object sender, EventArgs e) {
			WriteLog("---------Resumed--------------------------------");
		}

        // Called by LiveSplit
		public void OnPause(object sender, EventArgs e) {
			WriteLog("---------Paused---------------------------------");
		}

        // Called by LiveSplit
        public void OnStart(object sender, EventArgs e) {
			currentSplit++;
			Model.CurrentState.IsGameTimePaused = true;
			WriteLog("---------New Game-------------------------------");
		}

        // Called by LiveSplit
        public void OnUndoSplit(object sender, EventArgs e) {
			currentSplit--;
			WriteLog(DateTime.Now.ToString(@"HH\:mm\:ss.fff") + " | " + Model.CurrentState.CurrentTime.RealTime.Value.ToString("G").Substring(3, 11) + ": CurrentSplit: " + currentSplit.ToString().PadLeft(24, ' '));
		}

        // Called by LiveSplit
        public void OnSkipSplit(object sender, EventArgs e) {
			currentSplit++;
			WriteLog(DateTime.Now.ToString(@"HH\:mm\:ss.fff") + " | " + Model.CurrentState.CurrentTime.RealTime.Value.ToString("G").Substring(3, 11) + ": CurrentSplit: " + currentSplit.ToString().PadLeft(24, ' '));
		}

        // Called by LiveSplit
        public void OnSplit(object sender, EventArgs e) {
			currentSplit++;
			Model.CurrentState.IsGameTimePaused = true;
			WriteLog(DateTime.Now.ToString(@"HH\:mm\:ss.fff") + " | " + Model.CurrentState.CurrentTime.RealTime.Value.ToString("G").Substring(3, 11) + ": CurrentSplit: " + currentSplit.ToString().PadLeft(24, ' '));
		}

        // Writes data to log or to the console if applicable
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

		public Control GetSettingsControl(LayoutMode mode) {
            return settings;
        }

		public void SetSettings(XmlNode document) {
            settings.SetSettings(document);
        }

		public XmlNode GetSettings(XmlDocument document) {
            return settings.UpdateSettings(document);
        }
        
        // We take up no space visually, so we return nothing/zero for visual calls from LiveSplit
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