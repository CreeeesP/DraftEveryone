using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;
using Verse.AI;

namespace DraftEveryone
{
    [StaticConstructorOnStartup]
    internal static class ModInit
    {
        private static readonly Harmony Harmony = new("DraftEveryone");

        private static readonly FieldInfo TreeDefField;
        private static readonly FieldInfo TagToGiveField;

        static ModInit()
        {
            TreeDefField = AccessTools.Field(typeof(ThinkNode_Subtree), "treeDef");
            if (TreeDefField == null)
            {
                Log.Error("[DraftEveryone] Failed to find field 'treeDef' in ThinkNode_Subtree. Think tree patching will be skipped.");
            }

            TagToGiveField = AccessTools.Field(typeof(ThinkNode_Tagger), "tagToGive");
            if (TagToGiveField == null)
            {
                Log.Error("[DraftEveryone] Failed to find field 'tagToGive' in ThinkNode_Tagger. Think tree patching will be skipped.");
            }

            foreach (var def in DefDatabase<ThingDef>.AllDefs)
            {
                if (def.race == null || def.IsCorpse)
                    continue;

                def.comps ??= new List<CompProperties>();

                if (def.comps.Any(c => c is CompProperties_ControlPawn))
                    continue;

                def.comps.Add(new CompProperties_ControlPawn());
            }

            if (TreeDefField != null && TagToGiveField != null)
            {
                PatchThinkTreeDefs();
            }

            Harmony.PatchAll();
        }

        private static void PatchThinkTreeDefs()
        {
            foreach (var t in DefDatabase<ThinkTreeDef>.AllDefsListForReading)
            {
                if (t.defName == "Downed")
                    continue;

                var root = t.thinkRoot;

                if (root?.subNodes == null)
                    continue;

                if (root.subNodes.Any(n => n is ThinkNode_IsControllable))
                    continue;

                int i = root.subNodes.FindIndex(
                    n => n is ThinkNode_ConditionalColonist);

                if (i < 0)
                    i = root.subNodes.FindIndex(
                        n => n is ThinkNode_QueuedJob);

                if (i < 0)
                {
                    i = root.subNodes.FindIndex(n =>
                    {
                        if (n is not ThinkNode_Subtree subtree)
                            return false;

                        var treeDef =
                            TreeDefField.GetValue(subtree) as ThinkTreeDef;

                        return treeDef?.defName == "LordDuty";
                    });
                }

                if (i < 0)
                    i = root.subNodes.FindIndex(
                        n => n is ThinkNode_ConditionalRevenantState);

                if (i >= 0)
                {
                    var tagger = new ThinkNode_Tagger();

                    TagToGiveField.SetValue(
                        tagger,
                        JobTag.DraftedOrder);

                    tagger.subNodes = new List<ThinkNode>
                    {
                        new JobGiver_MoveToStandable(),
                        new JobGiver_Orders()
                    };

                    root.subNodes.Insert(
                        i,
                        new ThinkNode_IsControllable
                        {
                            subNodes = new List<ThinkNode>
                            {
                                new ThinkNode_QueuedJob(),
                                tagger
                            }
                        });
                }
            }
        }
    }
}