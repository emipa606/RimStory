using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Random = System.Random;

namespace RimStory
{
    public static class Resources
    {
        public static Map TEST_MAP;

        public static Random rng = new Random();
        public static int randomChanceRaid = 6;


        public static int xxx;
        public static int yyy;
        public static Vector2 vect = new Vector2(xxx, yyy);


        public static int minHour = 10;
        public static int maxHour = 20;

        public static List<Pawn> deadPawnsForMassFuneral = new List<Pawn>();
        public static List<Pawn> deadPawnsForMassFuneralBuried = new List<Pawn>();
        public static List<Building_Grave> graves = new List<Building_Grave>();


        public static List<IEvent> events = new List<IEvent>();
        public static List<IEvent> eventsToDelete = new List<IEvent>();

        public static List<IEvent> eventsLog = new List<IEvent>();
        public static List<Pawn> pawnsAttended = new List<Pawn>();


        public static Building_Grave lastGrave;

        public static List<Pawn> deadPawns = new List<Pawn>();

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
}