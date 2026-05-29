using RimWorld;
using RimWorld.Utility;
using Verse;

namespace BM_BulwarkonMechanoid.AbilityEffects;

public interface IAbilityReloadable : IReloadableComp
{
    JobDef ReloadJobDef { get; }
    void StartReloadJob(bool showMessages);
}

public static class AbilityReloadUtility
{
    public static IAbilityReloadable? FindReloadableComp(Ability ability)
    {
        if (ability.comps == null) return null;
        foreach (AbilityComp comp in ability.comps)
        {
            if (comp is IAbilityReloadable reloadable)
                return reloadable;
        }
        return null;
    }

    public static IEnumerable<IAbilityReloadable> GetReloadableAbilities(Pawn pawn)
    {
        if (pawn.abilities?.abilities == null) yield break;
        foreach (Ability ab in pawn.abilities.abilities)
        {
            var reloadable = FindReloadableComp(ab);
            if (reloadable != null)
                yield return reloadable;
        }
    }
}