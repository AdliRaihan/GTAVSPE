using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Scripts.StoredDataModel
{
    public struct SDMInventory
    {
        public Inventories[] inventories;
        public struct Inventories
        {
            public int id;
            public int quantity;
            public int slot;
            public Inventories(int id, int quantity, int slot)
            {
                this.id = id;
                this.quantity = quantity;
                this.slot = slot;
            }
        }
        public static SDMInventory createNew()
        {
            SDMInventory inventoryModel = new SDMInventory();
            inventoryModel.inventories = new Inventories[0];
            return inventoryModel;
        }
        public static SDMInventory Load()
        {
            int id, quantity;
            var inventory = new SDMInventory();
            inventory.inventories = new Inventories[15];
            var iniCharacter = new Utils.IniFile(Constants.Paths.InventoryPath);
            for(int i = 0;i < 15; i++)
            {
                //GTA.UI.Notification.Show($"READING SLOT slot{i+1} {iniCharacter.KeyExists("id", $"slot{i + 1}")}");
                int.TryParse(iniCharacter.Read("id", $"slot{i + 1}"), out id);
                int.TryParse(iniCharacter.Read("quantity", $"slot{i + 1}"), out quantity);
                //inventory.inventories.Append(new Inventories(id, quantity));
                inventory.inventories[i].id = id;
                inventory.inventories[i].quantity = quantity;
                inventory.inventories[i].slot = i;
            }
            return inventory;
        }
        public static void Set(int id, int quantity, int slot)
        {
            var iniCharacter = new Utils.IniFile(Constants.Paths.InventoryPath);
            iniCharacter.Write("id", id.ToString(), $"slot{slot + 1}");
            iniCharacter.Write("quantity", quantity.ToString(), $"slot{slot + 1}");
        }
        public static int findItem(int id)
        {
            var loaded = Load();
            var findItem = loaded.inventories.ToList().Find(item => item.id == id);
            return findItem.quantity;
        }
    }
}
