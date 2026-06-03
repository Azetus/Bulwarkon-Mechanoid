using BM_BulwarkonMechanoid.DefOfs;
using BM_BulwarkonMechanoid.Settings;
using RimWorld;
using RimWorld.Utility;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace BM_BulwarkonMechanoid.AbilityEffects;

public class CompAbilityEffect_DeployHealingSmokeGenerator : CompAbilityEffect, IAbilityReloadable
{
    public new CompProperties_DeployHealingSmokeGenerator Props => (CompProperties_DeployHealingSmokeGenerator)props;

    #region IReloadableComp

    public Thing ReloadableThing => parent.pawn;

    public ThingDef AmmoDef => BulwarkonMechanoid.settings.SmokeAmmo switch
    {
        SmokeAmmoType.MedicineUltratech => ThingDefOf.MedicineUltratech,
        _ => ThingDefOf.MedicineIndustrial
    };

    public int BaseReloadTicks => Props.baseReloadTicks;
    public int MaxCharges => parent.maxCharges;
    int ICompWithCharges.RemainingCharges => parent.RemainingCharges;
    public string LabelRemaining => $"{parent.RemainingCharges} / {parent.maxCharges}";

    public bool NeedsReload(bool allowForceReload) =>
        parent.RemainingCharges < parent.maxCharges;

    public int MinAmmoNeeded(bool allowForcedReload) => 1;

    public int MaxAmmoNeeded(bool allowForcedReload) =>
        parent.maxCharges - parent.RemainingCharges;

    public int MaxAmmoAmount() =>
        parent.maxCharges;

    public bool CanBeUsed(out string reason)
    {
        reason = null!;
        return true;
    }

    public string? DisabledReason(int minNeeded, int maxNeeded) => null;

    public void ReloadFrom(Thing ammo)
    {
        if (ammo == null || ammo.Destroyed) return;
        if (parent is not { UsesCharges: true } || parent.RemainingCharges >= parent.maxCharges)
            return;

        int missing = parent.maxCharges - parent.RemainingCharges;
        int toConsume = System.Math.Min(missing, ammo.stackCount);
        if (toConsume <= 0) return;
        ammo.SplitOff(toConsume).Destroy(DestroyMode.Vanish);
        parent.RemainingCharges += toConsume;
        if (Props.soundReload != null && ReloadableThing?.Map != null)
        {
            Props.soundReload.PlayOneShot(new TargetInfo(ReloadableThing.Position, ReloadableThing.Map));
        }
    }

    #endregion

    public JobDef ReloadJobDef => BM_BulwarkonDefOfs.BM_ReloadHealingSmokeGenerator;

    public void StartReloadJob(bool showMessages) =>
        TryStartReloadJob(this, showMessages);

    public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
    {
        base.Apply(target, dest);

        Map map = parent.pawn.Map;
        if (map == null) return;
        IntVec3 pos = parent.pawn.Position;
        GenSpawn.Spawn(Props.smokeGeneratorDef, pos, map);

        if (Props.deployEffect != null)
        {
            Effecter effect = Props.deployEffect.Spawn();
            effect.Trigger(new TargetInfo(pos, map), new TargetInfo(pos, map));
            effect.Cleanup();
        }

        if (Props.deploySound != null)
            Props.deploySound.PlayOneShot(new TargetInfo(pos, map));
    }

    public static void TryStartReloadJob(IReloadableComp reloadable, bool showMessages)
    {
        Pawn? pawn = reloadable.ReloadableThing as Pawn;
        if (pawn?.Map == null) return;
        List<Thing> ammo = ReloadableUtility.FindEnoughAmmo(pawn, pawn.Position, reloadable, forceReload: false);
        if (ammo.NullOrEmpty())
        {
            if (showMessages)
                Messages.Message("BM_NoAmmoAvailable".Translate(reloadable.AmmoDef.label), pawn, MessageTypeDefOf.RejectInput, false);
            return;
        }

        Job job = JobGiver_Reload.MakeReloadJob(reloadable, ammo);
        job.def = ((IAbilityReloadable)reloadable).ReloadJobDef;
        pawn.jobs.TryTakeOrderedJob(job, JobTag.Misc);
    }
}