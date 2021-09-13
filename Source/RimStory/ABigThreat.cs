using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace RimStory
{
    internal class ABigThreat : IEvent
    {
        private bool anniversary = true;
        private Date date;
        private Faction faction;
        private List<int> yearsWhenEventStarted = new List<int>();

        public ABigThreat()
        {
        }

        public ABigThreat(Date date, Faction faction)
        {
            this.date = date;
            this.faction = faction;
        }

        public Date Date()
        {
            return date;
        }

        public void EndEvent()
        {
            throw new NotImplementedException();
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref anniversary, "ff", true);
            Scribe_Collections.Look(ref yearsWhenEventStarted, "RS_yearsWhenEventStarted", LookMode.Value);
            Scribe_References.Look(ref faction, "RS_FactionAttacked");
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
            if (faction != null && date != null)
            {
                return $"{date.day} {date.quadrum} {date.year} " + "ColonyAttacked".Translate(faction.Name);
            }

            if (faction == null && date != null)
            {
                return $"{date.day} {date.quadrum} {date.year} Your colony was raided.";
            }

            return "Your colony was raided.";
        }

        public bool TryStartEvent()
        {
            //throw new NotImplementedException();
            return false;
        }

        public bool TryStartEvent(Map map)
        {
            if (faction == null)
            {
                return false;
            }

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
            var pawn = GatheringsUtility.FindRandomGatheringOrganizer(Faction.OfPlayer, map,
                GatheringDefOf.Party);
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

            yearsWhenEventStarted.Add(Utils.CurrentYear());
            //Lord lord = LordMaker.MakeNewLord(pawn.Faction, new LordJob_Joinable_Party(intVec, pawn), map, null);
            var unused = LordMaker.MakeNewLord(pawn.Faction,
                new LordJob_Joinable_Party(intVec, pawn, GatheringDefOf.Party), map);

            //Find.LetterStack.ReceiveLetter("Day of "+faction.Name+" defeat", "Your colonists are celebrating " + faction.Name + "'s defeat on \n" + date, LetterDefOf.PositiveEvent);
            //Find.LetterStack.ReceiveLetter("DayOfVictory".Translate(faction.Name), "DayOfVictoryDesc".Translate(new object[] { faction.Name, date }), LetterDefOf.PositiveEvent);
            string label = "DayOfVictory".Translate(faction.Name);
            string text = "DayOfVictoryDesc".Translate(faction.Name, date.ToString());
            Find.LetterStack.ReceiveLetter(label, text, LetterDefOf.PositiveEvent);
            return true;
        }

        private void AddAttendedMemorialDay()
        {
        }
    }
}