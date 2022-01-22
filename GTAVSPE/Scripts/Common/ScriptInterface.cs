using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Scripts.Interface
{
    using Information = Dictionary<Controller.Identifier, Action>;
    public interface ScriptKeyDelegate
    {

    }
    public interface ScriptCycle
    {
        void onUpdates(Information Data);
    }
}
