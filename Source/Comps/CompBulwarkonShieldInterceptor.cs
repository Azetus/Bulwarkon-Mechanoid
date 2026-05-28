using RimWorld;

namespace BM_BulwarkonMechanoid.Comps;

public class CompBulwarkonShieldInterceptor : CompProjectileInterceptor
{
    protected override int HitPointsPerInterval => BulwarkonMechanoid.settings.ShieldHitPointsPerInterval;
}