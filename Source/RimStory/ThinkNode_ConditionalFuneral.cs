using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace RimStory;

internal class ThinkNode_ConditionalFuneral : ThinkNode_Priority
{
    public ThinkTreeDutyHook dutyHook;

    private void CheckLeaveCurrentVoluntarilyJoinableLord(Pawn pawn)
    {
        var lord = pawn.GetLord();

        if (lord?.LordJob is not LordJob_VoluntarilyJoinable lordJob_VoluntarilyJoinable)
        {
            return;
        }

        if (lordJob_VoluntarilyJoinable.VoluntaryJoinPriorityFor(pawn) <= 0f)
        {
            lord.Notify_PawnLost(pawn, PawnLostCondition.LeftVoluntarily);
        }
    }

    private void JoinVoluntarilyJoinableLord(Pawn pawn)
    {
        var lord = pawn.GetLord();
        Lord lord2 = null;
        var num = 0f;
        if (lord != null)
        {
            if (lord.LordJob is not LordJob_VoluntarilyJoinable lordJob_VoluntarilyJoinable)
            {
                return;
            }

            lord2 = lord;
            num = lordJob_VoluntarilyJoinable.VoluntaryJoinPriorityFor(pawn);
        }

        var lords = pawn.Map.lordManager.lords;
        foreach (var lord1 in lords)
        {
            if (lord1.LordJob is not LordJob_VoluntarilyJoinable lordJob_VoluntarilyJoinable2)
            {
                continue;
            }

            if (lord1.CurLordToil.VoluntaryJoinDutyHookFor(pawn) != dutyHook)
            {
                continue;
            }

            var num2 = lordJob_VoluntarilyJoinable2.VoluntaryJoinPriorityFor(pawn);
            if (!(num2 > 0f))
            {
                continue;
            }

            if (lord2 != null && !(num2 > num))
            {
                continue;
            }

            lord2 = lord1;
            num = num2;
        }

        if (lord2 == null || lord == lord2)
        {
            return;
        }

        lord?.Notify_PawnLost(pawn, PawnLostCondition.LeftVoluntarily);

        lord2.AddPawn(pawn);
    }

    public override ThinkNode DeepCopy(bool resolve = true)
    {
        var thinkNode_JoinVoluntarilyJoinableLord = (ThinkNode_JoinVoluntarilyJoinableLord)base.DeepCopy(resolve);
        thinkNode_JoinVoluntarilyJoinableLord.dutyHook = dutyHook;
        return thinkNode_JoinVoluntarilyJoinableLord;
    }

    public override ThinkResult TryIssueJobPackage(Pawn pawn, JobIssueParams jobParams)
    {
        CheckLeaveCurrentVoluntarilyJoinableLord(pawn);
        JoinVoluntarilyJoinableLord(pawn);
        if (pawn.GetLord() != null && (pawn.mindState.duty == null || pawn.mindState.duty.def.hook == dutyHook))
        {
            return base.TryIssueJobPackage(pawn, jobParams);
        }

        return ThinkResult.NoJob;
    }
}