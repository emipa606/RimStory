using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace RimStory;

internal class LordToil_Funeral : LordToil
{
    private const int DefaultTicksPerPartyPulse = 600;

    private readonly IntVec3 spot;

    private readonly int ticksPerPartyPulse = 600;

    public LordToil_Funeral(IntVec3 spot, int ticksPerPartyPulse = 600)
    {
        this.spot = spot;
        this.ticksPerPartyPulse = ticksPerPartyPulse;
        data = new LordToilData_Gathering();
        //this.Data.ticksToNextPulse = ticksPerPartyPulse;

        foreach (var pawn in lord.ownedPawns)
        {
            Data.presentForTicks[pawn] = ticksPerPartyPulse;
        }
        //TODO correct?
    }

    private LordToilData_Gathering Data => (LordToilData_Gathering)data;

    public override ThinkTreeDutyHook VoluntaryJoinDutyHookFor(Pawn p)
    {
        return DefDatabase<DutyDef>.GetNamed("Funeral").hook;
    }

    public override void UpdateAllDuties()
    {
        foreach (var pawn in lord.ownedPawns)
        {
            pawn.mindState.duty = new PawnDuty(DefDatabase<DutyDef>.GetNamedSilentFail("Party"), spot);
        }
    }

    public override void LordToilTick()
    {
        //TODO test

        //if (--this.Data.ticksToNextPulse <= 0)
        {
            //this.Data.ticksToNextPulse = this.ticksPerPartyPulse;

            var ownedPawns = lord.ownedPawns;
            foreach (var pawn in ownedPawns)
            {
                //
                if (--Data.presentForTicks[pawn] > 0)
                {
                    continue;
                }

                Data.presentForTicks[pawn] = ticksPerPartyPulse;
                //

                //if (PartyUtility.InPartyArea(ownedPawns[i].Position, this.spot, base.Map))
                if (!GatheringsUtility.InGatheringArea(pawn.Position, spot, Map))
                {
                    continue;
                }

                //ownedPawns[i].needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.AttendedParty, null);
                if (lord.LordJob is LordJob_RimStory lordJob_Joinable_Party)
                {
                    TaleRecorder.RecordTale(TaleDefOf.AttendedParty, pawn,
                        lordJob_Joinable_Party.Organizer);
                }
            }
        }
    }
}