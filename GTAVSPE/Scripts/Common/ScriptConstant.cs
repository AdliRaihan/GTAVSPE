using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTAVSPE.Scripts.Struct;
using GTAVSPE.Scripts.Enum;
namespace GTAVSPE.Scripts.Constants
{
    public struct InteractableHumansNPCsAction
    {
        public static Dictionary<int, NPCAction> Lists = new Dictionary<int, NPCAction>()
        {

            { 1933174915, NPCAction.Create(PCNPCInteraction.refuelTheCah) },
            { 303280717, NPCAction.Create(PCNPCInteraction.shopTool, new int[] {rawItemCode.sandwich, rawItemCode.water})},
            { (int)GTA.PedHash.ShopKeep01, NPCAction.Create(PCNPCInteraction.shopTool, new int[] {rawItemCode.lockpick, rawItemCode.advanceLockpick})},
            { (int)GTA.PedHash.Business02AFM, NPCAction.Create(PCNPCInteraction.shopTool, new int[] {rawItemCode.lockpick, rawItemCode.advanceLockpick})}
        };
        public static NPCAction findKeyForName(int key)
        {
            if (Lists.ContainsKey(key))
                return Lists[key];
            return NPCAction.Create(PCNPCInteraction.taxiJob, new int[] { });
        }
    }
    public struct InteractableHumansNPCs
    {
        public static Dictionary<GTA.Math.Vector3, GTA.PedHash> Lists = new Dictionary<GTA.Math.Vector3, GTA.PedHash>()
        {
            { 
                new GTA.Math.Vector3(-1082.159f, -261.8969f, 36.79615f), 
                GTA.PedHash.Business02AFM
            },
            { 
                new GTA.Math.Vector3(-338.7404f, -734.8129f, 33.65045f), 
                GTA.PedHash.Indian01AFY
            }
        };
    }
    public struct rawItemCode
    {
        public static int empty = -1;
        public static int lockpick = 0;
        public static int advanceLockpick = 1;
        public static int laptop = 2;
        public static int anyFood = 3;
        public static int anyDrink = 4;
        public static int handphone = 5;
        public static int jewel = 6;
        public static int anymeat = 7;
        public static int burger = 8;
        public static int sandwich = 9;
        public static int fries = 10;
        public static int cola = 11;
        public static int water = 12;
    }
    public struct Items
    {
        public static Dictionary<int, ItemsDetails> Lists = new Dictionary<int, Struct.ItemsDetails>()
        {
            {rawItemCode.empty, ItemsDetails.Create("Empty", PlayerItemAction.noItem, -1)},
            {rawItemCode.lockpick, ItemsDetails.Create("Lockpick", PlayerItemAction.lockpick, 50)},
            {rawItemCode.advanceLockpick, ItemsDetails.Create("Advance Lockpick", Enum.PlayerItemAction.advanceLockpick, 5000)},
            {rawItemCode.laptop, ItemsDetails.Create("Laptop", PlayerItemAction.propObject, 2400)},
            {rawItemCode.anyFood, ItemsDetails.Create("Any Food", PlayerItemAction.hunger, 150)},
            {rawItemCode.anyDrink, ItemsDetails.Create("Any Drink", PlayerItemAction.thirst, 75)},
            {rawItemCode.handphone, ItemsDetails.Create("Handphone", PlayerItemAction.propObject, 864)},
            {rawItemCode.jewel, ItemsDetails.Create("Jewel", PlayerItemAction.propObject, 964)},
            {rawItemCode.anymeat, ItemsDetails.Create("Any Meat", PlayerItemAction.noItem, 430)},
            {rawItemCode.burger, ItemsDetails.Create("Burger", PlayerItemAction.hunger, 35)},
            {rawItemCode.sandwich, ItemsDetails.Create("Sandwich", PlayerItemAction.hunger, 55)},
            {rawItemCode.fries, ItemsDetails.Create("Fries", PlayerItemAction.hunger, 15)},
            {rawItemCode.cola, ItemsDetails.Create("Cola", PlayerItemAction.noItem, 13)},
            {rawItemCode.water, ItemsDetails.Create("Water", PlayerItemAction.noItem, 7)},
        };
        public static ItemsDetails findKeyForName(int key)
        {
            if (Lists.ContainsKey(key))
                return Lists[key];
            return ItemsDetails.Create("Undefined", Enum.PlayerItemAction.noItem, 0);
        }
    }
    public struct Paths
    {
        public static string CharactersPath = @"scripts\GTAVSPE\character.ini";
        public static string InventoryPath = @"scripts\GTAVSPE\inventory.ini";
    }
}
