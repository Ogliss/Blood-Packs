using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace BloodPack
{
	public static class ExtentionMethods
    {
		public static void LabeledScrollbarSetting(this Listing_Standard listing_Standard, string label, ref float setting, string tooltip = null)
		{
			Rect rect = listing_Standard.GetRect(Text.LineHeight).Rounded();
			Rect SliderOffset = rect.RightHalf().Rounded();

			Widgets.Label(rect, label);
			setting = Widgets.HorizontalSlider(
			SliderOffset,
			setting, 0.01f, 1f, true, roundTo: 0.01f);
			if (!tooltip.NullOrEmpty())
			{
				if (Mouse.IsOver(rect))
				{
					Widgets.DrawHighlight(rect);
				}
				TooltipHandler.TipRegion(rect, tooltip);
			}
		}

	}
}
