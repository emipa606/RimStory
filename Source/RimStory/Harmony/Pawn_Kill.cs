using HarmonyLib;
using Verse;

namespace RimStory.Harmony;

[HarmonyPatch(typeof(Pawn), nameof(Pawn.Kill))]
internal class Pawn_Kill
{
    private static void Prefix(Pawn __instance)
    {
        if (!__instance.IsColonist)
        {
            return;
        }

        Resources.eventsLog.Add(new AMemorialDay(Utils.CurrentDate(), __instance));

        Resources.deadPawns.Add(__instance);
        Resources.deadPawnsForMassFuneral.Add(__instance);

        Resources.events.Add(new ADead(Utils.CurrentDate(), __instance));

        if (Resources.isMemorialDayCreated)
        {
            return;
        }

        Resources.events.Add(new AMemorialDay(Utils.CurrentDate(), __instance));
        Resources.isMemorialDayCreated = true;
    }
}