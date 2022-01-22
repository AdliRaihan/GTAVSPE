using GTAVSPE.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace GTAVSPE.Scripts
{
    internal class Pointer: Script
    {
        public bool isPointerDraw, charged = false;
        private GTA.UI.ContainerElement pCE;
        public Pointer()
        {
        }
        private void PointerOnTick(object sender, EventArgs e)
        {
            if (isPointerDraw)
                pCE.Draw();
        }
        private void PointerKD(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.X:
                    Game.Player.ForcedAim = isPointerDraw = !isPointerDraw;
                    Game.Player.IgnoredByEveryone = isPointerDraw;
                    break;
                case System.Windows.Forms.Keys.E when isPointerDraw:
                    checkTargetEntity(Game.Player.TargetedEntity);
                    break;
                case System.Windows.Forms.Keys.F:
                    handleOnVehicleTryingToEnter(Game.Player.Character.VehicleTryingToEnter);
                    break;
                case System.Windows.Forms.Keys.NumPad0 when isPointerDraw:
                    //askToGoToVec(Game.Player.TargetedEntity);
                    break;
                case System.Windows.Forms.Keys.NumPad9:
                    break;
                case System.Windows.Forms.Keys.NumPad3 when Game.Player.Character.CurrentVehicle != null:
                    //debugAnimation();
                    GTA.UI.Notification.Show(Game.Player.Character.Position.ToString(), false);
                    GTA.Math.Vector3 loc = Game.Player.Character.Position;

                    string lines = System.IO.File.ReadAllText("JOEMOM.txt");
                    lines += "new GTA.Math.Vector3(" + loc.X + ", " + loc.Y + ", " + loc.Z + "),\n";
                    System.IO.File.WriteAllText("JOEMOM.txt", lines);
                    break;
                case System.Windows.Forms.Keys.NumPad1:
                    GTA.UI.Notification.Show(Game.Player.Character.Heading.ToString(), false);
                    break;
                default:
                    break;
            }
        }
        private void createCursorMode()
        {
            pCE = new GTA.UI.ContainerElement(
                new System.Drawing.PointF(GTA.UI.Screen.Width/2, GTA.UI.Screen.Height / 2), 
                new System.Drawing.SizeF(3.5f, 3.5f),
                System.Drawing.Color.White);
            pCE.Enabled = true;
        }
        private void checkTargetEntity(Entity entity)
        {
            if (entity == null) return;
            if (entity.Model.IsAnimalPed && entity.IsDead)
            {
                Game.Player.Money = new Random().Next(0, 1000);
                entity.Delete();
            } else
            {
                switch (hObjectWithAction.findByObject(entity.Model.Hash))
                {
                    case sAction.refuelCar:
                        Vehicle.Vehicle.refuelCurrentCar();
                        break;
                    case sAction.officeWork:
                        handleOfficeWork();
                        break;
                    case sAction.taxiWork:
                        handleTaxiWork();
                        break;
                    default:
                        entity.IsRecordingCollisions = true;
                        GTA.UI.Notification.Show(entity.Model.NativeValue.ToString(), false);
                        break;
                }
            }
        }
        private void handleOnVehicleTryingToEnter(GTA.Vehicle tVec)
        {
            if (tVec != null)
                if (tVec.PreviouslyOwnedByPlayer)
                    tVec.LockStatus = VehicleLockStatus.Unlocked;
                else if (tVec.Driver != null)
                    if (tVec.Driver.IsDead)
                        tVec.LockStatus = VehicleLockStatus.CanBeBrokenInto;
                else
                    tVec.LockStatus = VehicleLockStatus.PlayerCannotEnter;
        }
        private void handleOfficeWork()
        { 
            var owHours = World.CurrentTimeOfDay.Hours;
            if (owHours >= 7 && owHours <= 10) { 
                World.CurrentTimeOfDay = TimeSpan.FromHours(owHours + 9);
                Game.Player.Money += (owHours + 1) * 55;
            } else
            {
                GTA.UI.Notification.Show("You can't Work Right now!", false);
            }
        }
        private void handleTaxiWork()
        {
            var mMainCharacter = DataModel.MainCharacter.Read();
            if (mMainCharacter.playerJob == (int)sAction.taxiWork) return;
            mMainCharacter.playerJob = (int)sAction.taxiWork;
            DataModel.MainCharacter.Set(mMainCharacter);
            GTA.UI.Notification.Show("You are now taxi driver!", false);
        }
    }
}
