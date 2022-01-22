using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTAVSPE.Scripts
{
    internal class EntryKey
    {
        private GTAVSPE parent;
        private Struct.EntryDM ScriptDelegate = new Struct.EntryDM();
        public static EntryKey Make(Interface.ScriptCycle CycleDelegate, GTAVSPE parent)
        {
            var EK = new EntryKey(CycleDelegate);
            EK.parent = parent;
            return EK;
        }
        private EntryKey(Interface.ScriptCycle CycleDelegate)
        {
            ScriptDelegate.ScriptCycleDelegate = CycleDelegate;
        }
        public void Listen(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.A || e.KeyCode == Keys.S || e.KeyCode == Keys.D || e.KeyCode == Keys.Shift) { return; }
            switch (e.KeyCode)
            {
                case Keys.Escape when parent.ListScripts.inventory.inventoryShow || parent.ListScripts.buyScript.isIBuyShowActive:
                    parent.ListScripts.inventory.inventoryShow = parent.ListScripts.buyScript.isIBuyShowActive = false;
                    break;
                case Keys.F when GTA.Game.Player.Character.CurrentVehicle == null:
                    parent.ListScripts.vehScript.HandleOnTryToHijack();
                    break;
                case Keys.G when parent.ListScripts.cursorScript.cursorActive:
                    parent.ListScripts.cursorScript.getFreeAimingEntities();
                    break;
                case Keys.X:
                    parent.ListScripts.cursorScript.cursorActive = !parent.ListScripts.cursorScript.cursorActive;
                    GTA.Game.Player.ForcedAim = parent.ListScripts.cursorScript.cursorActive;
                    break;
                case Keys.NumPad0 when parent.ListScripts.cursorScript.cursorActive:
                    if (GTA.Game.Player.TargetedEntity != null)
                        GTA.UI.Notification.Show($"{GTA.Game.Player.TargetedEntity.Model.Hash}");
                    break;
                case Keys.I:
                    parent.ListScripts.inventory.inventoryShow = !parent.ListScripts.inventory.inventoryShow;
                    break;
                default:
                    break;
            }
        }
    }
}
