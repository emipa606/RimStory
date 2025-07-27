using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Random = System.Random;

namespace RimStory;

public static class Resources
{
    public static Map TEST_MAP;

    public static readonly Random rng = new();
    public static readonly int randomChanceRaid = 6;


    public static int xxx;
    public static int yyy;
    public static Vector2 vect = new(xxx, yyy);


    public static readonly int minHour = 10;
    public static readonly int maxHour = 20;

    public static List<Pawn> deadPawnsForMassFuneral = [];
    public static List<Pawn> deadPawnsForMassFuneralBuried = [];
    public static List<Building_Grave> graves = [];


    public static List<IEvent> events = [];
    public static readonly List<IEvent> eventsToDelete = [];

    public static List<IEvent> eventsLog = [];
    public static List<Pawn> pawnsAttended = [];


    public static Building_Grave lastGrave;

    public static readonly List<Pawn> deadPawns = [];

    public static bool isMemorialDayCreated = false;
    public static Date dateLastFuneral = null;

    public static bool showRaidsInLog = true;
    public static bool showDeadColonistsInLog = true;
    public static bool showIncidentsInLog = true;
    public static bool showCustomTextInLog = true;

    public static void RESET_LOG()
    {
        eventsLog.Clear();
    }
}