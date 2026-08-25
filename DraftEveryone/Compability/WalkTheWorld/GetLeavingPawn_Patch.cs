using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace DraftEveryone.Compability.WalkTheWorld
{
    [HarmonyPatch]
    internal static class GetLeavingPawn_Patch
    {
        private static readonly Type WalkTheWorldType =
            AccessTools.TypeByName("WalkTheWorld.WalkTheWorld");

        private static readonly MethodInfo GetLeavingPawnMethod =
            WalkTheWorldType != null
                ? AccessTools.Method(WalkTheWorldType, "GetLeavingPawn")
                : null;

        private static bool Prepare()
        {
            return GetLeavingPawnMethod != null;
        }

        private static MethodBase TargetMethod()
        {
            return GetLeavingPawnMethod;
        }

        private static void Postfix(ref Pawn __result)
        {
            if (__result == null)
                return;

            if (__result.IsColonistPlayerControlled)
                return;

            __result = null;
        }
    }
}