using RimWorld;
using UnityEngine;
using Verse;

namespace BM_BulwarkonMechanoid.Settings.ModSettingUI;

public class ModSettingDrawer
{
    private const float SliderHeight = 28f;
    private const float Gap = 8f;
    private const float ButtonHeight = 36f;

    public static void Draw(Rect inRect)
    {
        Listing_Standard listing = new();
        listing.Begin(inRect);

        listing.Gap(Gap);

        DrawSliderRow(listing, "BM_ShieldRadius".Translate(), ref BulwarkonMechanoid.settings.ShieldRadius, 3f, 13f);

        listing.Gap(Gap);

        DrawIntSliderRow(listing, "BM_ShieldHP".Translate(), ref BulwarkonMechanoid.settings.ShieldHitPoints, 200, 10000);

        listing.Gap(Gap);

        DrawIntSliderRow(listing, "BM_ChargeDurationTicks".Translate(), ref BulwarkonMechanoid.settings.chargeDurationTicks, 0, 18000);

        listing.Gap(Gap);

        DrawIntSliderRow(listing, "BM_ShieldRegenPerInterval".Translate(), ref BulwarkonMechanoid.settings.ShieldHitPointsPerInterval, 1, 100);

        listing.Gap(Gap);

        DrawIntSliderRow(listing, "BM_ShieldRechargeIntervalTicks".Translate(), ref BulwarkonMechanoid.settings.RechargeHitPointsIntervalTicks, 1,
            1800);

        listing.Gap(Gap);

        DrawIntSliderRow(listing, "BM_DisarmedByEmpForTicks".Translate(), ref BulwarkonMechanoid.settings.DisarmedByEmpForTicks, 0, 1800);

        listing.Gap(Gap);
        listing.GapLine();

        DrawIntSliderRow(listing, "BM_SmokeHealInterval".Translate(), ref BulwarkonMechanoid.settings.SmokeHealIntervalTicks, 1, 6000);

        listing.Gap(Gap);

        DrawIntSliderRow(listing, "BM_SmokeHealAmount".Translate(), ref BulwarkonMechanoid.settings.SmokeHealAmount, 1, 20);

        listing.Gap(Gap);
        listing.GapLine();

        listing.Label("BM_ShieldAmmoLabel".Translate());
        if (listing.RadioButton("BM_ShieldAmmoShieldCore".Translate(),
                BulwarkonMechanoid.settings.ShieldAmmo == ShieldAmmoType.BroadshieldCore))
            BulwarkonMechanoid.settings.ShieldAmmo = ShieldAmmoType.BroadshieldCore;
        if (listing.RadioButton("BM_ShieldAmmoAdvancedComponent".Translate(),
                BulwarkonMechanoid.settings.ShieldAmmo == ShieldAmmoType.AdvancedComponent))
            BulwarkonMechanoid.settings.ShieldAmmo = ShieldAmmoType.AdvancedComponent;

        listing.GapLine();

        listing.Label("BM_SmokeAmmoLabel".Translate());
        if (listing.RadioButton("BM_SmokeAmmoUltratech".Translate(),
                BulwarkonMechanoid.settings.SmokeAmmo == SmokeAmmoType.MedicineUltratech))
            BulwarkonMechanoid.settings.SmokeAmmo = SmokeAmmoType.MedicineUltratech;
        if (listing.RadioButton("BM_SmokeAmmoRegular".Translate(),
                BulwarkonMechanoid.settings.SmokeAmmo == SmokeAmmoType.MedicineIndustrial))
            BulwarkonMechanoid.settings.SmokeAmmo = SmokeAmmoType.MedicineIndustrial;

        listing.Gap(Gap);
        listing.GapLine();

        listing.CheckboxLabeled("BM_AllowHostileBulwarkon".Translate(), ref BulwarkonMechanoid.settings.AllowHostileBulwarkon);

        listing.Gap(Gap);

        if (Widgets.ButtonText(listing.GetRect(ButtonHeight), "BM_ResetDefaults".Translate()))
        {
            BulwarkonMechanoid.settings.ResetToDefaults();
        }

        listing.End();
    }

    private static void DrawSliderRow(Listing_Standard listing, string label, ref float value, float min, float max)
    {
        Rect rowRect = listing.GetRect(SliderHeight);
        Rect labelRect = rowRect.LeftPart(0.35f);
        Rect inputRect = rowRect.RightPart(0.18f);
        Rect sliderRect = new(
            rowRect.x + labelRect.width + 4f,
            rowRect.y,
            rowRect.width - labelRect.width - inputRect.width - 8f,
            rowRect.height
        );

        Widgets.Label(labelRect, label);
        value = Widgets.HorizontalSlider(sliderRect, value, min, max, true, value.ToString("0.00"));
        string buffer = value.ToString("0.00");
        Widgets.TextFieldNumeric(inputRect, ref value, ref buffer, min, max);
    }

    private static void DrawIntSliderRow(Listing_Standard listing, string label, ref int value, int min, int max)
    {
        float floatVal = value;
        Rect rowRect = listing.GetRect(SliderHeight);
        Rect labelRect = rowRect.LeftPart(0.35f);
        Rect inputRect = rowRect.RightPart(0.18f);
        Rect sliderRect = new(
            rowRect.x + labelRect.width + 4f,
            rowRect.y,
            rowRect.width - labelRect.width - inputRect.width - 8f,
            rowRect.height
        );

        Widgets.Label(labelRect, label);
        floatVal = Widgets.HorizontalSlider(sliderRect, floatVal, min, max, true, value.ToString("0"), null, null, 1f);
        string buffer = value.ToString("0");
        Widgets.TextFieldNumeric(inputRect, ref floatVal, ref buffer, min, max);
        value = Mathf.RoundToInt(floatVal);
    }
}