using RimWorld;
using Verse;

namespace BM_BulwarkonMechanoid.DefOfs;

[DefOf]
public class BM_BulwarkonDefOfs
{
    public static JobDef BM_ReloadShieldProjector;
    public static JobDef BM_ReloadHealingSmokeGenerator;
    public static ThingDef Mech_Bulwarkon;
    
    static BM_BulwarkonDefOfs()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(BM_BulwarkonDefOfs));
    }
}