using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace RimStory
{
    internal class ThinkNode_ConditionalFuneral : ThinkNode_Priority
    {
        // Token: 0x040003BE RID: 958
        public ThinkTreeDutyHook dutyHook;

        // Token: 0x060008B4 RID: 2228 RVA: 0x00046A24 File Offset: 0x00044E24
        public override ThinkNode DeepCopy(bool resolve = true)
        {
            var thinkNode_JoinVoluntarilyJoinableLord = (ThinkNode_JoinVoluntarilyJoinableLord)base.DeepCopy(resolve);
            thinkNode_JoinVoluntarilyJoinableLord.dutyHook = dutyHook;
            return thinkNode_JoinVoluntarilyJoinableLord;
        }

        // Token: 0x060008B5 RID: 2229 RVA: 0x00046A4C File Offset: 0x00044E4C
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

        // Token: 0x060008B6 RID: 2230 RVA: 0x00046AB0 File Offset: 0x00044EB0
        private void CheckLeaveCurrentVoluntarilyJoinableLord(Pawn pawn)
        {
            var lord = pawn.GetLord();
            if (lord == null)
            {
                return;
            }

            if (lord.LordJob is not LordJob_VoluntarilyJoinable lordJob_VoluntarilyJoinable)
            {
                return;
            }

            if (lordJob_VoluntarilyJoinable.VoluntaryJoinPriorityFor(pawn) <= 0f)
            {
                lord.Notify_PawnLost(pawn, PawnLostCondition.LeftVoluntarily);
            }
        }

        // Token: 0x060008B7 RID: 2231 RVA: 0x00046AF8 File Offset: 0x00044EF8
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

            if (lord != null)
            {
                lord.Notify_PawnLost(pawn, PawnLostCondition.LeftVoluntarily);
            }

            lord2.AddPawn(pawn);
        }
    }
}