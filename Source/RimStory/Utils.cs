using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace RimStory;

public static class Utils
{
    public static int CurrentDay()
    {
        Vector2 vector;
        switch (WorldRendererUtility.WorldRenderedNow)
        {
            case true when Find.WorldSelector.selectedTile >= 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.selectedTile);
                break;
            case true when Find.WorldSelector.NumSelectedObjects > 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.FirstSelectedObject.Tile);
                break;
            default:
            {
                if (Find.CurrentMap == null)
                {
                    return 0;
                }

                vector = Find.WorldGrid.LongLatOf(Find.CurrentMap.Tile);
                break;
            }
        }

        var num = Find.TickManager.gameStartAbsTick == 0 ? Find.TickManager.TicksGame : Find.TickManager.TicksAbs;

        return GenDate.DayOfSeason(num, vector.x) + 1;
    }

    public static int CurrentHour()
    {
        Vector2 vector;
        switch (WorldRendererUtility.WorldRenderedNow)
        {
            case true when Find.WorldSelector.selectedTile >= 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.selectedTile);
                break;
            case true when Find.WorldSelector.NumSelectedObjects > 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.FirstSelectedObject.Tile);
                break;
            default:
            {
                if (Find.CurrentMap == null)
                {
                    return 0;
                }

                vector = Find.WorldGrid.LongLatOf(Find.CurrentMap.Tile);
                break;
            }
        }

        var num = Find.TickManager.gameStartAbsTick == 0 ? Find.TickManager.TicksGame : Find.TickManager.TicksAbs;

        return GenDate.HourOfDay(num, vector.x) + 1;
    }

    public static Quadrum CurrentQuadrum()
    {
        Vector2 vector;
        switch (WorldRendererUtility.WorldRenderedNow)
        {
            case true when Find.WorldSelector.selectedTile >= 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.selectedTile);
                break;
            case true when Find.WorldSelector.NumSelectedObjects > 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.FirstSelectedObject.Tile);
                break;
            default:
            {
                if (Find.CurrentMap == null)
                {
                    return 0;
                }

                vector = Find.WorldGrid.LongLatOf(Find.CurrentMap.Tile);
                break;
            }
        }

        var num = Find.TickManager.gameStartAbsTick == 0 ? Find.TickManager.TicksGame : Find.TickManager.TicksAbs;
        return GenDate.Quadrum(num, vector.x);
    }

    public static int CurrentYear()
    {
        Vector2 vector;
        switch (WorldRendererUtility.WorldRenderedNow)
        {
            case true when Find.WorldSelector.selectedTile >= 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.selectedTile);
                break;
            case true when Find.WorldSelector.NumSelectedObjects > 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.FirstSelectedObject.Tile);
                break;
            default:
            {
                if (Find.CurrentMap == null)
                {
                    return 0;
                }

                vector = Find.WorldGrid.LongLatOf(Find.CurrentMap.Tile);
                break;
            }
        }

        var num = Find.TickManager.gameStartAbsTick == 0 ? Find.TickManager.TicksGame : Find.TickManager.TicksAbs;

        return GenDate.Year(num, vector.x);
    }

    public static Date CurrentDate()
    {
        Vector2 vector;
        switch (WorldRendererUtility.WorldRenderedNow)
        {
            case true when Find.WorldSelector.selectedTile >= 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.selectedTile);
                break;
            case true when Find.WorldSelector.NumSelectedObjects > 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.FirstSelectedObject.Tile);
                break;
            default:
            {
                if (Find.CurrentMap == null)
                {
                    return null;
                }

                vector = Find.WorldGrid.LongLatOf(Find.CurrentMap.Tile);
                break;
            }
        }

        var num = Find.TickManager.gameStartAbsTick == 0 ? Find.TickManager.TicksGame : Find.TickManager.TicksAbs;
        var day = GenDate.DayOfSeason(num, vector.x) + 1;


        switch (WorldRendererUtility.WorldRenderedNow)
        {
            case true when Find.WorldSelector.selectedTile >= 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.selectedTile);
                break;
            case true when Find.WorldSelector.NumSelectedObjects > 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.FirstSelectedObject.Tile);
                break;
            default:
            {
                if (Find.CurrentMap == null)
                {
                    return null;
                }

                vector = Find.WorldGrid.LongLatOf(Find.CurrentMap.Tile);
                break;
            }
        }

        var num2 = Find.TickManager.gameStartAbsTick == 0 ? Find.TickManager.TicksGame : Find.TickManager.TicksAbs;
        var quadrum = GenDate.Quadrum(num2, vector.x);

        switch (WorldRendererUtility.WorldRenderedNow)
        {
            case true when Find.WorldSelector.selectedTile >= 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.selectedTile);
                break;
            case true when Find.WorldSelector.NumSelectedObjects > 0:
                vector = Find.WorldGrid.LongLatOf(Find.WorldSelector.FirstSelectedObject.Tile);
                break;
            default:
            {
                if (Find.CurrentMap == null)
                {
                    return null;
                }

                vector = Find.WorldGrid.LongLatOf(Find.CurrentMap.Tile);
                break;
            }
        }

        var num3 = Find.TickManager.gameStartAbsTick == 0 ? Find.TickManager.TicksGame : Find.TickManager.TicksAbs;

        var year = GenDate.Year(num3, vector.x);


        return new Date(day, quadrum, year);
    }
}