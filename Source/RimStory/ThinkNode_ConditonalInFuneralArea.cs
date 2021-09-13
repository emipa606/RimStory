using RimWorld;
using Verse;
using Verse.AI;

namespace RimStory
{
    internal class ThinkNode_ConditonalInFuneralArea : ThinkNode_Conditional
    {
        // Token: 0x06000906 RID: 2310 RVA: 0x000474A0 File Offset: 0x000458A0
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
}