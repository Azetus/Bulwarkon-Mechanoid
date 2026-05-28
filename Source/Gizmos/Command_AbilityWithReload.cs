using BM_BulwarkonMechanoid.AbilityEffects;
using RimWorld;
using Verse;

namespace BM_BulwarkonMechanoid.Gizmos;

public class Command_AbilityWithReload : Command_Ability
{
    public Command_AbilityWithReload(Ability ability, Pawn pawn) : base(ability, pawn)
    {
    }
    
    public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions
    {
        get
        {
            if (ability.comps == null)
                yield break;

            foreach (AbilityComp comp in ability.comps)
            {
                if (comp is IAbilityReloadable reloadable
                    && reloadable.NeedsReload(allowForceReload: false))
                {
                    yield return new FloatMenuOption(
                        "BM_Reload".Translate(),
                        () => reloadable.StartReloadJob(showMessages: true));
                    yield break;
                }
            }
        }
    }
}