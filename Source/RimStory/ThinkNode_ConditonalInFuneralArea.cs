using RimWorld;
using Verse;
using Verse.AI;

namespace RimStory;

internal class ThinkNode_ConditonalInFuneralArea : ThinkNode_Conditional
{
    protected override bool Satisfied(Pawn pawn)
    {
        if (pawn.mindState.duty == null)
        {
            return false;
        }

        var cell = pawn.mindState.duty.focus.Cell;
        //return PartyUtility.InPartyArea(pawn.Position, cell, pawn.Map);
        return GatheringsUtility.InGatheringArea(pawn.Position, cell, pawn.Map);
    }
}