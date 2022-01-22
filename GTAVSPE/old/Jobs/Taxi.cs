using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace GTAVSPE.Scripts.Jobs.Taxi
{
    internal class Taxi: Script
    {
        DataModel.MainCharacter mMainCharacter;
        private bool onJob, searchPassenger = false;
        private Ped passengerPed;
        private Blip targetDestination;
        private float distanceCurrentPos;
        private GTA.UI.TextElement TE;
        public Taxi()
        {
        }
        private void TaxiKD(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.NumPad7 when Game.Player.Character.CurrentVehicle != null && onJob == false && searchPassenger == false:
                    mMainCharacter = DataModel.MainCharacter.Read();
                    if (checkIfEligibleForTheJob())
                    {
                        GTA.UI.Notification.Show("Searching for customer!", false);
                        searchPassenger = true;
                        if (searchPassenger) Tick += TaxiOnTick; else Tick -= TaxiOnTick;
                    }
                    break;
                case System.Windows.Forms.Keys.NumPad7 when Game.Player.Character.CurrentVehicle != null:
                    GTA.UI.Notification.Show("You Cancel the job!", false);
                    savedPassClear();
                    onJob = false;
                    searchPassenger = false;
                    Tick -= TaxiOnTick;
                    break;
                case System.Windows.Forms.Keys.E when Game.Player.Character.CurrentVehicle != null:
                    GetPaid();
                    break;
                default:
                    break;
            }
        }
        private bool checkIfEligibleForTheJob()
        {
            return false;//mMainCharacter.playerJob == (int)Helper.sAction.taxiWork;
        }
        private void TaxiOnTick(object sender, EventArgs e)
        {
            if (!onJob)
            {
                Ped[] peds = World.GetNearbyPeds(Game.Player.Character.Position, 50f);
                int odds = new Random().Next(0, 100);
                TE = new GTA.UI.TextElement($"Odds {odds} with peds Available {peds.Length}", new System.Drawing.PointF(20f, 20f), 1.0f);
                TE.Draw();
                if (peds.Length >= 2 && odds < 3)
                {
                    Ped tempP = peds[1];
                    if (!tempP.IsPersistent && tempP.CurrentVehicle == null && !tempP.IsDead && searchPassenger)
                    {
                        searchPassenger = false;
                        askToGoToVec(peds[1]);
                        Tick -= TaxiOnTick;
                    }
                }
            } else
            {
                
                if (passengerPed != null)
                {
                    if (passengerPed.IsInVehicle() && passengerPed.AttachedBlip != null)
                        passengerPed.AttachedBlip.Delete();
                    if (passengerPed.IsDead) savedPassClear();
                }
            }
        }
        private void askToGoToVec(Entity entity)
        {
            GTA.Vehicle myVec = Game.Player.Character.CurrentVehicle;
            if (entity == null && myVec != null) return;
            if (passengerPed != null)
            {
                if (targetDestination != null)
                    targetDestination.Delete();
                savedPassClear();
            }
            else if (entity.Model.IsPed)
            {
                GTA.UI.Notification.Show("You got customer!", false);
                passengerPed = (Ped)entity;
                passengerPed.Task.ClearAll();
                passengerPed.AlwaysKeepTask = true;
                passengerPed.BlockPermanentEvents = true;
                passengerPed.IsInvincible = true;
                passengerPed.CanRagdoll = false;
                passengerPed.AddBlip();
                passengerPed.Task.EnterVehicle(myVec, VehicleSeat.Passenger, -1, 1, EnterVehicleFlags.None);
                getTargetDestination();
            }
        }
        private void getTargetDestination(int loop = 0)
        {
            /*
            GTA.Math.Vector3 tempDest = Helper.hLoc.dest[new Random().Next(0, Helper.hLoc.dest.Length - 1)];
            distanceCurrentPos = World.GetDistance(Game.Player.Character.Position, tempDest);
            if (distanceCurrentPos > 50f)
            {
                targetDestination = World.CreateBlip(tempDest);
                onJob = true;
            }
            else if (loop < 9 && targetDestination == null)
                getTargetDestination(loop++);
            else if (loop >= 9)
                savedPassClear();
            */
        }
        private void savedPassClear()
        {
            if (passengerPed == null) return;
            passengerPed.AlwaysKeepTask = false;
            passengerPed.BlockPermanentEvents = false;
            passengerPed.CanRagdoll = true;
            passengerPed.IsRecordingCollisions = true;
            passengerPed.Task.ClearAllImmediately();
            passengerPed.Task.WanderAround();
            searchPassenger = false;
            onJob = false;
            Blip sspBlip = passengerPed.AttachedBlip;
            if (sspBlip != null)
                sspBlip.Delete();
            if (targetDestination != null)
                targetDestination.Delete();
            passengerPed = null;
        }
        private void GetPaid()
        {
            if (passengerPed == null) return;
            if (World.GetDistance(passengerPed.Position, targetDestination.Position) < 50f)
            {
                mMainCharacter.playerMoney += ((int)distanceCurrentPos / mMainCharacter.playerPayHourly);
                Game.Player.Money = mMainCharacter.playerMoney;
                DataModel.MainCharacter.Set(mMainCharacter);
                GTA.UI.Notification.Show($"You received ${mMainCharacter.playerMoney}!", false);
                savedPassClear();
            }
        }
    }
}
