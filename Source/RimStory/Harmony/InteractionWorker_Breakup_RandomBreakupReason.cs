using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory;

[HarmonyPatch(typeof(InteractionWorker_Breakup), nameof(InteractionWorker_Breakup.RandomBreakupReason))]
internal static class InteractionWorker_Breakup_RandomBreakupReason
{
    public static void Postfix(Thought __result, ref Pawn initiator, ref Pawn recipient)
    {
        Resources.eventsLog.Add(new Breakup(Utils.CurrentDate(), initiator, recipient, __result));
    }
}