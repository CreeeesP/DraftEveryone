using Verse;
using Verse.AI;

namespace DraftEveryone
{
    public class ThinkNode_IsControllable : ThinkNode_Conditional
    {
        protected override bool Satisfied(Pawn pawn) => pawn.IsControllable();
    }
}