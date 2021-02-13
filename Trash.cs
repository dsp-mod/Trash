using System;
using BepInEx;
using HarmonyLib;
namespace Trash
{
    [BepInPlugin("me.sky.plugin.Trash", "Trash Plugin", "1.1")]
    public class Trash : BaseUnityPlugin
    {
        void Start()
        {
            Harmony.CreateAndPatchAll(typeof(Trash), null);
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(StorageComponent), "AddItem",new Type[] { typeof(int),typeof(int), typeof(bool) })]
        public static bool DoTrash(ref int __result, StorageComponent __instance,int itemId, int count, bool useBan = false)
        {
            if (useBan && __instance.size==__instance.bans && itemId!=0 && count!=0)
            {
                int num = count*1000;
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