using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
namespace GTAVSPE
{
    public class DllMain: Script
    {
        private bool initSucc = false;
        public DllMain()
        {
        }
        private void OnTick(object sender, EventArgs e)
        {
            if (Game.Player != null && !initSucc)
            {
                Game.Player.Character.Weapons.RemoveAll();
                foreach (var x in World.GetAllEntities())
                {
                    if (x.Model.IsPed || x.Model.IsVehicle)
                        x.Delete();
                }
                foreach (var p in Helper.hObjectWithAction.NPC)
                {
                    Ped tempPed = World.CreatePed(new Model(p.Value), p.Key, 205f);
                    tempPed.IsPositionFrozen = true;
                    tempPed.BlockPermanentEvents = true;
                    tempPed.IsInvincible = true;
                    tempPed.DiesOnLowHealth = false;
                    tempPed.IsPersistent = true;
                }
                DataModel.MainCharacter mainCharacter = DataModel.MainCharacter.Read();
                Game.Player.Money = mainCharacter.playerMoney;
                initSucc = true;
                Tick -= OnTick;
            }
        }
    }
}
