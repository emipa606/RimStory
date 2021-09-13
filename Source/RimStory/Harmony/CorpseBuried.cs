using HarmonyLib;
using RimWorld;

namespace RimStory.Harmony
{
    [HarmonyPatch(typeof(Building_Grave))]
    [HarmonyPatch("Notify_CorpseBuried")]
    internal class CorpseBuried
    {
        private static void Postfix(Building_Grave __instance)
        {
            Resources.lastGrave = __instance;
            if (__instance.Corpse.InnerPawn.IsColonist)
            {
                Resources.deadPawnsForMassFuneralBuried.Add(__instance.Corpse.InnerPawn);
            }
        }
    }
}