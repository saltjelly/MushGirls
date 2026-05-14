using RimWorld;
using Verse;
using System.Collections.Generic;

namespace Porian
{
    // ==================== Properties ====================
    public class HediffCompProperties_Regeneration : HediffCompProperties
    {
        public float healIntervalDays = 15f;
        public float healIntervalDaysMax = 30f;
        public bool healScarOnly = true;

        public HediffCompProperties_Regeneration()
        {
            this.compClass = typeof(HediffComp_Regeneration);
        }
    }

    // ==================== 실제 Comp ====================
    public class HediffComp_Regeneration : HediffComp
    {
        private int ticksUntilHeal = -1;

        public HediffCompProperties_Regeneration Props => (HediffCompProperties_Regeneration)this.props;

        public override void CompPostMake()
        {
            base.CompPostMake();
            ResetHealTimer();
        }

        private void ResetHealTimer()
        {
            float days = Rand.Range(Props.healIntervalDays, Props.healIntervalDaysMax);
            ticksUntilHeal = (int)(days * 60000f);
        }

        // 1.6 기준 올바른 Tick 메서드
        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (ticksUntilHeal > 0)
            {
                ticksUntilHeal--;

                if (ticksUntilHeal <= 0)
                {
                    TryHealScar();
                    ResetHealTimer();
                }
            }
        }

        private void TryHealScar()
        {
            Pawn pawn = this.parent.pawn;
            if (pawn == null || pawn.Dead || pawn.health?.hediffSet == null)
                return;

            List<Hediff> scars = new List<Hediff>();

            foreach (Hediff hediff in pawn.health.hediffSet.hediffs)
            {
                if (hediff is Hediff_Injury injury && injury.IsPermanent())
                {
                    scars.Add(hediff);
                }
            }

            if (scars.Count > 0)
            {
                Hediff scarToHeal = scars.RandomElement();
                pawn.health.RemoveHediff(scarToHeal);

                Messages.Message("PorianRegen_ScarHealed".Translate(pawn.LabelShort, scarToHeal.Label),
                               pawn, MessageTypeDefOf.PositiveEvent);
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look(ref ticksUntilHeal, "ticksUntilHeal", -1);
        }
    }
}