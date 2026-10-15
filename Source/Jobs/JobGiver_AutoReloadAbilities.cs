using BM_BulwarkonMechanoid.AbilityEffects;
using RimWorld;
using RimWorld.Utility;
using Verse;
using Verse.AI;

namespace BM_BulwarkonMechanoid.Jobs;

public class JobGiver_AutoReloadAbilities : ThinkNode_JobGiver
{
    public override float GetPriority(Pawn pawn) => 5.9f;

    protected override Job TryGiveJob(Pawn pawn)
    {
        if (pawn.Drafted || !pawn.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation))
            return null;

        foreach (var reloadable in AbilityReloadUtility.GetReloadableAbilities(pawn))
        {
            if (!reloadable.NeedsReload(false)) continue;

            List<Thing> ammo = ReloadableUtility.FindEnoughAmmo(
                pawn, pawn.Position, reloadable, forceReload: false);
            if (ammo.NullOrEmpty()) continue;

            Job job = JobGiver_Reload.MakeReloadJob(reloadable, ammo);
            job.def = reloadable.ReloadJobDef;
            return job;
        }

        return null;
    }
}
