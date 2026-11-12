using HarmonyLib;
using RimWorld;
using Verse;

namespace RimStory;

[HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), nameof(InteractionWorker_RecruitAttempt.DoRecruit),
    typeof(Pawn), typeof(Pawn), typeof(bool))]
internal static class InteractionWorker_RecruitAttempt_DoRecruit
{
    public static void Postfix(Pawn recruiter, Pawn recruitee)
    {
        Resources.eventsLog.Add(new ARecruitment(Utils.CurrentDate(), recruiter, recruitee));
    }
}