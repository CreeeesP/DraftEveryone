using HarmonyLib;
using RimWorld;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(Game), "LoadGame")]
    internal static class Game_LoadGame_Patch
    {
        private static void Postfix()
        {
            RestoreControlledPawns();
        }

        private static void RestoreControlledPawns()
        {
            foreach (var pawn in PawnsFinder.AllMapsWorldAndTemporary_Alive)
            {
                if (CompControlPawn.Instances.TryGetValue(pawn, out var comp)
                    && comp.isControlled)
                {
                    if (pawn.drafter == null)
                    {
                        pawn.AssignComponents();
                    }

                    pawn.drafter.Drafted = true;
                }
            }
        }
    }
}