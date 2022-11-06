using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory.Harmony;

[HarmonyPatch(typeof(RelationsUtility))]
[HarmonyPatch("TryDevelopBondRelation")]
internal class BondHook
{
    private static void Postfix(bool __result, ref Pawn humanlike, ref Pawn animal)
    {
        if (!__result)
        {
            return;
        }

        Log.Message("bonded");
        Resources.eventsLog.Add(new ABonded(Utils.CurrentDate(), humanlike, animal));
    }
}