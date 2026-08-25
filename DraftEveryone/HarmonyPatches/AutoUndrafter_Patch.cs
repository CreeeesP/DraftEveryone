using HarmonyLib;
using RimWorld;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(AutoUndrafter), "ShouldAutoUndraft")]
    internal static class AutoUndrafter_Patch
    {
        private static readonly AccessTools.FieldRef<AutoUndrafter, Pawn> PawnRef =
            AccessTools.FieldRefAccess<AutoUndrafter, Pawn>("pawn");

        static void Postfix(
            AutoUndrafter __instance,
            ref bool __result)
        {
            var settings = DraftEveryoneMod.Settings;

            if (settings == null || !settings.permanentDrafting)
                return;

            Pawn pawn = PawnRef(__instance);

            if (pawn != null && pawn.IsControllable())
                __result = false;
        }
    }
}