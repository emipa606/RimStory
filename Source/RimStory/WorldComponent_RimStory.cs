using RimWorld;
using RimWorld.Planet;
using Verse;

namespace RimStory;

public class WorldComponent_RimStory : WorldComponent
{
    public WorldComponent_RimStory(World world) : base(world)
    {
    }

    //addresses WorldLoaded
    public override void FinalizeInit(bool fromLoad)
    {
        base.FinalizeInit(fromLoad);
        _ = Find.World.GetComponent<Saves>();
    }

    //addresses custom tick stuff
    public override void WorldComponentTick()
    {
        if (RimStoryMod.settings.ISLOGGONNARESET)
        {
            Resources.eventsLog.Clear();
            RimStoryMod.settings.ISLOGGONNARESET = false;
        }
    }
}
