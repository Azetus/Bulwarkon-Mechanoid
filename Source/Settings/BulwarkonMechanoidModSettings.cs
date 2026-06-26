using BM_BulwarkonMechanoid.Comps;
using BM_BulwarkonMechanoid.DefOfs;
using Verse;

namespace BM_BulwarkonMechanoid.Settings;

public class BulwarkonMechanoidModSettings : ModSettings
{
    public float ShieldRadius = 4;
    public int ShieldHitPoints = 2000;
    public int chargeDurationTicks = 1800;
    public int ShieldHitPointsPerInterval = 1;
    public int RechargeHitPointsIntervalTicks = 120;
    public int DisarmedByEmpForTicks = 0;
    public ShieldAmmoType ShieldAmmo = ShieldAmmoType.BroadshieldCore;
    public SmokeAmmoType SmokeAmmo = SmokeAmmoType.MedicineUltratech;
    public int SmokeHealIntervalTicks = 60;
    public int SmokeHealAmount = 1;
    public bool AllowHostileBulwarkon = false;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref ShieldRadius, nameof(ShieldRadius), 4);
        Scribe_Values.Look(ref ShieldHitPoints, nameof(ShieldHitPoints), 2000);
        Scribe_Values.Look(ref chargeDurationTicks, nameof(chargeDurationTicks), 1800);
        Scribe_Values.Look(ref ShieldHitPointsPerInterval, nameof(ShieldHitPointsPerInterval), 1);
        Scribe_Values.Look(ref RechargeHitPointsIntervalTicks, nameof(RechargeHitPointsIntervalTicks), 120);
        Scribe_Values.Look(ref DisarmedByEmpForTicks, nameof(DisarmedByEmpForTicks), 0);
        Scribe_Values.Look(ref ShieldAmmo, nameof(ShieldAmmo), ShieldAmmoType.BroadshieldCore);
        Scribe_Values.Look(ref SmokeAmmo, nameof(SmokeAmmo), SmokeAmmoType.MedicineUltratech);
        Scribe_Values.Look(ref SmokeHealIntervalTicks, nameof(SmokeHealIntervalTicks), 60);
        Scribe_Values.Look(ref SmokeHealAmount, nameof(SmokeHealAmount), 1);
        Scribe_Values.Look(ref AllowHostileBulwarkon, nameof(AllowHostileBulwarkon), false);

        if (Scribe.mode == LoadSaveMode.Saving)
        {
            ApplyToDefs();
            ApplyToPawnKind();
        }

        if (Scribe.mode == LoadSaveMode.PostLoadInit)
        {
            LongEventHandler.ExecuteWhenFinished(ApplyToPawnKind);
        }
    }

    public void ApplyToDefs()
    {
        var def = BM_BulwarkonThingDefOfs.Mech_Bulwarkon;
        if (def == null) return;
        var props = def.comps?.OfType<CompProperties_BulwarkonShieldInterceptor>().FirstOrDefault();
        if (props == null) return;
        props.radius = ShieldRadius;
        props.hitPoints = ShieldHitPoints;
        props.chargeDurationTicks = chargeDurationTicks;
        props.rechargeHitPointsIntervalTicks = RechargeHitPointsIntervalTicks;
        props.disarmedByEmpForTicks = DisarmedByEmpForTicks;
    }

    public void ApplyToPawnKind()
    {
        var pawnKind = BM_BulwarkonPawnKindDefOfs.Mech_Bulwarkon;
        if (pawnKind == null) return;
        pawnKind.isFighter = AllowHostileBulwarkon;
        pawnKind.allowInMechClusters = AllowHostileBulwarkon;
        pawnKind.appearsRandomlyInCombatGroups = AllowHostileBulwarkon;
    }

    public void ResetToDefaults()
    {
        ShieldRadius = 4;
        ShieldHitPoints = 2000;
        chargeDurationTicks = 1800;
        ShieldHitPointsPerInterval = 1;
        RechargeHitPointsIntervalTicks = 120;
        DisarmedByEmpForTicks = 0;
        ShieldAmmo = ShieldAmmoType.BroadshieldCore;
        SmokeAmmo = SmokeAmmoType.MedicineUltratech;
        SmokeHealIntervalTicks = 60;
        SmokeHealAmount = 1;
        AllowHostileBulwarkon = false;
    }
}