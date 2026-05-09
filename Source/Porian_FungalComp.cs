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
        private const int CheckInterval = 250; // 약 5초마다 체크 (가볍게)

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            // CheckInterval마다만 체크해서 성능 부하 최소화
            if (!this.parent.pawn.IsHashIntervalTick(CheckInterval))
                return;

            Pawn pawn = this.parent.pawn;
            if (pawn == null || pawn.Map == null || !pawn.Spawned || pawn.Dead)
                return;

            TerrainDef terrain = pawn.Map.terrainGrid.TerrainAt(pawn.Position);
            bool onFungalGravel = terrain != null && terrain.defName == "FungalGravel";

            // Fungal gravel 위면 severity = 1 (속도 증가), 아니면 0
            this.parent.Severity = onFungalGravel ? 1f : 0f;
        }
    }
}