using RimWorld;
using Verse;

namespace Porian
{
    public class Gene_PorianFungalAffinity : Gene
    {
        private int tickCounter = 0;

        public override void Tick()
        {
            base.Tick();

            // pawn 없으면 종료
            if (pawn == null || pawn.Map == null || pawn.Dead)
                return;

            // 60틱마다 검사
            tickCounter++;

            if (tickCounter < 60)
                return;

            tickCounter = 0;

            // 현재 terrain 가져오기
            TerrainDef terrain =
                pawn.Position.GetTerrain(pawn.Map);

            // 균사 자갈인지 검사

            bool onFungal =
                terrain != null &&
                terrain.defName == "FungalGravel";

            // 현재 Hediff 가져오기
            Hediff hediff =
                pawn.health.hediffSet.GetFirstHediffOfDef(
                    HediffDef.Named("Porian_FungalBoost")
                );

            // 균사 지형 위인데 Hediff 없으면 추가
            if (onFungal && hediff == null)
            {
                pawn.health.AddHediff(
                    HediffDef.Named("Porian_FungalBoost")
                );
            }

            // 균사 지형 아닌데 Hediff 있으면 제거
            if (!onFungal && hediff != null)
            {
                pawn.health.RemoveHediff(hediff);
            }
        }
    }
}