using System.Collections.Generic;
using Verse.AI;

namespace RimStory
{
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


            //yield return Toils_Interpersonal.WaitToBeAbleToInteract(this.pawn);
            //yield return Toils_Interpersonal.GotoInteractablePosition(graveToVisit);

            //Toil gotoTarget = Toils_Goto.GotoThing(graveToVisit, PathEndMode.Touch);
            //gotoTarget.socialMode = RandomSocialMode.Off;

            //Toil wait = Toils_General.WaitWith(graveToVisit, 4000, false, true);
            //Toil wait = Toils_General.Wait(4000);
            //wait.socialMode = RandomSocialMode.Off;
            //yield return Toils_General.Do(delegate
            //{
            //    Pawn petter = this.pawn;
            //    Building_Grave pettee = (Building_Grave)this.pawn.CurJob.targetA.Thing;
            //    //pettee.interactions.TryInteractWith(petter, InteractionDefOf.Nuzzle);
            //});
        }
    }
}