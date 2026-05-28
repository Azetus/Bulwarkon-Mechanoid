using Verse;

namespace BM_BulwarkonMechanoid.Settings;

public class BulwarkonMechanoidModSettings : ModSettings
{
    // 护盾半径
    public float ShieldRadius = 3;

    // 护盾盾量
    public int ShieldHitPoints = 2000;

    // 护盾破碎后恢复时间
    public int chargeDurationTicks = 5400;

    // 护盾每次恢复时恢复的盾量
    public int ShieldHitPointsPerInterval = 1;

    // 护盾恢复间隔
    public int ShieldRechargeIntervalTicks = 240;

    // 遭EMP打击后所需的恢复时间
    public int DisarmedByEmpForTicks = 0;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref ShieldRadius, nameof(ShieldRadius), 3);
        Scribe_Values.Look(ref ShieldHitPoints, nameof(ShieldHitPoints), 500);
        Scribe_Values.Look(ref chargeDurationTicks, nameof(chargeDurationTicks), 540);
        Scribe_Values.Look(ref ShieldHitPointsPerInterval, nameof(ShieldHitPointsPerInterval), 1);
        Scribe_Values.Look(ref ShieldRechargeIntervalTicks, nameof(ShieldRechargeIntervalTicks), 240);
        Scribe_Values.Look(ref DisarmedByEmpForTicks, nameof(DisarmedByEmpForTicks), 0);
    }
}