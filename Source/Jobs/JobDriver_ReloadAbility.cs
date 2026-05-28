using BM_BulwarkonMechanoid.AbilityEffects;
using RimWorld;
using Verse;
using Verse.AI;

namespace BM_BulwarkonMechanoid.Jobs;

public class JobDriver_ReloadAbility: JobDriver
{
     private const TargetIndex AmmoQueueIndex = TargetIndex.B;

    private IAbilityReloadable? reloadable;

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        pawn.ReserveAsManyAsPossible(job.GetTargetQueue(AmmoQueueIndex), job);
        return true;
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        reloadable = FindReloadable(pawn);
        this.FailOn(() => reloadable == null);
        int ticks = reloadable.BaseReloadTicks;

        Toil getNext = Toils_JobTransforms.ExtractNextTargetFromQueue(AmmoQueueIndex);
        yield return getNext;

        yield return Toils_Goto.GotoThing(AmmoQueueIndex, PathEndMode.ClosestTouch)
            .FailOnDespawnedNullOrForbidden(AmmoQueueIndex);

        yield return Toils_General.WaitWith(AmmoQueueIndex, ticks, true, true)
            .WithProgressBarToilDelay(AmmoQueueIndex);

        Toil consume = new Toil();
        consume.initAction = () =>
        {
            Thing ammo = job.GetTarget(AmmoQueueIndex).Thing;
            reloadable.ReloadFrom(ammo);
        };
        consume.defaultCompleteMode = ToilCompleteMode.Instant;
        yield return consume;

        yield return Toils_Jump.JumpIf(getNext, HasMoreToReload);
    }

    private bool HasMoreToReload()
    {
        if (reloadable == null) return false;
        if (!reloadable.NeedsReload(allowForceReload: true)) return false;
        if (job.GetTargetQueue(AmmoQueueIndex).NullOrEmpty()) return false;
        return true;
    }

    private static IAbilityReloadable? FindReloadable(Pawn pawn)
    {
        if (pawn.abilities?.abilities == null) return null;
        foreach (Ability ab in pawn.abilities.abilities)
        {
            if (ab.comps == null) continue;
            foreach (AbilityComp comp in ab.comps)
            {
                if (comp is IAbilityReloadable reloadable && reloadable.NeedsReload(false))
                    return reloadable;
            }
        }
        return null;
    }
}