using Verse;


namespace RimStory
{
    public class GameComponent_RimStory : GameComponent
    {
        //replicates the static list clearing code
        public GameComponent_RimStory(Game game) : base() { }

        public override void StartedNewGame()
        {            
            ClearStaticLists();
        }

        public override void LoadedGame()
        {           
            ClearStaticLists();
        }      
      

        private void ClearStaticLists()
        {
            Resources.events.Clear();
            Resources.eventsLog.Clear();
        }
    }
}
