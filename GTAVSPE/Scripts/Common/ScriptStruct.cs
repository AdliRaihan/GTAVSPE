using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Scripts.Struct
{
    public struct EntryListsScripts
    {
        public Implements.ITaxi taxiScript;
        public Implements.IVehicle vehicleScript;
    }
    public struct EntryDM
    {
        public Interface.ScriptCycle ScriptCycleDelegate;
    }
    public struct EntryControllerDM
    {
        public Controller.Identifier Caller;
        public Action Data;
    }
    public struct ItemsDetails
    {
        public string name;
        public Enum.PlayerItemAction actionFor;
        public int price;
        public static ItemsDetails Create(string name, Enum.PlayerItemAction actionFor, int price)
        {
            var _this = new ItemsDetails();
            _this.name = name;
            _this.actionFor = actionFor;
            _this.price = price;
            return _this;
        }
    }
    public struct NPCAction
    {
        public Enum.PCNPCInteraction interactionCode;
        public int[] sellItems;
        public static NPCAction Create(Enum.PCNPCInteraction interactionCode, int[] sellItems = null)
        {
            var _this = new NPCAction();
            _this.interactionCode = interactionCode;
            if (sellItems != null)
                _this.sellItems = sellItems;
            return _this;
        }
    }
}
