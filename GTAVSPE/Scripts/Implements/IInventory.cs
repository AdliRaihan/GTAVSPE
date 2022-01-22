using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GTA;
using GTA.UI;
using LemonUI;
using LemonUI.Menus;
using GTAVSPE.Scripts.StoredDataModel;

namespace GTAVSPE.Scripts.Implements
{
    public class IInventory
    {
        private GTAVSPE parent;
        private ObjectPool OBPool;
        private NativeMenu InventoryBanner;
        public SDMInventory inventoryList;
        public SDMCharacter character;
        TextElement TE;
        public int indexSelected = 0;
        public bool inventoryShow = false;
        public IInventory()
        {
            InternalLoadInventory();
            OBPool = new ObjectPool();
            InventoryBanner = containerBase();
            OBPool.Add(InventoryBanner);
            ListedInventory();
            //InventoryBanner.Add(listItemPlayer());
            TE = new TextElement($"Is Inventory Show True => {inventoryList.inventories.Length}", new PointF(20f, 20f), 1.0f);
        }
        private void InternalLoadInventory()
        {
            inventoryList = SDMInventory.Load();
            character = SDMCharacter.Load();
        }
        public void BindWithParent(GTAVSPE parent)
        {
            this.parent = parent;
            parent.Tick += OnTickInventory;
        }
        private void OnTickInventory(object sender, EventArgs e)
        {
            try
            {
                //TE.Draw();
                OBPool.Process();
                InventoryBanner.Visible = inventoryShow;
            } catch (Exception ex)
            {

            }
        }
        private void calculateIventoryMenuHeight()
        {
        }
        //
        //
        //
        //
        // CREATING INVENTORY BASE
        private NativeMenu containerBase()
        {
            Game.Player.Money = character.playerMoney;
            return new NativeMenu("Inventory", $"Money ${character.playerMoney}");
        }
        private void ListedInventory()
        {
            InventoryBanner.Clear();
            foreach (var p in inventoryList.inventories)
            {
                var data = Constants.Items.findKeyForName(p.id);

                if (p.quantity < 0)
                    InventoryBanner.Add(
                        new NativeItem(data.name)
                    );
                else
                    InventoryBanner.Add(
                        new NativeItem(data.name, "", $"x{p.quantity}")
                    );
            };
        }
        //
        //
        //
        //
        // CREATING INVENTORY BLOCK BASE
        public void put(int id, int price) {
            if (price > character.playerMoney)
            {
                GTA.UI.Notification.Show("You dont have any money!");
                return;
            } 
            Nullable<SDMInventory.Inventories> isIdExist = inventoryList.inventories.ToList().Find(item => item.id == id);
            Nullable<SDMInventory.Inventories> findSlotEmpty = inventoryList.inventories.ToList().Find(item => item.id == -1);
            if (isIdExist.Value.quantity == 0 && findSlotEmpty.Value.slot != 0)
            {
                var emptySlot = (SDMInventory.Inventories)findSlotEmpty;
                SDMInventory.Set(id, 1, emptySlot.slot);
                SDMCharacter.SetMoney(-price);
            } else if (isIdExist != null)
            {
                var tempData = (SDMInventory.Inventories)isIdExist;
                tempData.quantity++;
                SDMInventory.Set(id, tempData.quantity, tempData.slot);
                SDMCharacter.SetMoney(-price);
            }
            InternalLoadInventory();
            ListedInventory();
        }
        public void updateQuantity(SDMInventory.Inventories target)
        {
            if (target.quantity <= 0)
            {
                SDMInventory.Set(-1, -1, target.slot);
            } else
            {
                SDMInventory.Set(target.id, target.quantity, target.slot);
            }
            InternalLoadInventory();
            ListedInventory();
        }
        private void selectedItem(object sender, SelectedEventArgs e)
        {
            indexSelected = e.Index;
        }
        private void activatedItem(object sender, EventArgs e)
        {
            //inventoryList.inventories[selectedItem].
            //Notification.Show($"You Bought {selectedItemIndex.name} with price ~y~${selectedItemIndex.price}!");
            //parent.ListScripts.inventory.put(action.sellItems[idItem], selectedItemIndex.price);
        }
    }
}
