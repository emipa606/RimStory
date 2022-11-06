//using SettingsHelper;

using Mlie;
using UnityEngine;
using Verse;

namespace RimStory;

internal class RimStoryMod : Mod
{
    public static RimStorySettings settings;
    private static string currentVersion;

    public RimStoryMod(ModContentPack content) : base(content)
    {
        settings = GetSettings<RimStorySettings>();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(ModLister.GetActiveModWithIdentifier("Mlie.RimStory"));
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(inRect);
        listing_Standard.CheckboxLabeled("EnEventLogger".Translate(), ref settings.enableLogging);
        listing_Standard.CheckboxLabeled("EnFunerals".Translate(), ref settings.enableFunerals);
        listing_Standard.CheckboxLabeled("EnMarriage".Translate(), ref settings.enableMarriageAnniversary);
        listing_Standard.CheckboxLabeled("EnMemorialDay".Translate(), ref settings.enableMemoryDay);
        listing_Standard.CheckboxLabeled("EnDaysOfVictory".Translate(), ref settings.enableDaysOfVictory);
        listing_Standard.CheckboxLabeled("EnIndividualThoughts".Translate(), ref settings.enableIndividualThoughts);

        if (Prefs.DevMode)
        {
            listing_Standard.CheckboxLabeled("LOG RESET? LOG RESET? LOG RESET?", ref settings.ISLOGGONNARESET);
        }

        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("EnCurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
        settings.Write();
    }

    public override string SettingsCategory()
    {
        return "RimStory";
    }
}