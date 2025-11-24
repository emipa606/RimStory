using Verse;


namespace RimStory;

public class GameComponent_RimStory : GameComponent
{
    //replicates the static list clearing code
    public GameComponent_RimStory(Game game) : base()
    {
    }


    private void ClearStaticLists()
    {
        Resources.events.Clear();
        Resources.eventsLog.Clear();
    }

    public override void LoadedGame() { ClearStaticLists(); }

    public override void StartedNewGame() { ClearStaticLists(); }
}
