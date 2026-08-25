using HarmonyLib;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn), "IsColonySubhumanPlayerControlled", MethodType.Getter)]
    internal static class Pawn_IsColonySubhuman_Patch
    {
        static void Postfix(Pawn __instance, ref bool __result)
        {
            if (__instance.IsControllable()) __result = true;
        }
    }
}