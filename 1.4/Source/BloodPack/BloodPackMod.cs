﻿using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Verse;

namespace BloodPack
{

	public sealed class BloodPackMod : Mod
	{
		public static BloodPackSettings packSettings;
		public BloodPackMod(ModContentPack mcp) : base(mcp)
		{
			packSettings = GetSettings<BloodPackSettings>();
		}

		public override string SettingsCategory()
		{
			return "BloodPack".Translate();
		}

		public override void DoSettingsWindowContents(Rect rect)
		{
			Listing_Standard list = new Listing_Standard()
			{
				ColumnWidth = rect.width
			};
			list.Begin(rect);
			list.LabeledScrollbarSetting("BloodPackkSettings_BloodDrawn".Translate(packSettings.BloodDrawn.ToStringPercent()), ref packSettings.BloodDrawn);
			list.End();
		}

		public override void WriteSettings()
		{
			RecipeDefOf.BloodPack.workAmount = 4000 * packSettings.BloodDrawn;
			base.WriteSettings();
		}

	}
}
