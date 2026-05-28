using BM_BulwarkonMechanoid.AbilityEffects;
using RimWorld;
using RimWorld.Utility;
using UnityEngine;
using Verse;
using Verse.AI;

namespace BM_BulwarkonMechanoid.Jobs;

public class JobGiver_AutoReloadAbilities : ThinkNode_JobGiver
{
    public override float GetPriority(Pawn pawn) => 5.9f;

    protected override Job TryGiveJob(Pawn pawn)
    {
        if (pawn.Drafted) return null;
        if (pawn.abilities?.abilities == null) return null;

        foreach (Ability ability in pawn.abilities.abilities)
        {
            if (ability.comps == null) continue;
            foreach (AbilityComp comp in ability.comps)
            {
                if (comp is not IAbilityReloadable reloadable) continue;
                if (!reloadable.NeedsReload(allowForceReload: false)) continue;

                List<Thing> ammo = ReloadableUtility.FindEnoughAmmo(
                    pawn, pawn.Position, reloadable, forceReload: false);
                if (ammo.NullOrEmpty()) continue;

                Job job = JobMaker.MakeJob(reloadable.ReloadJobDef, reloadable.ReloadableThing);
                job.targetQueueB = ammo.Select(t => new LocalTargetInfo(t)).ToList();
                job.count = Mathf.Min(ammo.Sum(t => t.stackCount),
                    reloadable.MaxAmmoNeeded(allowForcedReload: true));
                return job;
            }
        }

        return null;
    }
}