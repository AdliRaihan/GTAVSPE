using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVSPE.Helper
{
    public struct sRarity
    {
        
    }
    public enum sAction
    {
        refuelCar, officeWork, taxiWork, noAction
    }
    public struct hObjectWithAction
    {
        public static Dictionary<int, sAction> action = new Dictionary<int,sAction>()
        {
            { 1933174915, sAction.refuelCar},
            { (int)GTA.PedHash.Business02AFM, sAction.officeWork},
            { (int)GTA.PedHash.Indian01AFY, sAction.taxiWork},
        };
        public static Dictionary<GTA.Math.Vector3, GTA.PedHash> NPC = new Dictionary<GTA.Math.Vector3, GTA.PedHash>()
        {
            { new GTA.Math.Vector3(-1082.159f, -261.8969f, 36.79615f), GTA.PedHash.Business02AFM },
            { new GTA.Math.Vector3(-338.7404f, -734.8129f, 33.65045f), GTA.PedHash.Indian01AFY }
            
        };
        public static sAction findByObject(int objHash)
        {
            if (action.ContainsKey(objHash))
                return action[objHash];
            return sAction.noAction;
        } 
    }
    public struct hLoc
    {
        public static GTA.Math.Vector3[] dest =
        {
            new GTA.Math.Vector3(-745.0074f, 139.396f, 59.76827f),
            new GTA.Math.Vector3(-251.1142f, -770.0549f, 32.1463f),
            new GTA.Math.Vector3(-256.942f, -329.985f, 29.33583f),
            new GTA.Math.Vector3(-372.3233f, -135.3674f, 38.1989f),
            new GTA.Math.Vector3(-825.0593f, -240.7728f, 36.53729f),
            new GTA.Math.Vector3(-476.3614f, -388.5049f, 33.45285f),
            new GTA.Math.Vector3(295.5137f, -584.6812f, 42.67582f),
            new GTA.Math.Vector3(113.4563f, -942.8816f, 29.08659f),
            new GTA.Math.Vector3(151.5141f, -1029.037f, 28.74233f),
            new GTA.Math.Vector3(207.1913f, -1273.062f, 28.70587f),
            new GTA.Math.Vector3(84.30432f, -1086.699f, 28.76143f),
            new GTA.Math.Vector3(-681.858f, 493.9752f, 109.6865f),
            new GTA.Math.Vector3(-2553.835f, 2320.854f, 32.72613f),
            new GTA.Math.Vector3(272.5592f, 155.8251f, 103.8236f),
            new GTA.Math.Vector3(396.9566f, -1481.525f, 28.80616f),
            new GTA.Math.Vector3(142.3623f, -999.486f, 28.62283f),
        };
    }
    public struct hAnim
    {
        public struct animMisscarsteal2pimpsex
        {
            public static string dict = "misscarsteal2pimpsex";
            public static string animSexF = "shagloop_hooker";
            public static string animSexM = "shagloop_pimp";
        }
        public struct animHookerSexPlayer
        {
            public static string dict = "mini@prostitutes@sexlow_veh";
            public static string animSexBJ = "low_car_bj_to_prop_p1_female";
            public static string animSex = "low_car_sex_loop_female";
        }
    }
    public struct hVoice
    {
        public struct voiceHookerWhite01
        {
            public static string dict = "S_F_Y_HOOKER_01_WHITE_FULL_01";
            public static string voiceHJ = "SEX_HJ";
        }
    }
}
