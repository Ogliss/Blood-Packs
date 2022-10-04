using System;
using RimWorld;
using Verse;

namespace BloodPack
{
	public class CompUseEffect_BloodPack : CompUseEffect_FixWorstHealthCondition
	{
		public override void DoEffect(Pawn usedBy)
		{
			this.RemoveBloodLoss(usedBy);
		}

		private void RemoveBloodLoss(Pawn usedBy)
		{
			Hediff firstHediffOfDef = usedBy.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.BloodLoss, false);
            if (firstHediffOfDef != null)
            {
				firstHediffOfDef.Severity -= BloodPackMod.packSettings.BloodDrawn;
				if (firstHediffOfDef.Severity < 0f)
				{
					usedBy.health.RemoveHediff(firstHediffOfDef);
				}
				Messages.Message(Translator.Translate("MessageHediffCuredByBloodPack"), usedBy, MessageTypeDefOf.PositiveEvent, true);
			}
		}

	}
}
