using BM_BulwarkonMechanoid.Comps;
using BM_BulwarkonMechanoid.DefOfs;
using Verse;

namespace BM_BulwarkonMechanoid.Settings;

public class BulwarkonMechanoidModSettings : ModSettings
{
    public float ShieldRadius = 3;
    public int ShieldHitPoints = 2000;
    public int chargeDurationTicks = 3600;
    public int ShieldHitPointsPerInterval = 1;
    public int RechargeHitPointsIntervalTicks = 120;
    public int DisarmedByEmpForTicks = 0;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref ShieldRadius, nameof(ShieldRadius), 3);
        Scribe_Values.Look(ref ShieldHitPoints, nameof(ShieldHitPoints), 2000);
        Scribe_Values.Look(ref chargeDurationTicks, nameof(chargeDurationTicks), 3600);
        Scribe_Values.Look(ref ShieldHitPointsPerInterval, nameof(ShieldHitPointsPerInterval), 1);
        Scribe_Values.Look(ref RechargeHitPointsIntervalTicks, nameof(RechargeHitPointsIntervalTicks), 120);
        Scribe_Values.Look(ref DisarmedByEmpForTicks, nameof(DisarmedByEmpForTicks), 0);
    }

    public void ApplyToDefs()
    {
        var def = BM_BulwarkonDefOfs.Mech_Bulwarkon;
        if (def == null) return;
        var props = def.comps.OfType<CompProperties_BulwarkonShieldInterceptor>().FirstOrDefault();
        if (props == null) return;
        props.radius = ShieldRadius;
        props.hitPoints = ShieldHitPoints;
        props.chargeDurationTicks = chargeDurationTicks;
        props.rechargeHitPointsIntervalTicks = RechargeHitPointsIntervalTicks;
        props.disarmedByEmpForTicks = DisarmedByEmpForTicks;
    }
    
    public void ResetToDefaults()
    {
        ShieldRadius = 3;
        ShieldHitPoints = 2000;
        chargeDurationTicks = 3600;
        ShieldHitPointsPerInterval = 1;
        RechargeHitPointsIntervalTicks = 120;
        DisarmedByEmpForTicks = 0;
    }
}