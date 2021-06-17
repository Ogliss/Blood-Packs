using System.Collections.Generic;

using Verse;

namespace BloodPack
{

    public class BloodPackSettings : ModSettings
    {
        internal static float BloodDrawn = 0.25f;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref BloodDrawn, "BloodPack_BloodDrawn", 0.25f);
        }
    }
}
