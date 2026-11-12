using HarmonyLib;
using RimWorld;

namespace RimStory;

[HarmonyPatch(typeof(Building_Grave), nameof(Building_Grave.Notify_HauledTo))]
internal static class Building_Grave_Notify_HauledTo
{
    public static void Postfix(Building_Grave __instance)
    {
        Resources.lastGrave = __instance;
        if (__instance.Corpse.InnerPawn.IsColonist)
        {
            Resources.deadPawnsForMassFuneralBuried.Add(__instance.Corpse.InnerPawn);
        }
    }
}