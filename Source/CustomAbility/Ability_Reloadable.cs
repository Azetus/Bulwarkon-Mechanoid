using RimWorld;
using Verse;

namespace BM_BulwarkonMechanoid.CustomAbility;

public class Ability_Reloadable : Ability
{
    private int lastAICastTick = -1;

    public Ability_Reloadable()
    {
    }

    public Ability_Reloadable(Pawn pawn) : base(pawn)
    {
    }

    public Ability_Reloadable(Pawn pawn, Precept sourcePrecept) : base(pawn, sourcePrecept)
    {
    }

    public Ability_Reloadable(Pawn pawn, AbilityDef def) : base(pawn, def)
    {
    }

    public Ability_Reloadable(Pawn pawn, Precept sourcePrecept, AbilityDef def) : base(pawn, sourcePrecept, def)
    {
    }

    public override AcceptanceReport CanCast
    {
        get
        {
            if (pawn.Faction != null && pawn.Faction.HostileTo(Faction.OfPlayer)
                && Find.TickManager.TicksGame - lastAICastTick < 1200)
            {
                return "Ability On Cooldown";
            }
            return base.CanCast;
        }
    }

    public override bool Activate(LocalTargetInfo target, LocalTargetInfo dest)
    {
        if (pawn.Faction != null && pawn.Faction.HostileTo(Faction.OfPlayer))
        {
            lastAICastTick = Find.TickManager.TicksGame;
        }
        return base.Activate(target, dest);
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref lastAICastTick, "lastAICastTick", -1);
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