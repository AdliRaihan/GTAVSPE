using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Scripts.StoredDataModel
{
    public struct SDMCharacter
    {
        public int playerLevel;
        public int playerMoney;
        public int playerJob;
        public int playerPayHourly;
        public int playerStaff;
        public int weekOfDay;
        public static SDMCharacter createNew()
        {
            SDMCharacter inventoryModel = new SDMCharacter();
            return inventoryModel;
        }
        public static SDMCharacter Load()
        {
            if (!System.IO.File.Exists(Constants.Paths.CharactersPath))
            {
                GTA.UI.Notification.Show("PATH NOT FOUND!");
                return createNew();
            }
            var character = new SDMCharacter();
            var iniCharacter = new Utils.IniFile(Constants.Paths.CharactersPath);
            //GTA.UI.Notification.Show($"PATH FOUND READING LEVEL {iniCharacter.Read("Lv", "character")} {iniCharacter.KeyExists("Lv", "character")}!");
            int.TryParse(iniCharacter.Read("Lv", "character"), out character.playerLevel);
            int.TryParse(iniCharacter.Read("Money", "character"), out character.playerMoney);
            int.TryParse(iniCharacter.Read("Job", "character"), out character.playerJob);
            int.TryParse(iniCharacter.Read("PayHourly", "character"), out character.playerPayHourly);
            int.TryParse(iniCharacter.Read("Position", "character"), out character.playerStaff);
            int.TryParse(iniCharacter.Read("WeekOfDay", "character"), out character.weekOfDay);
            return character;
        }
        public static void Set(SDMCharacter dm)
        {
            if (!System.IO.File.Exists(Constants.Paths.CharactersPath)) return;
            var iniCharacter = new Utils.IniFile(Constants.Paths.CharactersPath);
            iniCharacter.Write("Lv", dm.playerLevel.ToString());
            iniCharacter.Write("Money", dm.playerMoney.ToString());
            iniCharacter.Write("Job", dm.playerJob.ToString());
            iniCharacter.Write("PayHourly", dm.playerPayHourly.ToString());
            iniCharacter.Write("Position", dm.playerStaff.ToString());
            iniCharacter.Write("WeekOfDay", dm.weekOfDay.ToString());
        }
        public static void SetMoney(int value)
        {
            if (!System.IO.File.Exists(Constants.Paths.CharactersPath)) return;
            GTA.Game.Player.Money += value;
            var iniCharacter = new Utils.IniFile(Constants.Paths.CharactersPath);
            iniCharacter.Write("Money", GTA.Game.Player.Money.ToString(), "character");
        }
    }
}
