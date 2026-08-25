using HarmonyLib;
using RimWorld;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(ForbidUtility), "SetForbidden")]
    internal static class ForbidUtility_Patch
    {
        static bool Prefix(Thing t)
        {
            return t is ThingWithComps thing &&
                   thing.GetComp<CompForbiddable>() != null;
        }
    }
}