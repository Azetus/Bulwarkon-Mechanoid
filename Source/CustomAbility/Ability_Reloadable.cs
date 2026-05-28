using RimWorld;
using Verse;

namespace BM_BulwarkonMechanoid.CustomAbility;

public class Ability_Reloadable : RimWorld.Ability
{
    public Ability_Reloadable()
    {
    }

    public Ability_Reloadable(Pawn pawn, AbilityDef def) : base(pawn, def)
    {
    }

    public Ability_Reloadable(Pawn pawn, Precept sourcePrecept, AbilityDef def) : base(pawn, sourcePrecept, def)
    {
    }

    public override bool GizmoDisabled(out string reason)
    {
        if (UsesCharges && RemainingCharges <= 0)
        {
            reason = null!;
            return false;
        }

        return base.GizmoDisabled(out reason);
    }
}