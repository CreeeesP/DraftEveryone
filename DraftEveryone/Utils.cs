using RimWorld;
using System.Linq;
using Verse;

namespace DraftEveryone
{
    public static class Utils
    {
        public static bool IsControllable(this Pawn pawn) => pawn.IsControllable(out _);

        public static bool IsControllable(
            this Pawn pawn,
            out CompControlPawn comp)
        {
            if (pawn == null)
            {
                comp = null;
                return false;
            }

            return CompControlPawn.Instances.TryGetValue(pawn, out comp)
                   && comp.isControlled;
        }

        public static void AssignComponents(this Pawn pawn)
        {
            pawn.playerSettings ??= new Pawn_PlayerSettings(pawn);

            pawn.drafter ??= new Pawn_DraftController(pawn);
            pawn.equipment ??= new Pawn_EquipmentTracker(pawn);
        }

        public static void RestoreOriginalState(this Pawn pawn)
        {
            if (!CompControlPawn.Instances.TryGetValue(pawn, out var comp))
                return;

            comp.isControlled = false;

            if (comp.originalFaction != null
                && Find.FactionManager != null
                && Find.FactionManager.AllFactions.Contains(comp.originalFaction)
                && pawn.Faction != comp.originalFaction)
            {
                pawn.SetFaction(comp.originalFaction);
            }

            pawn.drafter?.Drafted = false;
        }
    }
}