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
				firstHediffOfDef.Severity -= bloodAmount;
				if (firstHediffOfDef.Severity < 0f)
				{
					usedBy.health.RemoveHediff(firstHediffOfDef);
				}
				Messages.Message(Translator.Translate("MessageHediffCuredByBloodPack"), usedBy, MessageTypeDefOf.PositiveEvent, true);
			}
		}

		public override string CompInspectStringExtra()
		{
			return $"Contains {bloodAmount} blood";
			return base.CompInspectStringExtra();
		}

		public override void PostExposeData()
		{
			base.PostExposeData();
            Scribe_Values.Look(ref this.bloodAmount, "BloodPack_BloodDrawn", 0.1f);
        }
		public float bloodAmount;
	}
}
