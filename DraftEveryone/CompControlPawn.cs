using RimWorld;
using System.Collections.Generic;
using Verse;

namespace DraftEveryone
{
    public class CompProperties_ControlPawn : CompProperties
    {
        public CompProperties_ControlPawn() => compClass = typeof(CompControlPawn);
    }

    public class CompControlPawn : ThingComp
    {
        internal bool isControlled;
        public Faction originalFaction;
        internal static readonly Dictionary<Pawn, CompControlPawn> Instances = new();
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            if (parent is not Pawn pawn)
                return;

            Instances[pawn] = this;
            if (isControlled)
            {
                originalFaction ??= pawn.Faction;
                pawn.AssignComponents();

                if (respawningAfterLoad && pawn.drafter != null)
                {
                    pawn.drafter.Drafted = true;
                }
            }
        }

        private void RemoveInstance()
        {
            if (parent is Pawn pawn)
                Instances.Remove(pawn);
        }

        public override void PostDeSpawn(Map map, DestroyMode mode = DestroyMode.Vanish)
        {
            base.PostDeSpawn(map, mode);
            RemoveInstance();
        }

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            base.PostDestroy(mode, previousMap);
            RemoveInstance();
        }

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(ref isControlled, "isControlled");
            Scribe_References.Look(ref originalFaction, "originalFaction");
        }
    }
}