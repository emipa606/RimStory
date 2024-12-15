using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory.Harmony;

[HarmonyPatch(typeof(InteractionWorker_Breakup), nameof(InteractionWorker_Breakup.RandomBreakupReason))]
internal class InteractionWorker_Breakup_RandomBreakupReason
{
    private static void Postfix(Thought __result, ref Pawn initiator, ref Pawn recipient)
    {
        Resources.eventsLog.Add(new Breakup(Utils.CurrentDate(), initiator, recipient, __result));
    }
}