using Verse;

namespace BM_BulwarkonMechanoid.Hediffs;

public class HediffComp_HealingSmoke : HediffComp
{
    public HediffCompProperties_HealingSmoke Props =>
        (HediffCompProperties_HealingSmoke)props;

    public override void CompPostTick(ref float severityAdjustment)
    {
        if (Pawn.IsHashIntervalTick(BulwarkonMechanoid.settings.SmokeHealIntervalTicks))
        {
            TendOneInjury();
            HealOneInjury();
        }
    }


    private void TendOneInjury()
    {
        var injury = Pawn.health.hediffSet.hediffs
            .Find(h => h.TendableNow()) as Hediff_Injury;
        if (injury != null)
            injury.Tended(Props.tendQuality, Props.tendMaxQuality, 0);
    }

    private void HealOneInjury()
    {
        var injury = Pawn.health.hediffSet.hediffs
            .OfType<Hediff_Injury>()
            .FirstOrDefault(h => !h.IsPermanent());
        if (injury != null)
            injury.Heal(BulwarkonMechanoid.settings.SmokeHealAmount);
    }
}

public class HediffCompProperties_HealingSmoke : HediffCompProperties
{
    public float tendQuality = 1.0f;
    public float tendMaxQuality = 1.3f;

    public HediffCompProperties_HealingSmoke()
    {
        compClass = typeof(HediffComp_HealingSmoke);
    }
}