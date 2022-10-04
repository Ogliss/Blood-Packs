using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BloodPack
{
	public class MakeBloodPack : Recipe_InstallImplant
	{
		public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
		{
			bool flag = billDoer != null;
			if (flag)
			{
				bool flag2 = !base.CheckSurgeryFail(billDoer, pawn, ingredients, part, bill);
				if (flag2)
				{
					TaleRecorder.RecordTale(TaleDefOf.DidSurgery, new object[]
					{
						billDoer,
						pawn
					});
					this.GiveBloodLoss(pawn);
					this.SpawnBloodPack(pawn, billDoer);
					if (pawn.Faction != null && billDoer != null && billDoer.Faction != null)
					{
						Faction faction = pawn.Faction;
						Faction faction2 = billDoer.Faction;
						int goodwillChange = -10;
						GlobalTargetInfo? lookTarget = new GlobalTargetInfo?(pawn);
						faction.TryAffectGoodwillWith(faction2, goodwillChange, true, true, HistoryEventDefOf.BloodPack_DrewBlood, lookTarget);
					}
				}
			}
		}

		private void GiveBloodLoss(Pawn pawn)
		{
			Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.BloodLoss, pawn, null);
			hediff.Severity = BloodPackMod.packSettings.BloodDrawn;
			pawn.health.AddHediff(hediff, null, null, null);
		}

		private void SpawnBloodPack(Pawn pawn, Pawn billDoer)
		{
			Thing thing = ThingMaker.MakeThing(ThingDefOf.BloodPack, null);
			GenPlace.TryPlaceThing(thing, pawn.Position, billDoer.Map, ThingPlaceMode.Near, null, null);
		}
	}
}
