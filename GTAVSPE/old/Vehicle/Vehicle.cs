using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace GTAVSPE.Scripts.Vehicle
{
    internal class Vehicle: Script
    {
        private GTA.UI.ContainerElement pCEs, pCE;
        private bool isToggleSport = false;
        public Vehicle()
        {
        }
        private void VehicleOnUpdate(object sender, EventArgs e)
        {
            if (Game.Player.Character.CurrentVehicle != null)
            {
                if (!isToggleSport)
                    Game.Player.Character.CurrentVehicle.EngineTorqueMultiplier = 0.3f;
                if (Game.Player.Character.CurrentVehicle.FuelLevel > 0)
                    Game.Player.Character.CurrentVehicle.FuelLevel -= (Game.Player.Character.CurrentVehicle.Speed / 10000);
                updateFuelTank();
            }
        }
        private void PointerKD(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Z when Game.Player.Character.CurrentVehicle != null:
                    isToggleSport = !isToggleSport;
                    Game.Player.Character.CurrentVehicle.EngineTorqueMultiplier = (isToggleSport) ? 0.3f : 1f;
                    break;
                default:
                    break;
            }
        }
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

        public static void refuelCurrentCar() {
            if (Game.Player.LastVehicle != null)
                Game.Player.Money -= ((100 - (int)Game.Player.LastVehicle.FuelLevel) * 10);
            Game.Player.LastVehicle.FuelLevel += (100 - (int)Game.Player.LastVehicle.FuelLevel);
        }
    }
}
