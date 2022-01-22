using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Helper
{
    interface dBaseListener
    {
        void __sendToMainWith(Dictionary<string, Object> information);
    }
    interface dBaseFeature
    {
        void RunModule();
    }
}
