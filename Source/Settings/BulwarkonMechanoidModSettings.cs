using Verse;

namespace BM_BulwarkonMechanoid.Settings;

public class BulwarkonMechanoidModSettings : ModSettings
{
    public float ShieldRadius = 3;
    public int ShieldHitPoints = 500;
    public int chargeDurationTicks = 5400;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref ShieldRadius, nameof(ShieldRadius), 3);
        Scribe_Values.Look(ref ShieldHitPoints, nameof(ShieldHitPoints), 500);
        Scribe_Values.Look(ref chargeDurationTicks, nameof(chargeDurationTicks), 540);
    }
}