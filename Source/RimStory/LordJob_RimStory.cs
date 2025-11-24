using RimWorld;
using Verse;
using Verse.AI.Group;

namespace RimStory;

internal class LordJob_RimStory : LordJob_VoluntarilyJoinable
{
    private Pawn organizer;

    private IntVec3 spot;

    private Trigger_TicksPassed timeoutTrigger;

    public LordJob_RimStory()
    {
    }

    public LordJob_RimStory(IntVec3 spot, Pawn organizer)
    {
        //this.spot = spot;
        this.spot = Resources.lastGrave.Position;
        this.organizer = organizer;
    }

    private bool IsInvited(Pawn p)
    {
        return lord.faction != null && p.Faction == lord.faction;
    }

    private bool IsPartyAboutToEnd()
    {
        return timeoutTrigger.TicksLeft < 1200;
    }

    private bool ShouldBeCalledOff()
    {
        //return !PartyUtility.AcceptableGameConditionsToContinueParty(base.Map) || (!this.spot.Roofed(base.Map) && !JoyUtility.EnjoyableOutsideNow(base.Map, null));
        return !GatheringsUtility.AcceptableGameConditionsToContinueGathering(Map) ||
               !spot.Roofed(Map) && !JoyUtility.EnjoyableOutsideNow(Map);
    }

    public override StateGraph CreateGraph()
    {
        var stateGraph = new StateGraph();

        var lordToil_Party = new LordToil_Funeral(spot);
        stateGraph.AddToil(lordToil_Party);

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

    public override void ExposeData()
    {
        Scribe_Values.Look(ref spot, "spot");
        Scribe_References.Look(ref organizer, "organizer");
    }

    public override string GetReport(Pawn pawn)
    {
        return "Attending funeral";
    }

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

    public override bool AllowStartNewGatherings => false;

    public Pawn Organizer => organizer;
}