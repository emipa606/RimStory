using System;
using System.Collections.Generic;
using Verse;

namespace RimStory
{
    internal class ABonded : IEvent
    {
        private Date date;
        private Pawn pawn, animal;

        private List<int> yearsWhenEventStarted = new List<int>();

        public ABonded()
        {
        }

        public ABonded(Date date, Pawn pawn, Pawn animal)
        {
            this.date = date;
            this.pawn = pawn;
            this.animal = animal;
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
            Scribe_References.Look(ref pawn, "RS_Trainer");
            Scribe_References.Look(ref animal, "RS_PetBonded");
            Scribe_Collections.Look(ref yearsWhenEventStarted, "RS_yearsWhenEventStarted", LookMode.Value);
            Scribe_Deep.Look(ref date, "RS_DateBonded");
        }

        public bool GetIsAnniversary()
        {
            throw new NotImplementedException();
        }

        public bool IsStillEvent()
        {
            throw new NotImplementedException();
        }

        public string ShowInLog()
        {
            if (pawn != null && animal != null)
            {
                //return (date.day + " " + date.quadrum + " " + date.year + " " + "ABondedAnimal".Translate(new object[] { pawn.Name, animal.Name }));
                return $"{date.day} {date.quadrum} {date.year} " +
                       "ABondedAnimal".Translate(pawn.Name.ToString(), animal.Name.ToString());
            }

            return $"{date.day} {date.quadrum} {date.year} bond with animal.";
        }

        public bool TryStartEvent()
        {
            return false;
        }

        public bool TryStartEvent(Map map)
        {
            return false;
        }
    }
}