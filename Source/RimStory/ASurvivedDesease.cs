using System;
using Verse;

namespace RimStory;

internal class ASurvivedDesease : IEvent
{
    private Date date;
    private Pawn pawn;

    public ASurvivedDesease()
    {
    }

    public ASurvivedDesease(Date date, Pawn pawn, Hediff hediff)
    {
        this.date = date;
        this.pawn = pawn;
    }

    public Date Date()
    {
        throw new NotImplementedException();
    }

    public void EndEvent()
    {
        throw new NotImplementedException();
    }

    public void ExposeData()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public bool TryStartEvent()
    {
        throw new NotImplementedException();
    }

    public bool TryStartEvent(Map map)
    {
        return false;
    }
}