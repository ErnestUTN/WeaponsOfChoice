using System.Collections.Generic;
using System.Linq;
using Verse;

namespace WeaponsOfChoice
{
    [StaticConstructorOnStartup]
    public static class WeaponOfChoiceUtility
    {
        /// <summary>[StaticConstructorOnStartup] class
        /// </summary>
        static WeaponOfChoiceUtility()
        {
            // Add WeaponPresetTrackers to all appropriate pawns
            foreach (ThingDef tDef in DefDatabase<ThingDef>.AllDefs.Where(t => t.race?.Humanlike == true))
            {
                Log.Message($"Attaching WeaponPresetTrackers to {tDef}");
                if (tDef.comps == null)
                    tDef.comps = new List<CompProperties>();
                tDef.comps.Add(new CompProperties(typeof(Pawn_WeaponPresetTracker)));
            }
        }


    }
}
