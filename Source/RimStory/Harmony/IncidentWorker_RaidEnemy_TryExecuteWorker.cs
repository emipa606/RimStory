using HarmonyLib;
using RimWorld;

namespace RimStory;

[HarmonyPatch(typeof(IncidentWorker_RaidEnemy), "TryExecuteWorker")]
internal class IncidentWorker_RaidEnemy_TryExecuteWorker
{
    private static void Postfix(IncidentParms parms)
    {
        if (Resources.rng.Next(101) <= Resources.randomChanceRaid)
        {
            Resources.events.Add(new ABigThreat(Utils.CurrentDate(), parms.faction));
        }

        Resources.eventsLog.Add(new ABigThreat(Utils.CurrentDate(), parms.faction));
    }
}