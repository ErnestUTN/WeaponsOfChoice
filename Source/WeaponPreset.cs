using System;
using System.Text.RegularExpressions;
using Verse;
using RimWorld;
using System.Collections.Generic;

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
            Scribe_Collections.Look(ref this.SearchPriorityThingDefs, "WPriorityWeapons", LookMode.Deep);
        }


        public string GetUniqueLoadID()
        {
            return "WeaponPreset_" + this.label + this.uniqueId.ToString();
        }


        public int uniqueId;


        public string label;


        public ThingFilter filter = new ThingFilter();
        public List<ThingDef> SearchPriorityThingDefs = new List<ThingDef>() { null, null, null };
        

        public static readonly Regex ValidNameRegex = new Regex("^[\\p{L}0-9 '\\-]*$");
    }
}
