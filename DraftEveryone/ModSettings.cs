using Verse;

namespace DraftEveryone
{
    public class DraftEveryoneSettings : ModSettings
    {
        public bool permanentDrafting;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref permanentDrafting, "permanentDrafting");
        }
    }
}