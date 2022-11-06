using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace RimStory;

internal class AMemorialDay : IEvent
{
    private bool anniversary = true;
    private Date date;
    private Pawn deadPawn;
    private List<int> yearsWhenEventStarted = new List<int>();

    public AMemorialDay()
    {
    }

    public AMemorialDay(Date date, Pawn deadPawn)
    {
        this.date = date;
        this.deadPawn = deadPawn;
    }

    public Date Date()
    {
        return date;
    }

    public void EndEvent()
    {
    }

    public void ExposeData()
    {
        Scribe_Values.Look(ref anniversary, "RS_Anniversary", true);
        Scribe_References.Look(ref deadPawn, "RS_DeadPawn", true);
        Scribe_Collections.Look(ref yearsWhenEventStarted, "RS_yearsWhenEventStarted", LookMode.Value);
        Scribe_Deep.Look(ref date, "RS_DateAttacked");
    }

    public bool GetIsAnniversary()
    {
        return anniversary;
    }

    public bool IsStillEvent()
    {
        throw new NotImplementedException();
    }

    public string ShowInLog()
    {
        if (deadPawn != null)
        {
            //return (date.day + " " + date.quadrum + " " + date.year + " " + "AMemorialDay".Translate(deadPawn.Name));
            return $"{date.day} {date.quadrum} {date.year} " +
                   "AMemorialDay".Translate(deadPawn.Name.ToString());
        }

        return $"{date.day} {date.quadrum} {date.year} colonist died";
        //return (date.day + " " + date.quadrum + " " + date.year + " " + deadPawn.Name + " died.");
    }

    public bool TryStartEvent()
    {
        throw new NotImplementedException();
    }

    public bool TryStartEvent(Map map)
    {
        var isThisYear = true;
        foreach (var y in yearsWhenEventStarted)
        {
            if (y == Utils.CurrentYear())
            {
                isThisYear = false;
            }
        }


        if (Utils.CurrentDay() != date.day || Utils.CurrentQuadrum() != date.quadrum ||
            Utils.CurrentHour() < Resources.minHour || Utils.CurrentHour() > Resources.maxHour ||
            Utils.CurrentYear() == date.year || !isThisYear)
        {
            return false;
        }

        //Pawn pawn = PartyUtility.FindRandomPartyOrganizer(Faction.OfPlayer, map);
        var pawn = GatheringsUtility.FindRandomGatheringOrganizer(Faction.OfPlayer, map, GatheringDefOf.Party);

        if (pawn == null)
        {
            return false;
        }

        //if (!RCellFinder.TryFindPartySpot(pawn, out intVec))
        if (!RCellFinder.TryFindGatheringSpot(pawn, GatheringDefOf.Party, true, out var intVec))
        {
            return false;
        }

        yearsWhenEventStarted.Add(Utils.CurrentYear());
        //Lord lord = LordMaker.MakeNewLord(pawn.Faction, new LordJob_Joinable_Party(intVec, pawn), map, null);
        var unused = LordMaker.MakeNewLord(pawn.Faction,
            new LordJob_Joinable_Party(intVec, pawn, GatheringDefOf.Party), map);

        //Find.LetterStack.ReceiveLetter("Memorial Day", "Colonist are gathering to honor fallen colonists.", LetterDefOf.PositiveEvent);
        Find.LetterStack.ReceiveLetter("AMemorialDayLetter".Translate(), "AMemorialDayDesc".Translate(),
            LetterDefOf.PositiveEvent);
        return true;
    }

    private void AddAttendedMemorialDay(Pawn pawn)
    {
        pawn.needs.mood.thoughts.memories.TryGainMemory(Thoughts.RS_AttendedMemorialDay);
    }
}