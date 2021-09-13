using Verse;

namespace RimStory
{
    public interface IEvent : IExposable
    {
        bool GetIsAnniversary();

        bool TryStartEvent();
        bool TryStartEvent(Map map);
        bool IsStillEvent();
        void EndEvent();
        string ShowInLog();
        Date Date();
    }
}