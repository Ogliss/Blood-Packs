using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BloodPack
{
	// Token: 0x02000004 RID: 4
	public class UseBloodPack : Recipe_InstallImplant
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002198 File Offset: 0x00000398
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
					UseBloodPack.RemoveBloodLoss(pawn);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021F0 File Offset: 0x000003F0
		private static void RemoveBloodLoss(Pawn pawn)
		{
			Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.BloodLoss, false);
			firstHediffOfDef.Severity -= BloodPackSettings.BloodDrawn;
			if (firstHediffOfDef.Severity<0f)
			{
				pawn.health.RemoveHediff(firstHediffOfDef);
			}
			Messages.Message(Translator.Translate("MessageHediffCuredByBloodPack"), pawn, MessageTypeDefOf.PositiveEvent, true);
		}

	}
}
