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
					this.RemoveBloodLoss(pawn, (CompUseEffect_BloodPack)ingredients[0].TryGetComp<CompUseEffect_BloodPack>());
				}
			}
		}

		private void RemoveBloodLoss(Pawn pawn, CompUseEffect_BloodPack bloodpack)
		{
			Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.BloodLoss, false);
			firstHediffOfDef.Severity -= bloodpack.bloodAmount;
			if (firstHediffOfDef.Severity<0f)
			{
				pawn.health.RemoveHediff(firstHediffOfDef);
			}
			Messages.Message(Translator.Translate("MessageHediffCuredByBloodPack"), pawn, MessageTypeDefOf.PositiveEvent, true);
		}

	}
}
