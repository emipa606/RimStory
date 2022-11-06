using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory.Harmony;

[HarmonyPatch(typeof(IncidentWorker))]
[HarmonyPatch("TryExecute")]
internal class IncidentWorkerHookShort
{
    private static void Postfix(IncidentWorker __instance, bool __result)
    {
        if (!__result)
        {
            return;
        }

        if (__instance is IncidentWorker_AnimalInsanityMass)
        {
            Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
        }


        if (__instance is IncidentWorker_ManhunterPack)
        {
            Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
        }

        if (__instance is IncidentWorker_ColdSnap)
        {
            Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
        }

        if (__instance is IncidentWorker_HeatWave)
        {
            Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
        }

        if (__instance is IncidentWorker_FarmAnimalsWanderIn)
        {
            Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
        }

        if (__instance is IncidentWorker_Infestation)
        {
            Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
        }

        if (__instance is IncidentWorker_WandererJoin)
        {
            Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
        }

        ////////////////////// DIRTY HACKS \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        if (__instance.def.defName == "VolcanicWinter")
        {
            Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
        }

        if (__instance.def.defName == "ToxicFallout")
        {
            Resources.eventsLog.Add(new IncidentShort(Utils.CurrentDate(), $"RS_{__instance.def.defName}"));
        }

        Log.Warning(__instance.def.defName);
    }
}