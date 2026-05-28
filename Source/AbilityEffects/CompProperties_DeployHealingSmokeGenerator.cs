using RimWorld;
using Verse;

namespace BM_BulwarkonMechanoid.AbilityEffects;

public class CompProperties_DeployHealingSmokeGenerator: CompProperties_AbilityEffect
{
    public ThingDef smokeGeneratorDef;
    public int baseReloadTicks = 60;
    public EffecterDef? deployEffect;
    public SoundDef? deploySound;

    public CompProperties_DeployHealingSmokeGenerator()
    {
        compClass = typeof(CompAbilityEffect_DeployHealingSmokeGenerator);
    }
}