using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace DraftEveryone.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn), "GetGizmos")]
    internal static class Pawn_GetGizmos_Patch
    {
        private static readonly string DraftDesc =
            "DE_DraftDescription".Translate();

        static IEnumerable<Gizmo> Postfix(
            IEnumerable<Gizmo> __result,
            Pawn __instance)
        {
            if (!CompControlPawn.Instances.TryGetValue(
                    __instance,
                    out var comp))
            {
                foreach (var gizmo in __result)
                    yield return gizmo;

                yield break;
            }

            foreach (var gizmo in __result)
            {
                if (IsVanillaDraftCommand(gizmo))
                    continue;

                yield return gizmo;
            }

            yield return CreateDraftCommand(__instance, comp);
        }

        private static bool IsVanillaDraftCommand(Gizmo gizmo)
        {
            return gizmo is Command_Toggle command &&
                   command.hotKey == KeyBindingDefOf.Command_ColonistDraft &&
                   command.icon == TexCommand.Draft;
        }

        private static Command_Toggle CreateDraftCommand(
            Pawn pawn,
            CompControlPawn comp)
        {
            return new Command_Toggle
            {
                hotKey = KeyBindingDefOf.Command_ColonistDraft,
                isActive = () => pawn.Drafted,
                defaultDesc = DraftDesc,
                icon = TexCommand.Draft,
                turnOnSound = SoundDefOf.DraftOn,
                turnOffSound = SoundDefOf.DraftOff,
                groupKey = 81729172,

                toggleAction = () =>
                {
                    pawn.AssignComponents();

                    if (!pawn.Drafted)
                    {
                        comp.isControlled = true;
                        comp.originalFaction ??= pawn.Faction;
                        pawn.drafter.Drafted = true;
                    }
                    else
                    {
                        pawn.RestoreOriginalState();
                    }

                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(
                        ConceptDefOf.Drafting,
                        KnowledgeAmount.SpecificInteraction);
                },

                defaultLabel = (
                    pawn.Drafted
                        ? "CommandUndraftLabel"
                        : "CommandDraftLabel"
                ).Translate(),

                tutorTag = pawn.Drafted ? "Draft" : "Undraft"
            };
        }
    }
}