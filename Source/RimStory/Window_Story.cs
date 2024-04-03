using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace RimStory;

internal class Window_Story : MainTabWindow
{
    public static readonly int sie = 0;
    public static Vector2 vect = new Vector2(sie, sie);
    public static Vector2 logSize;

    private static readonly int defaultLogSize = 200;
    private readonly Listing_Standard listing_Standard = new Listing_Standard();

    private readonly Texture2D texDelete = ContentFinder<Texture2D>.Get("ui/buttons/Delete");
    private Rect bigRect;
    private string customText = "";
    private Rect filterRect;
    private Rect inner;
    private Rect outter;

    public override void DoWindowContents(Rect rect)
    {
        if (!RimStoryMod.settings.enableLogging)
        {
            return;
        }

        if (Resources.eventsLog != null)
        {
            Text.Font = GameFont.Small;

            outter = new Rect(rect.position, new Vector2(rect.width - 200, rect.height));
            var txtWidth = outter.width - 120;
            var yoff = 20f;
            float logHeight = 0;
            foreach (var e in Resources.eventsLog)
            {
                logHeight += Text.CalcHeight(e.ShowInLog(), txtWidth);
            }

            logHeight += Resources.eventsLog.Count * 10;

            bigRect = new Rect(rect.position, new Vector2(rect.width, defaultLogSize + logHeight));
            logSize = new Vector2(rect.x, defaultLogSize + logHeight);
            inner = new Rect(rect.position, logSize);

            Widgets.BeginScrollView(outter, ref vect, inner);

            foreach (var e in Resources.eventsLog)
            {
                if (e?.ShowInLog() == null)
                {
                    continue;
                }

                if (!Resources.showRaidsInLog && e is ABigThreat)
                {
                }
                else if (!Resources.showDeadColonistsInLog && e is AMemorialDay)
                {
                }
                else if (!Resources.showIncidentsInLog && e is IncidentShort)
                {
                }
                else if (!Resources.showCustomTextInLog && e is CustomEvent)
                {
                }
                else
                {
                    if (e is CustomEvent)
                    {
                        var btnRect = new Rect(outter.x + 10, yoff, 20, 20);
                        if (Widgets.ButtonImage(btnRect, texDelete))
                        {
                            e.EndEvent();
                        }
                    }

                    var height = Text.CalcHeight(e.ShowInLog(), txtWidth);
                    Widgets.Label(new Rect(outter.x + 50, yoff, txtWidth, height), e.ShowInLog());
                    yoff += height + 10;
                }
            }

            foreach (var e in Resources.eventsLog.Reverse<IEvent>())
            {
                if (e is CustomEvent && !e.IsStillEvent())
                {
                    Resources.eventsLog.Remove(e);
                }
            }

            var txtRect = new Rect(outter.x + 50, yoff + 20, txtWidth, 200);
            listing_Standard.Begin(txtRect);

            if (Resources.eventsLog.Count == 0)
            {
                listing_Standard.Label("Nothing here yet.");
            }

            listing_Standard.Label("Enter new story:");
            var str = listing_Standard.TextEntry(customText, 3);
            if (str.Length < 4000)
            {
                customText = str;
            }

            if (listing_Standard.ButtonText("Save"))
            {
                if (customText != "")
                {
                    Resources.eventsLog.Add(new CustomEvent(Utils.CurrentDate(), customText));
                    customText = "";
                }
            }

            listing_Standard.End();
        }

        Widgets.EndScrollView();

        filterRect = new Rect(new Vector2(outter.width, rect.position.y), new Vector2(200, 200));
        listing_Standard.Begin(filterRect);
        listing_Standard.CheckboxLabeled("ShowRaidsInLog".Translate(), ref Resources.showRaidsInLog);
        listing_Standard.CheckboxLabeled("ShowDeadColonistsInLog".Translate(),
            ref Resources.showDeadColonistsInLog);
        listing_Standard.CheckboxLabeled("ShowIncidentsInLog".Translate(), ref Resources.showIncidentsInLog);
        listing_Standard.CheckboxLabeled("Show custom events", ref Resources.showCustomTextInLog);
        listing_Standard.End();
    }

    public void ExposeData()
    {
        // throw new NotImplementedException();
    }
}