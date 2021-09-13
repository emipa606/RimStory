using Verse;

namespace RimStory
{
    internal class RimStorySettings : ModSettings
    {
        public bool enableAll = true;
        public bool enableDaysOfVictory = true;
        public bool enableFunerals = true;
        public bool enableIndividualThoughts = true;
        public bool enableLogging = true;
        public bool enableMarriageAnniversary = true;
        public bool enableMemoryDay = true;
        public bool ISLOGGONNARESET = false;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref enableFunerals, "enableFunerals", true);
            Scribe_Values.Look(ref enableMemoryDay, "enableMemoryDay", true);
            Scribe_Values.Look(ref enableMarriageAnniversary, "enableMarriageAnniversary", true);
            Scribe_Values.Look(ref enableIndividualThoughts, "enableIndividualThoughts", true);
            Scribe_Values.Look(ref enableDaysOfVictory, "enableDaysOfVictory", true);
            Scribe_Values.Look(ref enableLogging, "enableLogging", true);
        }
    }
}