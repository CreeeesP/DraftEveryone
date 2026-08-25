using UnityEngine;
using Verse;

namespace DraftEveryone
{
    public class DraftEveryoneMod : Mod
    {
        internal static DraftEveryoneSettings Settings;
        public DraftEveryoneMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<DraftEveryoneSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var l = new Listing_Standard();
            l.Begin(inRect);
            l.CheckboxLabeled("DE_PermanentDrafting".Translate(), ref Settings.permanentDrafting, "DE_PermanentDraftingTooltip".Translate());
            l.End();
        }

        public override string SettingsCategory() => "Draft Anything";
    }
}