using DraftEveryone;
using HarmonyLib;
using RimWorld;
using Verse;

namespace DraftEveryone.Compability.RJW
{
    [HarmonyPatch]
    internal static class RJW_RMB_CanControlPawn_Patch
    {
        private static System.Reflection.MethodBase TargetMethod()
        {
            return AccessTools.Method(
                "rjw.RMB.RMB_Menu:CanControlPawn");
        }

        private static void Postfix(
            Pawn pawn,
            ref AcceptanceReport __result)
        {
            if (pawn == null || !pawn.IsControllable())
                return;

            if (__result.Accepted)
                return;

            string reason = __result.Reason;

            if (reason == "Drafted" || reason == "Not a colonist")
            {
                __result = true;
            }
        }
    }
}