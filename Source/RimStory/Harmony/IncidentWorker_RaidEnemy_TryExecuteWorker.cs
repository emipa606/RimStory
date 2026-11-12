using HarmonyLib;
using RimWorld;

namespace RimStory;

[HarmonyPatch(typeof(IncidentWorker_RaidEnemy), "TryExecuteWorker")]
internal static class IncidentWorker_RaidEnemy_TryExecuteWorker
{
    public static void Postfix(IncidentParms parms)
    {
        if (Resources.rng.Next(101) <= Resources.randomChanceRaid)
        {
            Resources.events.Add(new ABigThreat(Utils.CurrentDate(), parms.faction));
        }

        Resources.eventsLog.Add(new ABigThreat(Utils.CurrentDate(), parms.faction));
    }
}