using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace RimStory
{
    internal class AMarriage : IEvent
    {
        private bool anniversary = true;
        private Date date;
        private Pawn pawn1, pawn2;
        private List<int> yearsWhenEventStarted = new List<int>();

        public AMarriage()
        {
        }


        public AMarriage(Date date, Pawn pawn1, Pawn pawn2)
        {
            this.date = date;
            this.pawn1 = pawn1;
            this.pawn2 = pawn2;
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
            Scribe_Values.Look(ref anniversary, "RS_Anniversary", true);
            Scribe_References.Look(ref pawn1, "RS_DeadPawn1");
            Scribe_References.Look(ref pawn2, "RS_DeadPawn2");
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
            //return (date.day + " " + date.quadrum + " " + date.year + " " + pawn1.Name + " and " + pawn2.Name+" married.");
            //return (date.day + " " + date.quadrum + " " + date.year + " " + "AMarriage".Translate(new object[] {pawn1, pawn2}));
            return $"{date.day} {date.quadrum} {date.year} " + "AMarriage".Translate(pawn1, pawn2);
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

            if (pawn1 == null)
            {
                return false;
            }

            if (pawn1.GetFirstSpouse() != pawn2)
            {
                return false;
            }

            //if (!RCellFinder.TryFindPartySpot(pawn1, out intVec))
            if (!RCellFinder.TryFindMarriageSite(pawn1, pawn2, out var intVec))
            {
                return false;
            }

            yearsWhenEventStarted.Add(Utils.CurrentYear());

            //Lord lord = LordMaker.MakeNewLord(pawn1.Faction, new LordJob_Joinable_Party(intVec, pawn1), map, null);
            var unused = LordMaker.MakeNewLord(pawn1.Faction,
                new LordJob_Joinable_Party(intVec, pawn1, GatheringDefOf.Party), map);

            //Find.LetterStack.ReceiveLetter("Marriage anniversary", pawn1.LabelShort + " and " + pawn2.LabelShort + " anniversary.", LetterDefOf.PositiveEvent);
            Find.LetterStack.ReceiveLetter("AMarriageLetter".Translate(),
                "AMarriageDesc".Translate(pawn1.LabelShort, pawn2.LabelShort),
                LetterDefOf.PositiveEvent);


            foreach (var p in pawn1.Map.mapPawns.FreeColonists)
            {
                if (p == pawn1 || p == pawn2)
                {
                    if (p.GetFirstSpouse() == pawn1 || p.GetFirstSpouse() == pawn2)
                    {
                        AddAttendedOurAnniversaryThoughts(p);
                    }

                    else
                    {
                        anniversary = false;
                        return false;
                    }
                }
                else
                {
                    AddAttendedAnniversaryThoughts(p);
                }
            }

            return true;
        }

        private void AddAttendedAnniversaryThoughts(Pawn pawn)
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(Thoughts.RS_AttendedAnniversary);
        }

        private void AddAttendedOurAnniversaryThoughts(Pawn pawn)
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(Thoughts.RS_AttendedOurAnniversary);
        }
    }
}