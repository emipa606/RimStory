using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory.Harmony;

[HarmonyPatch(typeof(GameCondition))]
[HarmonyPatch("Init")]
internal class IncidentWorkerHookLong
{
    private static void Postfix(IncidentWorker __instance)
    {
        Log.Message($"it works {__instance.def.defName}");
    }
}