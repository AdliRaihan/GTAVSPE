using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonUI;
using LemonUI.Menus;
using GTAVSPE.Scripts.Constants;

namespace GTAVSPE.Scripts.Implements
{
    public class IBuy
    {
        public bool isIBuyShowActive = false;
        private ObjectPool OBPool;
        private NativeMenu OBMenu;
        private GTAVSPE parent;
        private Struct.NPCAction action;
        private Struct.ItemsDetails selectedItemIndex;
        private int idItem;
        public IBuy()
        {

        }
        public void BindWithParent(GTAVSPE parent)
        {
            this.parent = parent;
            parent.Tick += OnTickBuy;
            OBPool = new ObjectPool();
            OBMenu = new NativeMenu("Shop");
            OBPool.Add(OBMenu);
            //OBMenu.SelectedIndexChanged += selectedItem;
        }
        private void OnTickBuy(object sender, EventArgs e)
        {
            try
            {
                //TE.Draw();
                OBPool.Process();
                OBMenu.Visible = isIBuyShowActive;
            }
            catch (Exception ex)
            {

            }
        }
        public void shopUIShow(Struct.NPCAction data)
        {
            OBMenu.Clear();
            //var data = InteractableHumansNPCsAction.findKeyForName(hash);
            //data.interactionCode == Enum.PCNPCInteraction.shopTool;
            GetSellerItems(data);
        }
        public void GetSellerItems(Struct.NPCAction action)
        {
            this.action = action;
            foreach (var item in action.sellItems)
            {
                var itemMenu = new NativeItem(Items.findKeyForName(item).name);
                OBMenu.Add(itemMenu);
                itemMenu.Activated += activatedItem;
                itemMenu.Selected += selectedItem;
            }
            isIBuyShowActive = true;
            parent.ListScripts.cursorScript.cursorActive = !isIBuyShowActive;
        }

        private void selectedItem(object sender, SelectedEventArgs e)
        {
            idItem = e.Index;
            selectedItemIndex = Items.findKeyForName(action.sellItems[e.Index]);
        }
        private void activatedItem(object sender, EventArgs e)
        {
            GTA.UI.Notification.Show($"You Bought {selectedItemIndex.name} with price ~y~${selectedItemIndex.price}!");
            parent.ListScripts.inventory.put(action.sellItems[idItem], selectedItemIndex.price);
        }
    }
}
