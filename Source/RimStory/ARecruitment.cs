using System;
using Verse;

namespace RimStory;

internal class ARecruitment : IEvent
{
    private Date date;
    private Pawn recruitee;
    private Pawn recruiter;

    public ARecruitment()
    {
    }


    public ARecruitment(Date date, Pawn recruiter, Pawn recruitee)
    {
        this.date = date;
        this.recruiter = recruiter;
        this.recruitee = recruitee;
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
        Scribe_References.Look(ref recruiter, "RS_Recruiter", true);
        Scribe_References.Look(ref recruitee, "RS_Recruitee", true);
        Scribe_Deep.Look(ref date, "RS_DateRecruited");
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
        if (recruitee == null)
        {
            return null;
        }

        if (!recruitee.NonHumanlikeOrWildMan())
        {
            return
                $"{date.day} {date.quadrum} {date.year} {"ARecruitment".Translate(recruiter.Name, recruitee.Name)}";
        }

        if (recruitee.NonHumanlikeOrWildMan())
        {
            return $"{date.day} {date.quadrum} {date.year} {"ATamed".Translate(recruiter.Name, recruitee.Name)}";
        }

        return null;
    }

    public bool TryStartEvent()
    {
        throw new NotImplementedException();
    }

    public bool TryStartEvent(Map map)
    {
        throw new NotImplementedException();
    }
}