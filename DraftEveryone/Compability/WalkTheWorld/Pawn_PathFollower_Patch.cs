using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using Verse.AI;

namespace DraftEveryone.Compability.WalkTheWorld
{
    [HarmonyPatch(typeof(Pawn_PathFollower), nameof(Pawn_PathFollower.PatherTick))]
    internal static class Pawn_PathFollower_Patch
    {
        private static readonly FieldInfo PawnField =
            AccessTools.Field(typeof(Pawn_PathFollower), "pawn");

        private static bool Prefix(Pawn_PathFollower __instance)
        {
            if (PawnField == null)
                return true;

            Pawn pawn = PawnField.GetValue(__instance) as Pawn;

            if (pawn == null)
                return true;

            if (!pawn.IsControllable())
                return true;

            if (pawn.IsColonistPlayerControlled)
                return true;

            if (!pawn.Spawned || pawn.Map == null)
                return true;

            if (pawn.CurJob == null ||
                pawn.CurJob.def != JobDefOf.Goto)
            {
                return true;
            }

            if (!pawn.Position.OnEdge(pawn.Map))
                return true;

            pawn.ExitMap(
                allowedToJoinOrCreateCaravan: false,
                exitDir: CellRect.WholeMap(pawn.Map)
                    .GetClosestEdge(pawn.Position));

            return false;
        }
    }
}