using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Scripts.Controller
{
    using Information = Dictionary<Controller.Identifier, Struct.EntryControllerDM>;
    public enum Identifier
    {
        CIEntryScene, CIVehicle, CINoControlListed
    }
    public interface ScriptController : IDisposable
    {

    };
    public interface CommunicationDataSource
    {
        Information dataSource { get; set; }
    }
}
