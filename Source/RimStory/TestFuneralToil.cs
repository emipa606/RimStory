using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace RimStory;

internal class TestFuneralToil : LordToil
{
    public static readonly IntVec3 OtherFianceNoMarriageSpotCellOffset = new(-1, 0, 0);

    private readonly Pawn firstPawn;

    private readonly Pawn secondPawn;

    private readonly IntVec3 spot;

    public TestFuneralToil(Pawn firstPawn, Pawn secondPawn, IntVec3 spot)
    {
        this.firstPawn = firstPawn;
        this.secondPawn = secondPawn;
        this.spot = spot;
        data = new LordToilData_MarriageCeremony();
    }

    private LordToilData_MarriageCeremony Data => (LordToilData_MarriageCeremony)data;

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

    public override ThinkTreeDutyHook VoluntaryJoinDutyHookFor(Pawn p)
    {
        return IsFiance(p) ? DutyDefOf.MarryPawn.hook : DutyDefOf.Spectate.hook;
    }

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

    private bool IsFiance(Pawn p)
    {
        return p == firstPawn || p == secondPawn;
    }

    private IntVec3 FianceStandingSpotFor(Pawn pawn)
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

    private Thing GetMarriageSpotAt(IntVec3 cell)
    {
        return cell.GetThingList(Map).Find(x => x.def == ThingDefOf.MarriageSpot);
    }

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

    private CellRect CalculateSpectateRect()
    {
        var first = FianceStandingSpotFor(firstPawn);
        var second = FianceStandingSpotFor(secondPawn);
        return CellRect.FromLimits(first, second);
    }
}