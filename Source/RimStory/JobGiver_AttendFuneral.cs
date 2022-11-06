using RimWorld;
using Verse;
using Verse.AI;

namespace RimStory;

internal class JobGiver_AttendFuneral : ThinkNode_JobGiver
{
    protected override Job TryGiveJob(Pawn pawn)
    {
        var grave = FindGrave();
        if (grave == null)
        {
            return null;
        }

        return new Job(RS_JobDefOf.AttendFuneral, grave);
    }


    private Building_Grave FindGrave()
    {
        return Resources.lastGrave;
    }


    public class RS_JobDefOf
    {
        public static JobDef AttendFuneral;
    }
}