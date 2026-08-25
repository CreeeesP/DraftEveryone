using DraftEveryone;
using HarmonyLib;
using RimWorld;
using Verse;

namespace DraftEveryone.Compability.RJW
{
    [HarmonyPatch(typeof(FloatMenuOptionProvider), "SelectedPawnValid")]
    internal static class RJW_RMB_SelectedPawnValid_Patch
    {
        private static readonly System.Type RMBMenuType =
            AccessTools.TypeByName("rjw.RMB.RMB_Menu");

        private static void Postfix(
            FloatMenuOptionProvider __instance,
            Pawn pawn,
            FloatMenuContext context,
            ref bool __result)
        {
            if (__result)
                return;

            if (pawn == null || !pawn.IsControllable())
                return;

            if (RMBMenuType == null)
                return;

            if (!RMBMenuType.IsInstanceOfType(__instance))
                return;

            __result = true;
        }
    }
}