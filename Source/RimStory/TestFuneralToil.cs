using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace RimStory
{
    internal class TestFuneralToil : LordToil
    {
        // Token: 0x04000388 RID: 904
        public static readonly IntVec3 OtherFianceNoMarriageSpotCellOffset = new IntVec3(-1, 0, 0);

        // Token: 0x04000385 RID: 901
        private readonly Pawn firstPawn;

        // Token: 0x04000386 RID: 902
        private readonly Pawn secondPawn;

        // Token: 0x04000387 RID: 903
        private readonly IntVec3 spot;

        // Token: 0x06000831 RID: 2097 RVA: 0x000435FB File Offset: 0x000419FB
        public TestFuneralToil(Pawn firstPawn, Pawn secondPawn, IntVec3 spot)
        {
            this.firstPawn = firstPawn;
            this.secondPawn = secondPawn;
            this.spot = spot;
            data = new LordToilData_MarriageCeremony();
        }

        // Token: 0x1700014A RID: 330
        // (get) Token: 0x06000832 RID: 2098 RVA: 0x00043623 File Offset: 0x00041A23
        public LordToilData_MarriageCeremony Data => (LordToilData_MarriageCeremony)data;

        // Token: 0x06000833 RID: 2099 RVA: 0x00043630 File Offset: 0x00041A30
        public override void Init()
        {
            base.Init();
            Data.spectateRect = CalculateSpectateRect();
            var allowedSides = SpectateRectSide.All;
            if (Data.spectateRect.Width > Data.spectateRect.Height)
            {
                allowedSides = SpectateRectSide.Vertical;
            }
            else if (Data.spectateRect.Height > Data.spectateRect.Width)
            {
                allowedSides = SpectateRectSide.Horizontal;
            }

            Data.spectateRectAllowedSides =
                SpectatorCellFinder.FindSingleBestSide(Data.spectateRect, Map, allowedSides);
        }

        // Token: 0x06000834 RID: 2100 RVA: 0x000436CE File Offset: 0x00041ACE
        public override ThinkTreeDutyHook VoluntaryJoinDutyHookFor(Pawn p)
        {
            if (IsFiance(p))
            {
                return DutyDefOf.MarryPawn.hook;
            }

            return DutyDefOf.Spectate.hook;
        }

        // Token: 0x06000835 RID: 2101 RVA: 0x000436F4 File Offset: 0x00041AF4
        public override void UpdateAllDuties()
        {
            foreach (var pawn in lord.ownedPawns)
            {
                if (IsFiance(pawn))
                {
                    pawn.mindState.duty = new PawnDuty(DutyDefOf.MarryPawn, FianceStandingSpotFor(pawn));
                }
                else
                {
                    var pawnDuty = new PawnDuty(DutyDefOf.Spectate)
                    {
                        spectateRect = Data.spectateRect,
                        spectateRectAllowedSides = Data.spectateRectAllowedSides
                    };
                    pawn.mindState.duty = pawnDuty;
                }
            }
        }

        // Token: 0x06000836 RID: 2102 RVA: 0x000437A4 File Offset: 0x00041BA4
        private bool IsFiance(Pawn p)
        {
            return p == firstPawn || p == secondPawn;
        }

        // Token: 0x06000837 RID: 2103 RVA: 0x000437C0 File Offset: 0x00041BC0
        public IntVec3 FianceStandingSpotFor(Pawn pawn)
        {
            Pawn pawn2;
            if (firstPawn == pawn)
            {
                pawn2 = secondPawn;
            }
            else
            {
                if (secondPawn != pawn)
                {
                    Log.Warning("Called ExactStandingSpotFor but it's not this pawn's ceremony.");
                    return IntVec3.Invalid;
                }

                pawn2 = firstPawn;
            }

            if (pawn.thingIDNumber < pawn2.thingIDNumber)
            {
                return spot;
            }

            if (GetMarriageSpotAt(spot) != null)
            {
                return FindCellForOtherPawnAtMarriageSpot(spot);
            }

            return spot + LordToil_MarriageCeremony.OtherFianceNoMarriageSpotCellOffset;
        }

        // Token: 0x06000838 RID: 2104 RVA: 0x00043855 File Offset: 0x00041C55
        private Thing GetMarriageSpotAt(IntVec3 cell)
        {
            return cell.GetThingList(Map).Find(x => x.def == ThingDefOf.MarriageSpot);
        }

        // Token: 0x06000839 RID: 2105 RVA: 0x00043888 File Offset: 0x00041C88
        private IntVec3 FindCellForOtherPawnAtMarriageSpot(IntVec3 cell)
        {
            var marriageSpotAt = GetMarriageSpotAt(cell);
            var cellRect = marriageSpotAt.OccupiedRect();
            for (var i = cellRect.minX; i <= cellRect.maxX; i++)
            {
                for (var j = cellRect.minZ; j <= cellRect.maxZ; j++)
                {
                    if (cell.x != i || cell.z != j)
                    {
                        return new IntVec3(i, 0, j);
                    }
                }
            }

            Log.Warning("Marriage spot is 1x1. There's no place for 2 pawns.");
            return IntVec3.Invalid;
        }

        // Token: 0x0600083A RID: 2106 RVA: 0x00043914 File Offset: 0x00041D14
        private CellRect CalculateSpectateRect()
        {
            var first = FianceStandingSpotFor(firstPawn);
            var second = FianceStandingSpotFor(secondPawn);
            return CellRect.FromLimits(first, second);
        }
    }
}