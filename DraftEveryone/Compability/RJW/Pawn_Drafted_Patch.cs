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
    internal static class RJW_MustNotBeDrafted_Patch
    {
        static System.Reflection.MethodBase TargetMethod()
        {
            return AccessTools.Method("rjw.JobDriver_SexBaseReciever:MustNotBeDrafted");
        }

        static void Postfix(Pawn partner, ref bool __result)
        {
            if (partner != null && partner.IsControllable())
                __result = true;
        }
    }
}