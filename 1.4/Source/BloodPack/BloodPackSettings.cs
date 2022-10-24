using System.Collections.Generic;

using Verse;

namespace BloodPack
{

    public class BloodPackSettings : ModSettings
    {
        public float BloodDrawn;
        public string BloodDrawnBuffer;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.BloodDrawn, "BloodPack_BloodDrawn", 0.1f);
            Scribe_Values.Look(ref this.BloodDrawnBuffer, "BloodPack_BloodDrawnBuffer", "0.1");
        }
    }
}
