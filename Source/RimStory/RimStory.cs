using HugsLib;
using UnityEngine.SceneManagement;
using Verse;

namespace RimStory;

public class RimStory : ModBase
{
    public override string ModIdentifier => "RimStory";

    public override void MapLoaded(Map map)
    {
        Resources.TEST_MAP = map;

        base.MapLoaded(map);


        ///////////////////////////////////// One tick per hour \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        HugsLibController.Instance.TickDelayScheduler.ScheduleCallback(() =>
        {
            if (Resources.events.Count > 0)
            {
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
            }

            foreach (var e in Resources.eventsToDelete)
            {
                Resources.events.Remove(e);
            }

            Resources.eventsToDelete.Clear();
        }, 2500, null, true);

        ///////////////////////////////////// Mass funeral \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\   
        HugsLibController.Instance.TickDelayScheduler.ScheduleCallback(() =>
        {
            if (Resources.deadPawnsForMassFuneralBuried.Count <= 0 || !RimStoryMod.settings.enableFunerals)
            {
                return;
            }

            if (Resources.dateLastFuneral == null ||
                Utils.CurrentDay() != Resources.dateLastFuneral.GetDate().day &&
                Utils.CurrentQuadrum() != Resources.dateLastFuneral.GetDate().quadrum &&
                Utils.CurrentYear() != Resources.dateLastFuneral.GetDate().year)
            {
            }

            if (!MassFuneral.TryStartMassFuneral(map))
            {
                return;
            }

            Resources.deadPawnsForMassFuneralBuried.Clear();
            Resources.dateLastFuneral = Utils.CurrentDate();
        }, 2500, null, true);
    }

    public override void SceneLoaded(Scene scene)
    {
        ///////////////////////////////////// Dirty hacks for deleting static lists. Don't look. \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ 

        base.SceneLoaded(scene);
        if (!GenScene.InEntryScene)
        {
            return;
        }

        Resources.eventsLog.Clear();
        Resources.events.Clear();
    }

    public override void Tick(int currentTick)
    {
        base.Tick(currentTick);
        if (!RimStoryMod.settings.ISLOGGONNARESET)
        {
            return;
        }

        Resources.eventsLog.Clear();
        RimStoryMod.settings.ISLOGGONNARESET = false;
    }

    public override void WorldLoaded()
    {
        base.WorldLoaded();
        //var obj = UtilityWorldObjectManager.GetUtilityWorldObject<Saves>();
        _ = Find.World.GetComponent<Saves>();
    }
}