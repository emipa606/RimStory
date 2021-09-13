using HarmonyLib;
using RimWorld;

namespace RimStory.Harmony
{
    [HarmonyPatch(typeof(IncidentWorker_RaidEnemy))]
    [HarmonyPatch("TryExecuteWorker")]
    internal class RaidHook
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
}