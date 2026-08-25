using Verse;

namespace DraftEveryone
{
    internal static class VerbCommandUtility
    {
        private static readonly string NonControlledReason =
            "DE_CannotOrderNonControlled".Translate();

        public static void EnableIfControlled(
            Pawn pawn,
            Command_VerbTarget command)
        {
            if (command == null || pawn == null || !pawn.IsControllable())
                return;

            if (command.disabledReason != NonControlledReason)
                return;

            command.Disabled = false;
            command.disabledReason = null;
        }
    }
}
