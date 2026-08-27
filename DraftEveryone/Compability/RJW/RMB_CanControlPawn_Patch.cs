using HarmonyLib;
using Verse;

namespace DraftEveryone.Compability.RJW
{
    [HarmonyPatch]
    internal static class RMB_CanControlPawn_Patch
    {
        private static bool rjwInstalled;
        private static System.Reflection.MethodBase targetMethod;

        private static bool Prepare()
        {
            rjwInstalled = AccessTools.TypeByName("rjw.RMB.RMB_Menu") != null;
            return rjwInstalled;
        }

        private static System.Reflection.MethodBase TargetMethod()
        {
            if (targetMethod == null && rjwInstalled)
            {
                targetMethod = AccessTools.Method(
                    "rjw.RMB.RMB_Menu:CanControlPawn");
            }
            return targetMethod;
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
