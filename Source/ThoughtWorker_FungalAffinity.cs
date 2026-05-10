using RimWorld;
using Verse;

namespace Porian
{
    // ThoughtWorker : Thought가 언제 활성화될지 결정하는 핵심 클래스
    public class ThoughtWorker_FungalAffinity : ThoughtWorker
    {
        // 체크 주기 (60 = 약 1초마다 체크, 너무 자주하면 성능 부하)
        private const int CheckInterval = 60;

        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            // Pawn이 맵에 없거나 죽었거나 다운되었으면 Thought 비활성화
            if (pawn == null || pawn.Map == null || !pawn.Spawned || pawn.Dead || pawn.Downed)
                return ThoughtState.Inactive;

            // 현재 서 있는 Terrain(지형) 가져오기
            TerrainDef terrain = pawn.Position.GetTerrain(pawn.Map);

            // FungalGravel (또는 Fungal 관련 지형) 위에 있으면 Thought 활성화
            if (terrain != null && terrain.defName.Contains("Fungal"))
            {
                return ThoughtState.ActiveAtStage(0);   // Stage 0 사용
            }

            return ThoughtState.Inactive;
        }
    }
}