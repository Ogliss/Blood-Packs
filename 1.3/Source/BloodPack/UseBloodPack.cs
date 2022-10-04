using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BloodPack
{
	public class UseBloodPack : Recipe_InstallImplant
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
					this.RemoveBloodLoss(pawn);
				}
			}
		}

		private void RemoveBloodLoss(Pawn pawn)
		{
			Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.BloodLoss, false);
			firstHediffOfDef.Severity -= BloodPackMod.packSettings.BloodDrawn;
			if (firstHediffOfDef.Severity<0f)
			{
				pawn.health.RemoveHediff(firstHediffOfDef);
			}
			Messages.Message(Translator.Translate("MessageHediffCuredByBloodPack"), pawn, MessageTypeDefOf.PositiveEvent, true);
		}

	}
}
