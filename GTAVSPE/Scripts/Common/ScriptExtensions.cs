using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Scripts.Extensions
{
    using Information = Dictionary<Controller.Identifier, Struct.EntryControllerDM>;
    internal static class PrepareDataTypes
    {
        public static Controller.Identifier KeyExist(this Information variable, Controller.Identifier matcher)
        {
            if (variable.ContainsKey(matcher))
                return matcher;
            return Controller.Identifier.CINoControlListed;
        }
    }
}
