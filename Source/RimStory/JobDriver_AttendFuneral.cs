using System.Collections.Generic;
using Verse.AI;

namespace RimStory;

internal class JobDriver_AttendFuneral : JobDriver
{
    private readonly TargetIndex graveToVisit = TargetIndex.A;

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return true;
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        this.FailOnDespawnedNullOrForbidden(graveToVisit);


        yield return Toils_Goto.GotoThing(graveToVisit, PathEndMode.Touch);

        yield return Toils_General.Wait(1000);
    }
}