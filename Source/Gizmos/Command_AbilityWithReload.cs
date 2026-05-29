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
            var reloadable = AbilityReloadUtility.FindReloadableComp(ability);
            if (reloadable != null && reloadable.NeedsReload(allowForceReload: false))
            {
                yield return new FloatMenuOption(
                    "BM_Reload".Translate(),
                    () => reloadable.StartReloadJob(showMessages: true));
            }
        }
    }
}