using RimWorld;
using Verse;
using Verse.AI.Group;

namespace RimStory
{
    internal class LordJob_RimStory : LordJob_VoluntarilyJoinable
    {
        // Token: 0x0400034E RID: 846
        private Pawn organizer;

        // Token: 0x0400034D RID: 845
        private IntVec3 spot;

        // Token: 0x0400034F RID: 847
        private Trigger_TicksPassed timeoutTrigger;

        // Token: 0x06000789 RID: 1929 RVA: 0x0003FC21 File Offset: 0x0003E021
        public LordJob_RimStory()
        {
        }

        // Token: 0x0600078A RID: 1930 RVA: 0x0003FC29 File Offset: 0x0003E029
        public LordJob_RimStory(IntVec3 spot, Pawn organizer)
        {
            //this.spot = spot;
            this.spot = Resources.lastGrave.Position;
            this.organizer = organizer;
        }

        // Token: 0x17000124 RID: 292
        // (get) Token: 0x0600078B RID: 1931 RVA: 0x0003FC3F File Offset: 0x0003E03F
        public override bool AllowStartNewGatherings => false;

        // Token: 0x17000125 RID: 293
        // (get) Token: 0x0600078C RID: 1932 RVA: 0x0003FC42 File Offset: 0x0003E042
        public Pawn Organizer => organizer;

        // Token: 0x0600078D RID: 1933 RVA: 0x0003FC4C File Offset: 0x0003E04C
        public override StateGraph CreateGraph()
        {
            //StateGraph stateGraph = new StateGraph();
            //LordToil_Gathering lordToil_Party = new LordToil_Gathering(this.spot, 600);
            //stateGraph.AddToil(lordToil_Party);

            //LordToil_End lordToil_End = new LordToil_End();
            //stateGraph.AddToil(lordToil_End);

            //Transition transition = new Transition(lordToil_Party, lordToil_End);

            //transition.AddTrigger(new Trigger_TickCondition(() => this.ShouldBeCalledOff(), 1));
            //transition.AddTrigger(new Trigger_PawnLostViolently());
            //transition.AddPreAction(new TransitionAction_Message("Funeral called off.", MessageTypeDefOf.NegativeEvent, new TargetInfo(this.spot, base.Map, false)));
            //stateGraph.AddTransition(transition);
            //this.timeoutTrigger = new Trigger_TicksPassed(Rand.RangeInclusive(5000, 15000));
            //Transition transition2 = new Transition(lordToil_Party, lordToil_End);
            //transition2.AddTrigger(this.timeoutTrigger);
            //transition2.AddPreAction(new TransitionAction_Message("Funeral finished.", MessageTypeDefOf.SituationResolved, new TargetInfo(this.spot, base.Map, false)));
            //stateGraph.AddTransition(transition2);
            //return stateGraph;

            var stateGraph = new StateGraph();

            var lordToil_Party = new LordToil_Funeral(spot);
            stateGraph.AddToil(lordToil_Party);

            //LordToil_MarriageCeremony lordToil_MarriageCeremony = new LordToil_MarriageCeremony(this.organizer, this.organizer, this.spot);
            //stateGraph.AddToil(lordToil_MarriageCeremony);

            var lordToil_End = new LordToil_EndGathering();
            stateGraph.AddToil(lordToil_End);

            var transition = new Transition(lordToil_Party, lordToil_End);

            transition.AddTrigger(new Trigger_TickCondition(ShouldBeCalledOff));
            transition.AddTrigger(new Trigger_PawnLostViolently());
            //transition.AddPreAction(new TransitionAction_Message("Funeral called off.", MessageTypeDefOf.NegativeEvent, new TargetInfo(this.spot, base.Map, false)));
            transition.AddPreAction(new TransitionAction_Message("FuneralCalledOff".Translate(),
                MessageTypeDefOf.NegativeEvent, new TargetInfo(spot, Map)));
            stateGraph.AddTransition(transition);
            timeoutTrigger = new Trigger_TicksPassed(Rand.RangeInclusive(5000, 15000));
            var transition2 = new Transition(lordToil_Party, lordToil_End);
            transition2.AddTrigger(timeoutTrigger);
            transition2.AddPreAction(new TransitionAction_Message("FuneralFinished".Translate(),
                MessageTypeDefOf.SituationResolved, new TargetInfo(spot, Map)));
            //transition2.AddPreAction(new TransitionAction_Message("Funeral finished.", MessageTypeDefOf.SituationResolved, new TargetInfo(this.spot, base.Map, false)));
            stateGraph.AddTransition(transition2);
            return stateGraph;
        }

        // Token: 0x0600078E RID: 1934 RVA: 0x0003FD48 File Offset: 0x0003E148
        private bool ShouldBeCalledOff()
        {
            //return !PartyUtility.AcceptableGameConditionsToContinueParty(base.Map) || (!this.spot.Roofed(base.Map) && !JoyUtility.EnjoyableOutsideNow(base.Map, null));
            return !GatheringsUtility.AcceptableGameConditionsToContinueGathering(Map) ||
                   !spot.Roofed(Map) && !JoyUtility.EnjoyableOutsideNow(Map);
        }

        // Token: 0x0600078F RID: 1935 RVA: 0x0003FD88 File Offset: 0x0003E188
        public override float VoluntaryJoinPriorityFor(Pawn p)
        {
            if (!IsInvited(p))
            {
                return 0f;
            }

            //if (!PartyUtility.ShouldPawnKeepPartying(p))
            if (GatheringsUtility.ShouldPawnKeepGathering(p, GatheringDefOf.Party))
            {
                return 0f;
            }

            if (spot.IsForbidden(p))
            {
                return 0f;
            }

            if (!lord.ownedPawns.Contains(p) && IsPartyAboutToEnd())
            {
                return 0f;
            }

            //return VoluntarilyJoinableLordJobJoinPriorities.PartyGuest;
            return VoluntarilyJoinableLordJobJoinPriorities.SocialGathering;
        }

        // Token: 0x06000790 RID: 1936 RVA: 0x0003FDFC File Offset: 0x0003E1FC
        public override void ExposeData()
        {
            Scribe_Values.Look(ref spot, "spot");
            Scribe_References.Look(ref organizer, "organizer");
        }

        // Token: 0x06000791 RID: 1937 RVA: 0x0003FE34 File Offset: 0x0003E234
        public override string GetReport(Pawn pawn)
        {
            return "Attending funeral";
        }

        // Token: 0x06000792 RID: 1938 RVA: 0x0003FE40 File Offset: 0x0003E240
        private bool IsPartyAboutToEnd()
        {
            return timeoutTrigger.TicksLeft < 1200;
        }

        // Token: 0x06000793 RID: 1939 RVA: 0x0003FE5A File Offset: 0x0003E25A
        private bool IsInvited(Pawn p)
        {
            return lord.faction != null && p.Faction == lord.faction;
        }
    }
}