using RimWorld;
using Verse;
using Verse.AI;

namespace RimStory;

internal class JobGiver_AttendFuneral : ThinkNode_JobGiver
{


    private Building_Grave FindGrave()
    {
        return Resources.lastGrave;
    }

    protected override Job TryGiveJob(Pawn pawn)
    {
        var grave = FindGrave();
        return grave == null ? null : new Job(RS_JobDefOf.AttendFuneral, grave);
    }


    public class RS_JobDefOf
    {
        public static JobDef AttendFuneral;
    }
}