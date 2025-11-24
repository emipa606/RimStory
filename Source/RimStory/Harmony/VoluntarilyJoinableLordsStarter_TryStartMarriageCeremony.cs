using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory;

[HarmonyPatch(typeof(VoluntarilyJoinableLordsStarter),
    nameof(VoluntarilyJoinableLordsStarter.TryStartMarriageCeremony))]
internal class VoluntarilyJoinableLordsStarter_TryStartMarriageCeremony
{
    private static void Postfix(ref Pawn firstFiance, ref Pawn secondFiance)
    {
        Resources.events.Add(new AMarriage(Utils.CurrentDate(), firstFiance, secondFiance));
        Resources.eventsLog.Add(new AMarriage(Utils.CurrentDate(), firstFiance, secondFiance));
    }
}