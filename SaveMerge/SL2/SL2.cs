using SoulsFormats;
using System;
using System.Text.RegularExpressions;

namespace SaveMerge
{
    class SL2
    {
        public string Path { get; set; }

        public int SteamID
        {
            get => Menu.SteamID;
            set => Menu.SteamID = value;
        }

        public MenuFile Menu { get; }

        public SaveSlot[] Slots { get; }

        public byte[] Regulation { get; }

        private BND4 bnd;

        public SL2(string path)
        {
            Path = path;
            bnd = BND4.Read(path);
            if (bnd.Files.Count != 12)
                throw new NotSupportedException($"Unexcepted number of files in SL2: {bnd.Files.Count}");

            for (int i = 0; i < 12; i++)
            {
                BinderFile file = bnd.Files[i];
                if (!Regex.IsMatch(file.Name, $"^USER_DATA{i:D3}$"))
                    throw new NotSupportedException($"Unexpected filename in SL2: {file.Name}");
            }

            Menu = new MenuFile(Decrypt(bnd.Files[10]));
            Regulation = Decrypt(bnd.Files[11]);
            Slots = new SaveSlot[10];
            for (int i = 0; i < 10; i++)
            {
                Slots[i] = new SaveSlot(Menu.SlotData[i], Decrypt(bnd.Files[i]));
            }
        }

        public void Write()
        {
            for (int i = 0; i < 10; i++)
            {
                SaveSlot slot = Slots[i];
                slot.WriteSteamID(SteamID);
                bnd.Files[i].Bytes = Encrypt(slot.SlotData);
                Menu.SlotData[i] = Slots[i].MenuData;
            }
            bnd.Files[10].Bytes = Encrypt(Menu.Write());
            bnd.Files[11].Bytes = Encrypt(Regulation);
            bnd.Write(Path);
        }

        private static byte[] Decrypt(BinderFile file)
        {
            return SFUtil.DecryptSL2File(file.Bytes, SFUtil.GetDS3SaveKey());
        }

        private static byte[] Encrypt(byte[] bytes)
        {
            return SFUtil.EncryptSL2File(bytes, SFUtil.GetDS3SaveKey());
        }
    }
}
