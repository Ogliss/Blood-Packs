using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BloodPack
{
	// Token: 0x02000003 RID: 3
	public class MakeBloodPack : Recipe_InstallImplant
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
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
					MakeBloodPack.GiveBloodLoss(pawn);
					MakeBloodPack.SpawnBloodPack(pawn, billDoer);
					if (pawn.Faction != null && billDoer != null && billDoer.Faction != null)
					{
						Faction faction = pawn.Faction;
						Faction faction2 = billDoer.Faction;
						int goodwillChange = -10;
						string reason = Translator.Translate("GoodwillChangedReason_MakeBloodPack");
						GlobalTargetInfo? lookTarget = new GlobalTargetInfo?(pawn);
						faction.TryAffectGoodwillWith(faction2, goodwillChange, true, true, reason, lookTarget);
					}
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000211C File Offset: 0x0000031C
		private static void GiveBloodLoss(Pawn pawn)
		{
			Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.BloodLoss, pawn, null);
			hediff.Severity = BloodPackSettings.BloodDrawn;
			pawn.health.AddHediff(hediff, null, null, null);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000215C File Offset: 0x0000035C
		private static void SpawnBloodPack(Pawn pawn, Pawn billDoer)
		{
			Thing thing = ThingMaker.MakeThing(ThingDefOf.BloodPack, null);
			GenPlace.TryPlaceThing(thing, pawn.Position, billDoer.Map, ThingPlaceMode.Near, null, null);
		}
	}
}
