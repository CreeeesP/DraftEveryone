using HarmonyLib;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(VerbTracker), "CreateVerbTargetCommand")]
    internal static class VerbTracker_Patch
    {
        static void Postfix(
            Verb verb,
            ref Command_VerbTarget __result)
        {
            VerbCommandUtility.EnableIfControlled(
                verb?.CasterPawn,
                __result);
        }
    }
}