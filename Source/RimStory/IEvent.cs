using Verse;

namespace RimStory;

public interface IEvent : IExposable
{
    Date Date();
    void EndEvent();
    bool GetIsAnniversary();
    bool IsStillEvent();
    string ShowInLog();

    bool TryStartEvent();
    bool TryStartEvent(Map map);
}