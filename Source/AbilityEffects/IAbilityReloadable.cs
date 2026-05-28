using RimWorld.Utility;
using Verse;

namespace BM_BulwarkonMechanoid.AbilityEffects;

public interface IAbilityReloadable : IReloadableComp
{
    JobDef ReloadJobDef { get; }
    void StartReloadJob(bool showMessages);
}