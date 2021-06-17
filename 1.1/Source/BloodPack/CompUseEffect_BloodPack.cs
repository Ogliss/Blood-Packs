using System;
using RimWorld;
using Verse;

namespace BloodPack
{
	// Token: 0x02000005 RID: 5
	public class CompUseEffect_BloodPack : CompUseEffect_FixWorstHealthCondition
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002288 File Offset: 0x00000488
		public override void DoEffect(Pawn usedBy)
		{
			CompUseEffect_BloodPack.RemoveBloodLoss(usedBy);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000229C File Offset: 0x0000049C
		private static void RemoveBloodLoss(Pawn usedBy)
		{
			Hediff firstHediffOfDef = usedBy.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.BloodLoss, false);
			firstHediffOfDef.Severity -= BloodPackSettings.BloodDrawn;
			if (firstHediffOfDef.Severity < 0f)
			{
				usedBy.health.RemoveHediff(firstHediffOfDef);
			}
			Messages.Message(Translator.Translate("MessageHediffCuredByBloodPack"), usedBy, MessageTypeDefOf.PositiveEvent, true);
		}

	}
}
