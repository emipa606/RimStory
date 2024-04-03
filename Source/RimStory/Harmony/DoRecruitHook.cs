using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory.Harmony;

[HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), nameof(InteractionWorker_RecruitAttempt.DoRecruit),
    [typeof(Pawn), typeof(Pawn), typeof(bool)])]
internal class DoRecruitHook
{
    private static void Postfix(Pawn recruiter, Pawn recruitee)
    {
        Resources.eventsLog.Add(new ARecruitment(Utils.CurrentDate(), recruiter, recruitee));
    }
}