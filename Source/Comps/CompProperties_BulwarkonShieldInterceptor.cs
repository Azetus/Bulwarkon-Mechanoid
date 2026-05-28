using RimWorld;

namespace BM_BulwarkonMechanoid.Comps;

public class CompProperties_BulwarkonShieldInterceptor : CompProperties_ProjectileInterceptor
{
    public CompProperties_BulwarkonShieldInterceptor()
    {
        compClass = typeof(CompProjectileInterceptor);
        radius = BulwarkonMechanoid.settings.ShieldRadius;
        hitPoints = BulwarkonMechanoid.settings.ShieldHitPoints;
        chargeDurationTicks = BulwarkonMechanoid.settings.chargeDurationTicks;
        disarmedByEmpForTicks = 0;
    }
}