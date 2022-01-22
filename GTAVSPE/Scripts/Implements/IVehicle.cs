using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTAVSPE.Scripts.Controller;
using GTAVSPE.Scripts.Struct;
using GTAVSPE.Scripts.StoredDataModel;

namespace GTAVSPE.Scripts.Implements
{
    public class IVehicle: Interface.ScriptCycle
    {
        private GTA.UI.ContainerElement pCEs, pCE;
        private ITaxi iTaxi;
        private GTAVSPE parent;
        public IVehicle()
        {
            //iTaxi = ITaxi.RunScript(this);
        }
        public void BindWithParent(GTAVSPE parent)
        {
            this.createDisplayFT();
            this.parent = parent;
            parent.Tick += OnTickVeh;
        }
        private void OnTickVeh(object sender, EventArgs e)
        {
            var cVeh = Game.Player.Character.CurrentVehicle;
            if (cVeh != null)
            {
                HandlePCInVeh(cVeh);
            }
        }
        public void onUpdates(Dictionary<Identifier, Action> Data)
        {

        }

        //
        //
        // 
        //
        //
        //
        public void HandlePCInVeh(GTA.Vehicle currentVeh)
        {
            currentVeh.EngineTorqueMultiplier = 0.3f;
            if (currentVeh.FuelLevel > 0)
                currentVeh.FuelLevel -= (currentVeh.Speed / 10000);
            updateFuelTank();
        }

        //
        //
        // This is to handle When PC trying to hijack a car
        //
        //
        //
        public void HandleOnTryToHijack()
        {
            var xp = parent.ListScripts.inventory.inventoryList.inventories.ToList().Find(item => item.id == Constants.rawItemCode.lockpick);
            var toEnterVeh = Game.Player.Character.VehicleTryingToEnter;
            if (toEnterVeh.Driver != null)
                toEnterVeh.LockStatus = VehicleLockStatus.CannotEnter;
            else if (toEnterVeh.IsStolen || Game.Player.Character.LastVehicle == toEnterVeh)
                toEnterVeh.LockStatus = VehicleLockStatus.Unlocked;
            else if (xp.quantity > 0)
            {
                xp.quantity -= 1;
                parent.ListScripts.inventory.updateQuantity(xp);
                toEnterVeh.LockStatus = VehicleLockStatus.CanBeBrokenInto;
            }
            else if (toEnterVeh != null)
                toEnterVeh.LockStatus = VehicleLockStatus.CannotEnter;
        }

        //
        //
        // 
        //
        //
        //
        private void createDisplayFT()
        {
            pCEs = new GTA.UI.ContainerElement(
                new System.Drawing.PointF(GTA.UI.Screen.Width - 150f, 50f),
                new System.Drawing.SizeF(100f, 3.5f),
                System.Drawing.Color.Black);
            pCE = new GTA.UI.ContainerElement(
                new System.Drawing.PointF(GTA.UI.Screen.Width - 150f, 50f),
                new System.Drawing.SizeF(3.5f, 3.5f),
                System.Drawing.Color.Red);
            pCE.Enabled = true;
            pCEs.Enabled = true;
        }
        private void updateFuelTank()
        {
            pCE.Size = new System.Drawing.SizeF(Game.Player.Character.CurrentVehicle.FuelLevel, 3.5f);
            pCEs.Draw();
            pCE.Draw();
        }
    }
}
