using HarmonyLib;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(SavedGameLoaderNow), "LoadGameFromSaveFileNow")]
    internal static class SavedGameLoaderNow_Patch
    {
        static void Prefix()
        {
            CompControlPawn.Instances.Clear();
        }
    }
}