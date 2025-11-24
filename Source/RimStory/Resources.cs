using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Random = System.Random;

namespace RimStory;

public static class Resources
{
    public static Date dateLastFuneral = null;

    public static readonly List<Pawn> deadPawns = [];

    public static List<Pawn> deadPawnsForMassFuneral = [];
    public static List<Pawn> deadPawnsForMassFuneralBuried = [];


    public static List<IEvent> events = [];

    public static List<IEvent> eventsLog = [];
    public static readonly List<IEvent> eventsToDelete = [];
    public static List<Building_Grave> graves = [];

    public static bool isMemorialDayCreated = false;


    public static Building_Grave lastGrave;
    public static readonly int maxHour = 20;


    public static readonly int minHour = 10;
    public static List<Pawn> pawnsAttended = [];
    public static readonly int randomChanceRaid = 6;

    public static readonly Random rng = new();
    public static bool showCustomTextInLog = true;
    public static bool showDeadColonistsInLog = true;
    public static bool showIncidentsInLog = true;

    public static bool showRaidsInLog = true;
    public static Map TEST_MAP;


    public static int xxx;
    public static int yyy;
    public static Vector2 vect = new(xxx, yyy);

    public static void RESET_LOG()
    {
        eventsLog.Clear();
    }
}