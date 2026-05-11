using RimWorld;
using Verse;

namespace Porian
{
    public static class MushroomCapUtility
    {
        public static bool HasMushroomGene(Pawn pawn)
        {
            if (pawn?.genes == null)
                return false;

            return pawn.genes.HasActiveGene(DefDatabase<GeneDef>.GetNamed("Porian_MushroomCap")
            );
        }
    }
}