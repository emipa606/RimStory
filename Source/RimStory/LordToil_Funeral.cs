using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace RimStory
{
    internal class LordToil_Funeral : LordToil
    {
        // Token: 0x0400038E RID: 910
        private const int DefaultTicksPerPartyPulse = 600;

        // Token: 0x0400038C RID: 908
        private readonly IntVec3 spot;

        // Token: 0x0400038D RID: 909
        private readonly int ticksPerPartyPulse = 600;

        // Token: 0x0600083F RID: 2111 RVA: 0x000439A9 File Offset: 0x00041DA9
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

        // Token: 0x1700014B RID: 331
        // (get) Token: 0x06000840 RID: 2112 RVA: 0x000439E1 File Offset: 0x00041DE1
        private LordToilData_Gathering Data => (LordToilData_Gathering)data;

        // Token: 0x06000841 RID: 2113 RVA: 0x000439EE File Offset: 0x00041DEE
        public override ThinkTreeDutyHook VoluntaryJoinDutyHookFor(Pawn p)
        {
            return DefDatabase<DutyDef>.GetNamed("Funeral").hook;
        }

        // Token: 0x06000842 RID: 2114 RVA: 0x000439FC File Offset: 0x00041DFC
        public override void UpdateAllDuties()
        {
            foreach (var pawn in lord.ownedPawns)
            {
                pawn.mindState.duty = new PawnDuty(DutyDefOf.Party, spot);
            }
        }

        // Token: 0x06000843 RID: 2115 RVA: 0x00043A60 File Offset: 0x00041E60
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
}