using RimWorld;
using Verse;

namespace Porian
{
    public class HediffCompProperties_FungalAffinity : HediffCompProperties
    {
        public HediffCompProperties_FungalAffinity()
        {
            this.compClass = typeof(HediffComp_FungalAffinity);
        }
    }

    public class HediffComp_FungalAffinity : HediffComp
    {
        private const int CheckInterval = 250; // 5초마다 체크

        public override void CompTickRare()
        {
            base.CompTickRare();

            Pawn pawn = this.parent.pawn;
            if (pawn == null || pawn.Map == null || !pawn.Spawned || pawn.Dead) return;

            TerrainDef terrain = pawn.Map.terrainGrid.TerrainAt(pawn.Position);
            bool onFungalGravel = terrain?.defName == "FungalGravel";

            this.parent.Severity = onFungalGravel ? 1f : 0f;
        }
    }
}