using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory.Harmony;

[HarmonyPatch(typeof(VoluntarilyJoinableLordsStarter),
    nameof(VoluntarilyJoinableLordsStarter.TryStartMarriageCeremony))]
internal class StartMarriageCeremony
{
    private static void Postfix(ref Pawn firstFiance, ref Pawn secondFiance)
    {
        Resources.events.Add(new AMarriage(Utils.CurrentDate(), firstFiance, secondFiance));
        Resources.eventsLog.Add(new AMarriage(Utils.CurrentDate(), firstFiance, secondFiance));
    }
}