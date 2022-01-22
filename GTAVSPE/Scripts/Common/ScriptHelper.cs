using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;

namespace GTAVSPE.Scripts.Helper
{
    internal static class PrepareWorld
    {
        public static void ClearPeds()
        {
            foreach (var ped in World.GetAllPeds())
                if (ped.CurrentVehicle == null)
                    ped.Delete();
        }
        public static void CreateStaticPed(Ped ped)
        {
            ped.IsPersistent = true;
            ped.IsPositionFrozen = true;
            ped.BlockPermanentEvents = true;
            ped.IsInvincible = true;
            ped.DiesOnLowHealth = false;
            ped.CanRagdoll = false;
            ped.IsCollisionProof = true;
        }
        public static Ped CreatePed(PedHash pedHash, Vector3 position, bool asStatic = false)
        {
            var ped = World.CreatePed(new Model(pedHash), position, 205f); 
            if (ped == null && asStatic)
                CreateStaticPed(ped);
            return ped;
        }
    }
}
