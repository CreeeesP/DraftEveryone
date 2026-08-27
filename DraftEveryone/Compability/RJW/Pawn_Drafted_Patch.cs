using HarmonyLib;
using Verse;

namespace DraftEveryone.Compability.RJW
{
    [HarmonyPatch(typeof(Pawn), "get_Drafted")]
    internal static class Pawn_Drafted_Patch
    {
        static void Postfix(Pawn __instance, ref bool __result)
        {
            if (!__result || !__instance.IsControllable())
                return;

            var curDriver = __instance.jobs?.curDriver;
            if (curDriver != null && curDriver.GetType().Namespace?.StartsWith("rjw") == true)
            {
                __result = false;
            }
        }
    }

    [HarmonyPatch]
    internal static class MustNotBeDrafted_Patch
    {
        private static bool rjwInstalled;
        private static System.Reflection.MethodBase targetMethod;
        static bool Prepare()
        {
            rjwInstalled = AccessTools.TypeByName("rjw.JobDriver_SexBaseReciever") != null;
            return rjwInstalled;
        }
        static System.Reflection.MethodBase TargetMethod()
        {
            if (targetMethod == null && rjwInstalled)
            {
                targetMethod = AccessTools.Method("rjw.JobDriver_SexBaseReciever:MustNotBeDrafted");
            }
            return targetMethod;
        }

        static void Postfix(Pawn partner, ref bool __result)
        {
            if (partner != null && partner.IsControllable())
                __result = true;
        }
    }
}
