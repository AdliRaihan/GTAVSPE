using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.DataModel
{
    public struct MainCharacter
    {
        public int playerLevel;
        public int playerMoney;
        public int playerJob;
        public int playerPayHourly;
        public int playerStaff;
        public int weekOfDay;
        public static MainCharacter createNew()
        { 
            MainCharacter mainCharacter = new MainCharacter();
            mainCharacter.playerLevel = 0;
            mainCharacter.playerMoney = 0;
            mainCharacter.playerJob = 0;
            mainCharacter.playerPayHourly = 0;
            mainCharacter.playerStaff = 0;
            mainCharacter.weekOfDay = 0;
            return mainCharacter;
        }
        public static MainCharacter Read()
        {
            if (!System.IO.File.Exists("character.txt")) return MainCharacter.createNew();
            MainCharacter MCTemp = new MainCharacter();
            string[] dataModel = System.IO.File.ReadAllText("character.txt").Split('|');
            if (dataModel.Length < 5) return createNew();
            int.TryParse(dataModel[0], out MCTemp.playerLevel);
            int.TryParse(dataModel[1], out MCTemp.playerMoney);
            int.TryParse(dataModel[2], out MCTemp.playerJob);
            int.TryParse(dataModel[3], out MCTemp.playerPayHourly);
            int.TryParse(dataModel[4], out MCTemp.playerStaff);
            int.TryParse(dataModel[5], out MCTemp.weekOfDay);
            return MCTemp;
        }
        public static void Set(MainCharacter dm)
        {
            if (!System.IO.File.Exists("character.txt")) return;
            string lines = $"{dm.playerLevel}|{dm.playerMoney}|{dm.playerJob}|{dm.playerPayHourly}|{dm.playerStaff}|{dm.weekOfDay}";
            System.IO.File.WriteAllText("character.txt", lines);
        }
        public static void SetMoney(int value)
        {
            MainCharacter mainCharacter = Read();
            mainCharacter.playerMoney = value;
            Set(mainCharacter);
        }
    }
}
