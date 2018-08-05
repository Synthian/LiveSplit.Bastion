// This file is pretty monolithic and should probably be split up
// (but I didn't write it initially so I'm gonna leave it mostly untouched)

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LiveSplit.Bastion.Memory
{
    public class BastionMemory
    {
        private ProgramPointer nextMap, player;
        public Process Program { get; set; }
        public bool IsHooked { get; set; } = false;
        private DateTime lastHooked;

        public BastionMemory()
        {
            nextMap = new ProgramPointer(this, MemPointer.World_Update) { AutoDeref = false };
            player = new ProgramPointer(this, MemPointer.UnitManager_UpdateBuffer) { AutoDeref = false };
            lastHooked = DateTime.MinValue;
        }

        // --- Variable retrieval methods ---
        // These work by following a pointer path from a pre-located source

        // Retrieves the AllowInput variable from game data
        // Basically, the variable that tells you if you have control of the kid
        public bool AllowInput()
        {
            if (player.Version == MemVersion.V2)
            {
                return player.Read<bool>(0x0, 0x4, 0x8, 0x308, 0x60);
            }
            else
            {
                return player.Read<bool>(0x0, 0x4, 0xc, 0x308, 0x60);
            }
        }

        // Retrieves PlayerUnit variable
        // This is a player object that exists when the game is loaded
        // The object will not be there when in a menu, for instance
        public int PlayerUnit()
        {
            if (player.Version == MemVersion.V2)
            {
                return player.Read<int>(0x0, 0x4, 0x8, 0x308, 0x8);
            }
            else
            {
                return player.Read<int>(0x0, 0x4, 0xc, 0x308, 0x8);
            }
        }

        // Retrieves the X position of the kid
        public float PlayerX()
        {
            if (player.Version == MemVersion.V2)
            {
                return player.Read<float>(0x0, 0x4, 0x8, 0x308, 0x8, 0xd8);
            }
            else
            {
                return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0x8, 0xd8);
            }
        }

        // Retrieves the Y position of the kid
        public float PlayerY()
        {
            if (player.Version == MemVersion.V2)
            {
                return player.Read<float>(0x0, 0x4, 0x8, 0x308, 0x8, 0xdc);
            }
            else
            {
                return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0x8, 0xdc);
            }
        }

        // Retrieves the X position of the object that the kid is targeting
        // In this context, "targeting" refers to what the kid will try to interact with.
        public float targetX()
        {
            return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0xc, 0xd8);
        }

        // Retrieves the Y position of the object that the kid is targeting
        public float targetY()
        {
            return player.Read<float>(0x0, 0x4, 0xc, 0x308, 0xc, 0xdc);
        }

        // TODO: remember what the MapPointer variable is
        public int MapPointer()
        {
            return nextMap.Value.ToInt32();
        }

        // Retrieves the name of the map that was most recently loaded
        // i.e. "ProtoIntro02a.map"
        public string NextMapName()
        {
            int length = 0;
            if (nextMap.Version == MemVersion.V2)
            {
                length = nextMap.Read<int>(0x2c, 0x4);
            }
            else
            {
                length = nextMap.Read<int>(0x2c, 0x8);
            }
            if (length < 120 && length > 0)
            {
                string mapName = nextMap.Read(0x2c);

                if (mapName.EndsWith(".map", StringComparison.OrdinalIgnoreCase))
                {
                    return System.IO.Path.GetFileName(mapName);
                }
            }
            return null;
        }

        // Hooks the splitter onto the Bastion process
        public bool HookProcess()
        {
            if ((Program == null || Program.HasExited) && DateTime.Now > lastHooked.AddSeconds(1))
            {
                lastHooked = DateTime.Now;
                Process[] processes = Process.GetProcessesByName("Bastion");
                Program = processes.Length == 0 ? null : processes[0];
                IsHooked = true;
            }

            if (Program == null || Program.HasExited)
            {
                IsHooked = false;
            }

            return IsHooked;
        }

        public void Dispose()
        {
            if (Program != null)
            {
                Program.Dispose();
            }
        }
    }
    
    // Versions of Memory layout, enumerated
    public enum MemVersion
    {
        None,
        V1,
        V2,
        V3
    }

    // The enumerated starting positions for pointer pathing
    public enum MemPointer
    {
        World_Update,
        UnitManager_UpdateBuffer
    }


    public class ProgramPointer
    {
        // Dictionary storing the data to be found in memory that orient the process
        // Basically, we don't know where certain things will be in memory, but we know where things are relative to each other
        // These are defined starting points that we can find
        private static Dictionary<MemVersion, Dictionary<MemPointer, string>> funcPatterns = new Dictionary<MemVersion, Dictionary<MemPointer, string>>() {
            {MemVersion.V1, new Dictionary<MemPointer, string>() {
                    {MemPointer.World_Update,             "4DB8FF15????????8B15????????39028D7DBC|-9" },
                    {MemPointer.UnitManager_UpdateBuffer, "85C0743D8B3D????????8B470C8B57043B4204750B8B570C428BCFE8????????8B4F048B5F0C8D430189470C568BD3E8????????FF47108BCE|-51" }
            }},
            {MemVersion.V2, new Dictionary<MemPointer, string>() {
                    {MemPointer.World_Update,             "4DBCFF15????????8B15????????38028D7DC0|-9" },
                    {MemPointer.UnitManager_UpdateBuffer, "85D274198B0D????????8BD63909E8????????8BCEFF15????????EB178B0D????????8BD63909E8|-34" }
            }},
            {MemVersion.V3, new Dictionary<MemPointer, string>() {
                    {MemPointer.World_Update,             "4DB8FF15????????8B15????????39028D7DBC|-9" },
                    {MemPointer.UnitManager_UpdateBuffer, "85C074??8B3D????????8B470C8B????????????????????????????????????????8B4F048B5F0C8D430189470C568BD3E8??????????????????|-53" }
            }},
        };

        private IntPtr pointer;
        public BastionMemory Memory { get; set; }
        public MemPointer Name { get; set; }
        public MemVersion Version { get; set; }
        public bool AutoDeref { get; set; }
        private int lastID;
        private DateTime lastTry;


        public ProgramPointer(BastionMemory memory, MemPointer pointer)
        {
            this.Memory = memory;
            this.Name = pointer;
            this.AutoDeref = true;
            lastID = memory.Program == null ? -1 : memory.Program.Id;
            lastTry = DateTime.MinValue;
        }

        public IntPtr Value
        {
            get
            {
                GetPointer();
                return pointer;
            }
        }
        public T Read<T>(params int[] offsets) where T : struct
        {
            return Memory.Program.Read<T>(Value, offsets);
        }
        public string Read(params int[] offsets)
        {
            if (!Memory.IsHooked) { return string.Empty; }

            bool is64bit = Memory.Program.Is64Bit();
            IntPtr p = IntPtr.Zero;
            if (is64bit)
            {
                p = (IntPtr)Memory.Program.Read<long>(Value, offsets);
            }
            else
            {
                p = (IntPtr)Memory.Program.Read<int>(Value, offsets);
            }
            return Memory.Program.Read(p, Version == MemVersion.V1);
        }
        public void Write(int value, params int[] offsets)
        {
            Memory.Program.Write(Value, value, offsets);
        }
        public void Write(long value, params int[] offsets)
        {
            Memory.Program.Write(Value, value, offsets);
        }
        public void Write(double value, params int[] offsets)
        {
            Memory.Program.Write(Value, value, offsets);
        }
        public void Write(float value, params int[] offsets)
        {
            Memory.Program.Write(Value, value, offsets);
        }
        public void Write(short value, params int[] offsets)
        {
            Memory.Program.Write(Value, value, offsets);
        }
        public void Write(byte value, params int[] offsets)
        {
            Memory.Program.Write(Value, value, offsets);
        }
        public void Write(bool value, params int[] offsets)
        {
            Memory.Program.Write(Value, value, offsets);
        }
        private void GetPointer()
        {
            if (!Memory.IsHooked)
            {
                pointer = IntPtr.Zero;
                Version = MemVersion.None;
                return;
            }

            if (Memory.Program.Id != lastID)
            {
                pointer = IntPtr.Zero;
                Version = MemVersion.None;
                lastID = Memory.Program.Id;
            }
            if (pointer == IntPtr.Zero && DateTime.Now > lastTry.AddSeconds(1))
            {
                lastTry = DateTime.Now;
                pointer = GetVersionedFunctionPointer();
                if (pointer != IntPtr.Zero)
                {
                    bool is64bit = Memory.Program.Is64Bit();
                    if (AutoDeref)
                    {
                        if (is64bit)
                        {
                            pointer = (IntPtr)Memory.Program.Read<long>(pointer, 0, 0);
                        }
                        else
                        {
                            pointer = (IntPtr)Memory.Program.Read<int>(pointer, 0, 0);
                        }
                    }
                    else if (is64bit)
                    {
                        pointer = (IntPtr)Memory.Program.Read<long>(pointer, 0);
                    }
                    else
                    {
                        pointer = (IntPtr)Memory.Program.Read<int>(pointer, 0);
                    }
                }
            }
        }
        private IntPtr GetVersionedFunctionPointer()
        {
            foreach (MemVersion version in Enum.GetValues(typeof(MemVersion)))
            {
                Dictionary<MemPointer, string> patterns = null;
                if (!funcPatterns.TryGetValue(version, out patterns)) { continue; }

                string pattern = null;
                if (!patterns.TryGetValue(Name, out pattern)) { continue; }

                IntPtr ptr = Memory.Program.FindSignatures(pattern)[0];
                if (ptr != IntPtr.Zero)
                {
                    Version = version;
                    return ptr;
                }
            }
            Version = MemVersion.None;
            return IntPtr.Zero;
        }
    }
}