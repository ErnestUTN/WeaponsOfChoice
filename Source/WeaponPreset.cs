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

            
            if (!SearchPriorityStrings.NullOrEmpty<string>())
            for(int i=0; i<3; i++)
            {
                SearchPriorityStrings[i] = SearchPriorityThingDefs[i]?.defName ?? "null";
            }
            Scribe_Collections.Look(ref this.SearchPriorityStrings, "WPriorityWeapons");


            if (!SearchPriorityStrings.NullOrEmpty<string>())
            for (int i = 0; i < 3; i++)
            {
                SearchPriorityThingDefs[i] = SearchPriorityStrings[i] == "null" ? null : DefDatabase<ThingDef>.GetNamedSilentFail(SearchPriorityStrings[i]);
            }
        }


        public string GetUniqueLoadID()
        {
            return "WeaponPreset_" + this.label + this.uniqueId.ToString();
        }


        public int uniqueId;


        public string label;


        public ThingFilter filter = new ThingFilter();
        public ThingDef[] SearchPriorityThingDefs = new ThingDef[3]
        {
            
            null,
            null,
            null,

        }; //default priority weapons --> to Avoid null exceptions for suck fake!!

        private List<string> SearchPriorityStrings = new List<string>()
        {
            "null",
            "null",
            "null"
        };



        public static readonly Regex ValidNameRegex = new Regex("^[\\p{L}0-9 '\\-]*$");
    }
}
