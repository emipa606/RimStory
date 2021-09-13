using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory.Harmony
{
    [HarmonyPatch(typeof(InteractionWorker_Breakup))]
    [HarmonyPatch("RandomBreakupReason")]
    internal class RandomBreakup
    {
        private static void Postfix(Thought __result, ref Pawn initiator, ref Pawn recipient)
        {
            Resources.eventsLog.Add(new Breakup(Utils.CurrentDate(), initiator, recipient, __result));
        }
    }
}