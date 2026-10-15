using BM_BulwarkonMechanoid.AbilityEffects;
using RimWorld;
using Verse;
using Verse.AI;

namespace BM_BulwarkonMechanoid.Jobs;

public class JobDriver_ReloadAbility : JobDriver
{
    private const TargetIndex PawnInd = TargetIndex.A;
    private const TargetIndex AmmoQueueIndex = TargetIndex.B;

    private IAbilityReloadable? reloadable;

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        pawn.ReserveAsManyAsPossible(job.GetTargetQueue(AmmoQueueIndex), job);
        return true;
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        foreach (var r in AbilityReloadUtility.GetReloadableAbilities(pawn))
        {
            if (r.ReloadJobDef == job.def)
            {
                reloadable = r;
                break;
            }
        }
        if (reloadable == null) yield break;

        this.FailOnDestroyedOrNull(PawnInd);
        this.FailOnIncapable(PawnCapacityDefOf.Manipulation);
        this.FailOn(() => reloadable == null);
        this.FailOn(() => !reloadable.NeedsReload(allowForceReload: true));
        this.FailOn(() =>
        {
            // Target B is assigned when the next ammo stack is extracted from the queue.
            Thing ammo = job.GetTarget(AmmoQueueIndex).Thing;
            return ammo != null && ammo.def != reloadable.AmmoDef;
        });

        Toil fetchNext = Toils_General.Label();
        yield return fetchNext;
        yield return Toils_JobTransforms.ExtractNextTargetFromQueue(AmmoQueueIndex);
        yield return Toils_Goto.GotoThing(AmmoQueueIndex, PathEndMode.ClosestTouch)
            .FailOnDespawnedNullOrForbidden(AmmoQueueIndex);
        yield return Toils_General.Wait(reloadable.BaseReloadTicks)
            .WithProgressBarToilDelay(AmmoQueueIndex);
        Toil reload = ToilMaker.MakeToil("Reload");
        reload.initAction = () =>
        {
            Thing ammo = job.GetTarget(AmmoQueueIndex).Thing;
            if (ammo != null && !ammo.Destroyed)
                reloadable.ReloadFrom(ammo);
        };
        reload.defaultCompleteMode = ToilCompleteMode.Instant;
        yield return reload;

        yield return Toils_Jump.JumpIf(fetchNext, () =>
            reloadable.NeedsReload(true) &&
            !job.GetTargetQueue(AmmoQueueIndex).NullOrEmpty());
    }
}
