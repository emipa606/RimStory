using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory.Harmony;

[HarmonyPatch(typeof(VoluntarilyJoinableLordsStarter))]
[HarmonyPatch("TryStartMarriageCeremony")]
internal class StartMarriageCeremony
{
    private static void Postfix(ref Pawn firstFiance, ref Pawn secondFiance)
    {
        Resources.events.Add(new AMarriage(Utils.CurrentDate(), firstFiance, secondFiance));
        Resources.eventsLog.Add(new AMarriage(Utils.CurrentDate(), firstFiance, secondFiance));
    }
}