using System;
using System.Text.RegularExpressions;
using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
namespace WeaponsOfChoice
{

    public class WeaponPreset : IExposable, ILoadReferenceable
    {

        public WeaponPreset()
        {
            
        }


        public WeaponPreset(int uniqueId, string label)
        {
            this.uniqueId = uniqueId;
            this.label = label;
        }


        public void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.uniqueId, "WuniqueId", 0, false);
            Scribe_Values.Look<string>(ref this.label, "Wlabel", null, false);
            Scribe_Deep.Look<ThingFilter>(ref this.filter, "Wfilter", new object[0]);
            
            List<ThingDef> defs = SearchPriorityThingDefs.ToList();
            Scribe_Collections.Look(ref defs, "WPriorityWeapons");

            if (defs != null)
            {
                SearchPriorityThingDefs = new ThingDef[3];
                for (int i = 0; i < defs.Count(); i++)
                {
                    SearchPriorityThingDefs[i] = defs[i];
                }
            }          
        }


        public string GetUniqueLoadID()
        {
            return "WeaponPreset_" + this.label + this.uniqueId.ToString();
        }


        public int uniqueId;


        public string label;


        public ThingFilter filter = new ThingFilter();
        public ThingDef[] SearchPriorityThingDefs = new ThingDef[3];
        

        public static readonly Regex ValidNameRegex = new Regex("^[\\p{L}0-9 '\\-]*$");
    }
}
