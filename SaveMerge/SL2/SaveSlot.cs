using SoulsFormats;
using System;
using System.IO;

namespace SaveMerge
{
    [Serializable]
    public class SaveSlot
    {
        public string CharName { get; }

        public int SoulLevel { get; }

        public int PlaytimeSeconds { get; }

        public byte[] MenuData { get; }

        public byte[] SlotData { get; }

        public SaveSlot(byte[] menuData, byte[] slotData)
        {
            MenuData = menuData;
            SlotData = slotData;

            using (var ms = new MemoryStream(menuData))
            {
                var br = new BinaryReaderEx(false, ms);
                CharName = br.ReadFixStrW(0x20);
                br.ReadInt16();
                SoulLevel = br.ReadInt32();
                PlaytimeSeconds = br.ReadInt32();
            }
        }

        public void WriteSteamID(int steamID)
        {
            byte[] buff = BitConverter.GetBytes(steamID);
            int offset = BitConverter.ToInt32(SlotData, 0x58);
            Buffer.BlockCopy(buff, 0, SlotData, offset + 0x6F, buff.Length);
        }
    }
}
