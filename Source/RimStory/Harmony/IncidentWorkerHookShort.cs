using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory.Harmony;

[HarmonyPatch(typeof(IncidentWorker), nameof(IncidentWorker.TryExecute))]
internal class IncidentWorkerHookShort
{
    private static void Postfix(IncidentWorker __instance, bool __result)
    {
        if (!__result)
        {
            return;
        }

        switch (__instance)
        {
            case IncidentWorker_AnimalInsanityMass:
            case IncidentWorker_AggressiveAnimals:
            case IncidentWorker_ColdSnap:
            case IncidentWorker_HeatWave:
            case IncidentWorker_FarmAnimalsWanderIn:
            case IncidentWorker_Infestation:
            case IncidentWorker_WandererJoin:
                Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
                break;
        }

        ////////////////////// DIRTY HACKS \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        switch (__instance.def.defName)
        {
            case "VolcanicWinter":
            case "ToxicFallout":
                Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
                break;
        }

        Log.Warning(__instance.def.defName);
    }
}