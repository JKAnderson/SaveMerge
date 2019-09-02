using SoulsFormats;
using System.IO;

namespace SaveMerge
{
    class MenuFile
    {
        public int SteamID { get; set; }

        public bool[] OccupiedSlots { get; set; }

        public byte[][] SlotData { get; set; }

        private byte[] Data;

        public MenuFile(byte[] data)
        {
            Data = data;
            using (var ms = new MemoryStream(Data))
            {
                var br = new BinaryReaderEx(false, ms);
                SteamID = br.GetInt32(8);
                OccupiedSlots = br.GetBooleans(0x1098, 10);
                SlotData = new byte[10][];
                for (int i = 0; i < 10; i++)
                    SlotData[i] = br.GetBytes(0x10A2 + 0x22A * i, 0x22A);
            }
        }

        public byte[] Write()
        {
            using (var ms = new MemoryStream())
            {
                var bw = new BinaryWriterEx(false, ms);
                bw.WriteBytes(Data);
                bw.Position = 8;
                bw.WriteInt32(SteamID);
                bw.Position = 0x1098;
                bw.WriteBooleans(OccupiedSlots);
                for (int i = 0; i < 10; i++)
                {
                    bw.Position = 0x10A2 + 0x22A * i;
                    bw.WriteBytes(SlotData[i]);
                }
                return bw.FinishBytes();
            }
        }
    }
}
