using BM_BulwarkonMechanoid.DefOfs;
using RimWorld;
using RimWorld.Utility;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace BM_BulwarkonMechanoid.AbilityEffects;

public class CompAbilityEffect_DeployBulwarkonShieldProjector : CompAbilityEffect, IAbilityReloadable
{
    public new CompProperties_DeployBulwarkonShieldProjector Props =>
        (CompProperties_DeployBulwarkonShieldProjector)props;

    #region IReloadableComp

    public Thing ReloadableThing => parent.pawn;
    public ThingDef AmmoDef => ThingDef.Named("BroadshieldCore");
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
        parent.maxCharges - parent.RemainingCharges;

    public bool CanBeUsed(out string reason)
    {
        reason = null;
        return true;
    }

    public string? DisabledReason(int minNeeded, int maxNeeded) => null;

    public void ReloadFrom(Thing ammo)
    {
        if (ammo == null || ammo.Destroyed) return;
        if (parent is not { UsesCharges: true } || parent.RemainingCharges >= parent.maxCharges)
            return;

        ammo.SplitOff(1).Destroy(DestroyMode.Vanish);
        parent.RemainingCharges = System.Math.Min(parent.RemainingCharges + 1, parent.maxCharges);
    }

    #endregion


    public JobDef ReloadJobDef => BM_BulwarkonDefOfs.BM_ReloadShieldProjector;

    public void StartReloadJob(bool showMessages) =>
        TryStartReloadJob(this, showMessages);

    public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
    {
        base.Apply(target, dest);

        Map map = parent.pawn.Map;
        if (map == null) return;
        IntVec3 deployPos = parent.pawn.Position;
        GenSpawn.Spawn(Props.shieldProjectorDef, deployPos, map);

        if (Props.deployEffect != null)
        {
            Effecter effect = Props.deployEffect.Spawn();
            effect.Trigger(new TargetInfo(deployPos, map), new TargetInfo(deployPos, map));
            effect.Cleanup();
        }

        if (Props.deploySound != null)
            Props.deploySound.PlayOneShot(new TargetInfo(deployPos, map));
    }

    public static void TryStartReloadJob(IReloadableComp reloadable, bool showMessages)
    {
        Pawn pawn = reloadable.ReloadableThing as Pawn;
        if (pawn?.Map == null) return;

        List<Thing> ammo = ReloadableUtility.FindEnoughAmmo(
            pawn, pawn.Position, reloadable, forceReload: false);

        if (ammo.NullOrEmpty())
        {
            if (showMessages)
                Messages.Message("BM_NoShieldCoreAvailable".Translate(), pawn,
                    MessageTypeDefOf.RejectInput, false);
            return;
        }

        Job job = JobMaker.MakeJob(
            BM_BulwarkonDefOfs.BM_ReloadShieldProjector, reloadable.ReloadableThing);
        job.targetQueueB = ammo.Select(t => new LocalTargetInfo(t)).ToList();
        job.count = System.Math.Min(ammo.Sum(t => t.stackCount),
            reloadable.MaxAmmoNeeded(allowForcedReload: true));

        pawn.jobs.TryTakeOrderedJob(job, JobTag.Misc);
    }
    // TODO 改成用DefOf找
    public static CompAbilityEffect_DeployBulwarkonShieldProjector? FindOnPawn(Pawn pawn)
    {
        if (pawn?.abilities == null) return null;
        foreach (Ability ab in pawn.abilities.abilities)
        {
            if (ab.comps != null)
            {
                foreach (AbilityComp comp in ab.comps)
                {
                    if (comp is CompAbilityEffect_DeployBulwarkonShieldProjector effect)
                        return effect;
                }
            }
        }
        return null;
    }
}