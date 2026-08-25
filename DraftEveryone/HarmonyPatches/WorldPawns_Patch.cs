using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(WorldPawns), "PassToWorld")]
    internal static class WorldPawns_Patch
    {
        public static void Prefix(Pawn pawn)
        {
            if (CompControlPawn.Instances.TryGetValue(
                    pawn,
                    out var comp) &&
                comp.isControlled)
            {
                pawn.RestoreOriginalState();
            }
        }
    }
}