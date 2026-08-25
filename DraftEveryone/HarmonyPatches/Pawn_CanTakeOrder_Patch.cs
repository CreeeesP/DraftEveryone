using HarmonyLib;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn), "CanTakeOrder", MethodType.Getter)]
    internal static class Pawn_CanTakeOrder_Patch
    {
        static void Postfix(Pawn __instance, ref bool __result)
        {
            if (!__result && __instance.IsControllable())
                __result = true;
        }
    }
}