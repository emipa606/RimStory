using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory;

[HarmonyPatch(typeof(RelationsUtility), nameof(RelationsUtility.TryDevelopBondRelation))]
internal static class RelationsUtility_TryDevelopBondRelation
{
    public static void Postfix(bool __result, ref Pawn humanlike, ref Pawn animal)
    {
        if (!__result)
        {
            return;
        }

        Resources.eventsLog.Add(new ABonded(Utils.CurrentDate(), humanlike, animal));
    }
}