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
			if (player.Version == "v1.1") {
				return player.Read<bool>(0x0, 0x4, 0x8, 0x308, 0x60);
			} else {
				return player.Read<bool>(0x0, 0x4, 0xc, 0x308, 0x60);
			}
		}
		public int PlayerUnit() {
			if (player.Version == "v1.1") {
				return player.Read<int>(0x0, 0x4, 0x8, 0x308, 0x8);
			} else {
				return player.Read<int>(0x0, 0x4, 0xc, 0x308, 0x8);
			}
		}
		public float PlayerX() {
			if (player.Version == "v1.1") {
				return player.Read<float>(0x0, 0x4, 0x8, 0x308, 0x8, 0xd8);
			} else {
				return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0x8, 0xd8);
			}
		}
		public float PlayerY() {
			if (player.Version == "v1.1") {
				return player.Read<float>(0x0, 0x4, 0x8, 0x308, 0x8, 0xdc);
			} else {
				return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0x8, 0xdc);
			}
		}
		/*public float targetX()
        {
            return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0xc, 0xd8);
        }
        public float targetY()
        {
            return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0xc, 0xdc);
        }*/
		public int MapPointer() {
			return nextMap.Value.ToInt32();
		}
		public string NextMapName() {
			int length = 0;
			if (nextMap.Version == "v1.1") {
				length = nextMap.Read<int>(0x2c, 0x4);
			}
            else {
				length = nextMap.Read<int>(0x2c, 0x8);
			}
			if (length < 120 && length > 0) {
				string mapName = "";
				if (nextMap.Version == "v1.1") {
					mapName = nextMap.ReadString2(0x2c);
				}
                else {
					mapName = nextMap.ReadString(0x2c);
				}

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
		private static string[] versions = new string[2] { "v1.0", "v1.1" };
		private static Dictionary<string, Dictionary<string, string>> funcPatterns = new Dictionary<string, Dictionary<string, string>>() {
			{"v1.0", new Dictionary<string, string>() {
					{"World.update()",             "4DB8FF15????????8B15????????39028D7DBC|-9" },
					{"UnitManager.updateBuffer()", "85C0743D8B3D????????8B470C8B57043B4204750B8B570C428BCFE8????????8B4F048B5F0C8D430189470C568BD3E8????????FF47108BCE|-51" }
			}},
			{"v1.1", new Dictionary<string, string>() {
					{"World.update()",             "4DBCFF15????????8B15????????38028D7DC0|-9" },
					{"UnitManager.updateBuffer()", "85D274198B0D????????8BD63909E8????????8BCEFF15????????EB178B0D????????8BD63909E8|-34" }
			}}
		};
		private IntPtr pointer;
		public BastionMemory Memory { get; set; }
		public string Name { get; set; }
		public string Version { get; set; }
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
		public string ReadString2(params int[] offsets) {
			if (!Memory.IsHooked) { return string.Empty; }
			IntPtr p = Memory.Program.Read<IntPtr>(Value, offsets);
			return Memory.Program.GetString2(p);
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
					IntPtr ptr = Memory.Program.FindSignatures(funcPatterns[version][name])[0];
					if (ptr != IntPtr.Zero) {
						Version = version;
						return ptr;
					}
				}
			}
			return IntPtr.Zero;
		}
	}
}