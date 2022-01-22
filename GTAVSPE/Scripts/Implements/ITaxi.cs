using GTAVSPE.Scripts.Controller;
using GTAVSPE.Scripts.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Scripts.Implements
{
    public class ITaxi: IVehicle
    {
        private Interface.ScriptCycle ITaxiDelegate;
        public ITaxi()
        {

        }
        public static ITaxi RunScript(Interface.ScriptCycle CycleDelegate)
        {
            var script = new ITaxi(CycleDelegate);
            return script;
        }
        private ITaxi(Interface.ScriptCycle CycleDelegate)
        {
            this.ITaxiDelegate = CycleDelegate;
        }
        public void ListenHKTaxi(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }
    }
}
