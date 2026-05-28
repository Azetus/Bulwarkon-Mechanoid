using UnityEngine;
using Verse;

namespace BM_BulwarkonMechanoid.Settings.ModSettingUI;

public class ModSettingDrawer
{
     private const float SliderHeight = 28f;
    private const float Gap = 8f;

    public static void Draw(Rect inRect)
    {
        Listing_Standard listing = new();
        listing.Begin(inRect);

        GUI.color = Color.yellow;
        Widgets.Label(listing.GetRect(30f), "BM_RestartWarning".Translate());
        GUI.color = Color.white;

        listing.Gap(Gap);

        DrawSliderRow(listing, "BM_ShieldRadius".Translate(), ref BulwarkonMechanoid.settings.ShieldRadius, 3f, 13f);

        listing.Gap(Gap);
        
        DrawIntSliderRow(listing, "BM_ShieldHP".Translate(), ref BulwarkonMechanoid.settings.ShieldHitPoints, 200, 10000);

        listing.Gap(Gap);
        
        DrawIntSliderRow(listing, "BM_RechargeTicks".Translate(), ref BulwarkonMechanoid.settings.chargeDurationTicks, 0, 18000);

        listing.Gap(Gap);

        DrawIntSliderRow(listing, "BM_ShieldRegenPerInterval_Temp".Translate(), ref BulwarkonMechanoid.settings.ShieldHitPointsPerInterval, 1, 100);

        listing.Gap(Gap);

        DrawIntSliderRow(listing, "BM_ShieldRechargeIntervalTicks_Temp".Translate(), ref BulwarkonMechanoid.settings.ShieldRechargeIntervalTicks, 60, 1800);

        listing.Gap(Gap);

        DrawIntSliderRow(listing, "BM_DisarmedByEmpForTicks_Temp".Translate(), ref BulwarkonMechanoid.settings.DisarmedByEmpForTicks, 0, 1800);

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
        value = Widgets.HorizontalSlider(sliderRect, value, min, max, true,value.ToString("0.00"));
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