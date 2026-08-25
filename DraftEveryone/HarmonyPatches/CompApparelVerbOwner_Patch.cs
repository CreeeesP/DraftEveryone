using HarmonyLib;
using RimWorld;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(CompApparelVerbOwner), "CreateVerbTargetCommand")]
    internal static class CompApparelVerbOwner_Patch
    {
        static void Postfix(
            CompApparelVerbOwner __instance,
            ref Command_VerbTarget __result)
        {
            VerbCommandUtility.EnableIfControlled(
                __instance.Wearer,
                __result);
        }
    }
}