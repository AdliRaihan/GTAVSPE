using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace GTAVSPE.Scripts.Implements
{
    public class ICursor
    {
        public bool cursorActive;
        private GTA.UI.ContainerElement pCE;
        private GTAVSPE parent;
        public ICursor()
        {
            //iTaxi = ITaxi.RunScript(this);
        }
        public void BindWithParent(GTAVSPE parent)
        {
            createCursorMode();
            this.parent = parent;
            parent.Tick += OnTickCursor;
        }
        private void OnTickCursor(object sender, EventArgs e)
        {
            if (cursorActive)
                pCE.Draw();
        }
        private void createCursorMode()
        {
            pCE = new GTA.UI.ContainerElement(
                new System.Drawing.PointF(GTA.UI.Screen.Width / 2, GTA.UI.Screen.Height / 2),
                new System.Drawing.SizeF(3.5f, 3.5f),
                System.Drawing.Color.White);
            pCE.Enabled = true;
        }
        public void getFreeAimingEntities()
        {
            if (!parent.ListScripts.inventory.inventoryShow && Game.Player.TargetedEntity != null)
            {
                var data = Constants.InteractableHumansNPCsAction.findKeyForName(Game.Player.TargetedEntity.Model.Hash);
                switch (data.interactionCode)
                {
                    case Enum.PCNPCInteraction.shopTool:
                        parent.ListScripts.buyScript.shopUIShow(data);
                        break;
                    default:
                        break;
                }
            } else
            {
                GTA.UI.Notification.Show("NIL Entity Somehow!");
            }
        }
    }
}
