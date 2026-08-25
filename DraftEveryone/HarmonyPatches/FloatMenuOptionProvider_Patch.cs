using HarmonyLib;
using RimWorld;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(FloatMenuOptionProvider), "SelectedPawnValid")]
    internal static class FloatMenuOptionProvider_Patch
    {
        public static void Postfix(FloatMenuOptionProvider __instance, ref bool __result, Pawn pawn)
        {
            if (__instance is FloatMenuOptionProvider_DraftedMove
                && pawn != null
                && pawn.Drafted
                && pawn.IsControllable())
            {
                __result = true;
            }
        }
    }
}