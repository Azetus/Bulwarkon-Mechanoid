using BM_BulwarkonMechanoid.Settings;
using BM_BulwarkonMechanoid.Settings.ModSettingUI;
using UnityEngine;
using Verse;

namespace BM_BulwarkonMechanoid;

public class BulwarkonMechanoid : Mod
{
    public static BulwarkonMechanoidModSettings settings;

    public BulwarkonMechanoid(ModContentPack content) : base(content)
    {
        settings = GetSettings<BulwarkonMechanoidModSettings>();
        Log.Message("<color=cyan>[BulwarkonMechanoid]</color> is loaded!");
    }

    public override string SettingsCategory()
    {
        return "BM_BulwarkonMechanoid_ModTitle".Translate();
    }


    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        ModSettingDrawer.Draw(inRect);
    }
}