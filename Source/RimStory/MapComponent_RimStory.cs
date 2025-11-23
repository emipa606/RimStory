using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RimStory
{
    public class MapComponent_RimStory : MapComponent
    {
        private int tickCounter = 0;

        public MapComponent_RimStory(Map map) : base(map)
        {
            // Equivalent of HugsLib's MapLoaded
            Resources.TEST_MAP = map;
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();

            tickCounter++;

            // Run roughly once per in-game hour (~2500 ticks)
            if (tickCounter % 2500 == 0)
            {
                RunScheduledEvents();
                RunMassFuneral();
            }
        }

        private void RunScheduledEvents()
        {
            if (Resources.events.Count == 0) return;

            foreach (var e in Resources.events)
            {
                switch (e)
                {
                    case AMarriage when RimStoryMod.settings.enableMarriageAnniversary:
                    case AMemorialDay when RimStoryMod.settings.enableMemoryDay:
                    case ABigThreat when RimStoryMod.settings.enableDaysOfVictory:
                    case ADead when RimStoryMod.settings.enableIndividualThoughts:
                        e.TryStartEvent(map);
                        break;
                }
            }

            foreach (var e in Resources.eventsToDelete)
            {
                Resources.events.Remove(e);
            }

            Resources.eventsToDelete.Clear();
        }

        private void RunMassFuneral()
        {
            if (!RimStoryMod.settings.enableFunerals || Resources.deadPawnsForMassFuneralBuried.Count == 0)
                return;

            var lastDate = Resources.dateLastFuneral?.GetDate();
            var now = Utils.CurrentDate();

            // Skip if a funeral has already happened today
            if (lastDate != null &&
                lastDate.day == now.day &&
                lastDate.quadrum == now.quadrum &&
                lastDate.year == now.year)
            {
                return;
            }

            if (!MassFuneral.TryStartMassFuneral(map))
                return;

            Resources.deadPawnsForMassFuneralBuried.Clear();
            Resources.dateLastFuneral = now;
        }
    }
}
