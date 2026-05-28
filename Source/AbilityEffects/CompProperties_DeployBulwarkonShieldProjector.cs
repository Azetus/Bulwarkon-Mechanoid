using RimWorld;
using Verse;

namespace BM_BulwarkonMechanoid.AbilityEffects;

public class CompProperties_DeployBulwarkonShieldProjector: CompProperties_AbilityEffect
{
    public ThingDef shieldProjectorDef;
    public int baseReloadTicks = 60;
    public EffecterDef deployEffect;
    public SoundDef deploySound;
    
    public CompProperties_DeployBulwarkonShieldProjector()
    {
        compClass = typeof(CompAbilityEffect_DeployBulwarkonShieldProjector);
    }
}