using HarmonyLib;
using RimWorld;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn), "ExitMap")]
    internal static class Pawn_ExitMap_Patch
    {
        private static void Prefix(
            Pawn __instance,
            ref bool allowedToJoinOrCreateCaravan)
        {
            if (!__instance.IsControllable())
                return;

            if (__instance.IsColonistPlayerControlled)
                return;

            allowedToJoinOrCreateCaravan = false;
        }
    }
}