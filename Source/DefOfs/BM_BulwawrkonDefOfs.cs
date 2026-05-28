using RimWorld;
using Verse;

namespace BM_BulwarkonMechanoid.DefOfs;

[DefOf]
public class BM_BulwawrkonDefOfs
{
    public static JobDef BM_ReloadShieldProjector;
    public static JobDef BM_ReloadHealingSmokeGenerator;
    // public static ThingDef BM_HealingSmokeGenerator;
    // public static HediffDef BM_HealingSmokeHediff;
    
    static BM_BulwawrkonDefOfs()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(BM_BulwawrkonDefOfs));
    }
}