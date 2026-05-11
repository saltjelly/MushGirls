using RimWorld;
using Verse;

namespace Porian
{
    public class Hediff_PorianTransformation : HediffWithComps
    {
        // 변이 진행 카운터
        private int tickCounter = 0;

        public override void Tick()
        {
            base.Tick();

            // pawn 없으면 종료
            if (pawn == null || pawn.Dead)
                return;

            tickCounter++;

            // 600틱 = 약 10초
            if (tickCounter < 600)
                return;

            tickCounter = 0;

            // 이미 포리안이면 종료
            if (pawn.def.defName == "Porian")
                return;

            // 포리안 유전자 부여
            GeneDef gene =
                DefDatabase<GeneDef>.GetNamed("Porian_MushroomCap");

            if (pawn.genes != null && !pawn.genes.HasActiveGene(gene))
            {
                pawn.genes.AddGene(gene, false);

                Messages.Message(
                    pawn.Name.ToStringShort + " has become connected to the fungal network.",
                    pawn,
                    MessageTypeDefOf.PositiveEvent
                );
            }

            // 헤디프 제거
            pawn.health.RemoveHediff(this); 
           
        }
    }
}