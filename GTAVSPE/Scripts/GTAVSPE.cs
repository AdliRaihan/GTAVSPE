using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTAVSPE.Scripts.Controller;
using GTAVSPE.Scripts.Struct;
using GTAVSPE.Scripts.Extensions;

namespace GTAVSPE.Scripts
{
    public class GTAVSPE: Script
    {
        public GTAVSPEScripts ListScripts = new GTAVSPEScripts();
        public GTAVSPE()
        {
            ListScripts.vehScript = new Implements.IVehicle();
            ListScripts.buyScript = new Implements.IBuy();
            ListScripts.cursorScript = new Implements.ICursor();
            ListScripts.inventory = new Implements.IInventory();
            ListScripts.vehScript.BindWithParent(this);
            ListScripts.cursorScript.BindWithParent(this);
            ListScripts.inventory.BindWithParent(this);
            ListScripts.buyScript.BindWithParent(this);
            var communicator = _initialWorld.Listen();
            KeyDown += EntryKey.Make(communicator, this).Listen;
            EntryScene.Make(communicator);
        }
        private class _initialWorld : Interface.ScriptCycle
        {
            public static _initialWorld Listen()
            {
                return new _initialWorld();
            }
            public void onUpdates(Dictionary<Identifier, Action> Data)
            {
                if (Data == null) return;
            }
        }
    }
    public struct GTAVSPEScripts
    {
        public Implements.IVehicle vehScript;
        public Implements.IInventory inventory;
        public Implements.ICursor cursorScript;
        public Implements.IBuy buyScript;
    }
}
