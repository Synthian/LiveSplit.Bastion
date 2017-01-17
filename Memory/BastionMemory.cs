using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace LiveSplit.Bastion.Memory {
	public partial class BastionMemory {
		private ProgramPointer nextMap, player;
		public Process Program { get; set; }
		public bool IsHooked { get; set; } = false;
		private DateTime lastHooked;

		public BastionMemory() {
			nextMap = new ProgramPointer(this, "World.update()") { IsStatic = false };
			player = new ProgramPointer(this, "UnitManager.updateBuffer()") { IsStatic = false };
			lastHooked = DateTime.MinValue;
		}

		public bool AllowInput() {
			return player.Read<bool>(0x0, 0x4, 0xc, 0x308, 0x60);
		}
		public int PlayerUnit() {
			return player.Read<int>(0x0, 0x4, 0xc, 0x308, 0x8);
		}
		public float PlayerX() {
			return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0x8, 0xd8);
		}
		public float PlayerY() {
			return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0x8, 0xdc);
		}
        /*public float targetX()
        {
            return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0xc, 0xd8);
        }
        public float targetY()
        {
            return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0xc, 0xdc);
        }*/

		public string NextMapName() {
			int length = nextMap.Read<int>(0x2c, 0x8);
			if (length < 120 && length > 0) {
				string mapName = nextMap.ReadString(0x2c);
				if (mapName.EndsWith(".map", StringComparison.OrdinalIgnoreCase)) {
					return System.IO.Path.GetFileName(mapName);
				}
			}
			return null;
		}
		public bool HookProcess() {
			if ((Program == null || Program.HasExited) && DateTime.Now > lastHooked.AddSeconds(1)) {
				lastHooked = DateTime.Now;
				Process[] processes = Process.GetProcessesByName("Bastion");
				Program = processes.Length == 0 ? null : processes[0];
				IsHooked = true;
			}

			if (Program == null || Program.HasExited) {
				IsHooked = false;
			}

			return IsHooked;
		}
		public void Dispose() {
			if (Program != null) {
				Program.Dispose();
			}
		}
	}
	public class ProgramPointer {
		private static string[] versions = new string[1] { "v1.0" };
		private static Dictionary<string, Dictionary<string, string>> funcPatterns = new Dictionary<string, Dictionary<string, string>>() {
			{"v1.0", new Dictionary<string, string>() {
					{"World.update()",             "4DB8FF15????????8B15????????39028D7DBC|-9" },
					{"UnitManager.updateBuffer()", "85C0743D8B3D????????8B470C8B57043B4204750B8B570C428BCFE8????????8B4F048B5F0C8D430189470C568BD3E8????????FF47108BCE|-51" }
			}},
		};
		private IntPtr pointer;
		public BastionMemory Memory { get; set; }
		public string Name { get; set; }
		public bool IsStatic { get; set; }
		private int lastID;
		private DateTime lastTry;
		public ProgramPointer(BastionMemory memory, string name) {
			this.Memory = memory;
			this.Name = name;
			this.IsStatic = true;
			lastID = memory.Program == null ? -1 : memory.Program.Id;
			lastTry = DateTime.MinValue;
		}

		public IntPtr Value {
			get {
				if (!Memory.IsHooked) {
					pointer = IntPtr.Zero;
				} else {
					GetPointer(ref pointer, Name);
				}
				return pointer;
			}
		}
		public T Read<T>(params int[] offsets) {
			if (!Memory.IsHooked) { return default(T); }
			return Memory.Program.Read<T>(Value, offsets);
		}
		public string ReadString(params int[] offsets) {
			if (!Memory.IsHooked) { return string.Empty; }
			IntPtr p = Memory.Program.Read<IntPtr>(Value, offsets);
			return Memory.Program.GetString(p);
		}
		public void Write<T>(T value, params int[] offsets) {
			if (!Memory.IsHooked) { return; }
			Memory.Program.Write<T>(Value, value, offsets);
		}
		private void GetPointer(ref IntPtr ptr, string name) {
			if (Memory.IsHooked) {
				if (Memory.Program.Id != lastID) {
					ptr = IntPtr.Zero;
					lastID = Memory.Program.Id;
				}
				if (ptr == IntPtr.Zero && DateTime.Now > lastTry.AddSeconds(1)) {
					lastTry = DateTime.Now;
					ptr = GetVersionedFunctionPointer(name);
					if (ptr != IntPtr.Zero) {
						if (IsStatic) {
							ptr = Memory.Program.Read<IntPtr>(ptr, 0, 0);
						} else {
							ptr = Memory.Program.Read<IntPtr>(ptr, 0);
						}
					}
				}
			}
		}
		public IntPtr GetVersionedFunctionPointer(string name) {
			foreach (string version in versions) {
				if (funcPatterns[version].ContainsKey(name)) {
					return Memory.Program.FindSignatures(funcPatterns[version][name])[0];
				}
			}
			return IntPtr.Zero;
		}
	}
	public enum FlagSet {
		NULL,
		MAPS_COMPLETE,
		MAPS_UNLOCKED,
		MAPS_VIEWED,
		MAPS_SAVED,
		THINGS_DEAD,
		CONVERSATIONS_COMPLETE,
		SEEDS_FOUND,
		SCRIPTS_FIRED,
		SCRIPT_FLAGS,
		LOCAL_SCRIPTS,
		GLOBAL_SCRIPTS,
		WEAPONS_UNLOCKED,
		WEAPONS_VIEWED,
		WEAPONS_FIRED,
		FLAGS,
		AUDIO_CUES_PLAYED,
		UPGRADES_UNLOCKED,
		UPGRADES_VIEWED,
		STORE_ITEMS_VIEWED,
		QUESTS_UNLOCKED,
		QUESTS_COMPLETE,
		QUESTS_TURNED_IN,
		CHALLENGE_FIRST_PRIZE,
		CHALLENGE_SECOND_PRIZE,
		CHALLENGE_THIRD_PRIZE,
		HINT_TEXT
	}
}