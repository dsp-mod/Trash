using System;
using BepInEx;
using HarmonyLib;
namespace Trash
{
    [BepInPlugin("sky.plugins.dsp.Trash", "Trash", "1.0")]
    public class Trash : BaseUnityPlugin
    {
        static int times;
        void Start()
        {
            Harmony.CreateAndPatchAll(typeof(Trash), null);
            times = Config.Bind<int>("config", "times", 100, "销毁每个物品获得沙土的数量").Value;
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(StorageComponent), "AddItem",new Type[] { typeof(int),typeof(int), typeof(bool) })]
        public static bool DoTrash(ref int __result, StorageComponent __instance,int itemId, int count, bool useBan = false)
        {
            if (useBan && __instance.size==__instance.bans && itemId!=0 && count!=0)
            {
                int num = count* times;
                GameMain.mainPlayer.SetSandCount(GameMain.mainPlayer.sandCount + num);
                __result = 1;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}