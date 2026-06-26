using RimWorld;
using Verse;

namespace BM_BulwarkonMechanoid.DefOfs;

[DefOf]
public class BM_BulwarkonJobDefOfs
{
    public static JobDef BM_ReloadShieldProjector;
    public static JobDef BM_ReloadHealingSmokeGenerator;
    
    static BM_BulwarkonJobDefOfs()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(BM_BulwarkonJobDefOfs));
    }
}

[DefOf]
public class BM_BulwarkonThingDefOfs
{
    public static ThingDef Mech_Bulwarkon;
    
    static BM_BulwarkonThingDefOfs()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(BM_BulwarkonThingDefOfs));
    }
}

[DefOf]
public class BM_BulwarkonPawnKindDefOfs
{
    public static PawnKindDef Mech_Bulwarkon;
    
    static BM_BulwarkonPawnKindDefOfs()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(BM_BulwarkonPawnKindDefOfs));
    }
}