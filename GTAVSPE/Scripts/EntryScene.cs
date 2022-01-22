using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Scripts
{
    internal class EntryScene
    {
        private GTAVSPE parent;
        private Struct.EntryDM ScriptDelegate = new Struct.EntryDM();
        public static void Make(Interface.ScriptCycle CycleDelegate)
        {
            new EntryScene(CycleDelegate); 
        }
        private EntryScene(Interface.ScriptCycle CycleDelegate)
        {
            ScriptDelegate.ScriptCycleDelegate = CycleDelegate;
            StartConfigurateWorld();
        }
        private void StartConfigurateWorld()
        {
            Helper.PrepareWorld.ClearPeds();
            foreach (var dictionary in Constants.InteractableHumansNPCs.Lists)
                _ = Helper.PrepareWorld.CreatePed(dictionary.Value, dictionary.Key);
        }
    }
}
