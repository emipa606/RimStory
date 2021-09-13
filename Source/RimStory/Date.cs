using RimWorld;
using Verse;

namespace RimStory
{
    public class Date : IExposable
    {
        public int day;
        public Quadrum quadrum;
        public int year;

        public Date()
        {
        }


        public Date(int day, Quadrum quadrum, int year)
        {
            this.day = day;
            this.quadrum = quadrum;
            this.year = year;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref day, "RS_DateDay");
            Scribe_Values.Look(ref quadrum, "RS_DateQuadrum", Quadrum.Undefined);
            Scribe_Values.Look(ref year, "RS_DateYear");
        }

        public Date GetDate()
        {
            return this;
        }

        public override string ToString()
        {
            return $"{day} {quadrum} {year}";
        }
    }
}