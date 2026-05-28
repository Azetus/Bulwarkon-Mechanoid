using Verse;

namespace BM_BulwarkonMechanoid.Hediffs;

public class HediffComp_HealingSmoke: HediffComp
{
    public HediffCompProperties_HealingSmoke Props =>
        (HediffCompProperties_HealingSmoke)props;

    private bool hasTended;

    public override void CompPostTick(ref float severityAdjustment)
    {
        if (hasTended) return;

        hasTended = true;
        List<Hediff> injuries = Pawn.health.hediffSet.hediffs;
        for (int i = 0; i < injuries.Count; i++)
        {
            if (injuries[i] is Hediff_Injury injury && injury.BleedRate > 0f)
                injury.Tended(Props.tendQuality, Props.tendMaxQuality, 0);
        }
    }

    public override void CompExposeData()
    {
        Scribe_Values.Look(ref hasTended, "hasTended");
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