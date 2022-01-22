using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace GTAVSPE.Scripts.Expenses
{
    internal class DailyExpenses: Script
    {
        DataModel.MainCharacter mMainCharacter;
        public DailyExpenses()
        {
        }
        private void ExpensesOnTick(object sender, EventArgs e)
        {
            if (mMainCharacter.weekOfDay != ((int)World.CurrentDate.DayOfWeek))
            {
                GTA.UI.Notification.Show("You have been charged $250 for daily expenses!");
                mMainCharacter.weekOfDay = ((int)World.CurrentDate.DayOfWeek);
                mMainCharacter.playerMoney -= 250;
                Game.Player.Money = mMainCharacter.playerMoney;
                DataModel.MainCharacter.Set(mMainCharacter);
            }
        }
    }
}
