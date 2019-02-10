using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace WeaponsOfChoice
{
    public sealed class WeaponPresetDatabase : GameComponent
    {
        public WeaponPresetDatabase(Game game)
        {
            
        }

        public override void FinalizeInit()
        {
            if (!initialized)
            {
                GenerateStartingWeaponPresets();
                initialized = true;
            }
        }

        public List<WeaponPreset> AllWeaponPresets
        {
            get
            {
                return this.WeaponPresets;
            }
        }

        override public void ExposeData()
        {
            Scribe_Values.Look(ref initialized, "initialized", false);
            Scribe_Collections.Look<WeaponPreset>(ref this.WeaponPresets, "weaponPresets", LookMode.Deep, new object[0]);
        }

        public WeaponPreset DefaultWeaponPreset()
        {
            if (this.WeaponPresets.Count == 0)
            {
                this.MakeNewWeaponPreset();
            }
            return this.WeaponPresets[0];
        }

        public AcceptanceReport TryDelete(WeaponPreset weaponPreset)
        {
            foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive)
            {
                
                if (pawn.TryGetComp<Pawn_WeaponPresetTracker>()?.CurrentWeaponPreset == weaponPreset)

                    return new AcceptanceReport("WeaponPresetInUse".Translate(pawn));
 
            }
            foreach (Pawn pawn2 in PawnsFinder.AllMapsWorldAndTemporary_AliveOrDead)
            {
                if (pawn2.TryGetComp<Pawn_WeaponPresetTracker>() is Pawn_WeaponPresetTracker Tracker && Tracker.CurrentWeaponPreset == weaponPreset)

                    Tracker.CurrentWeaponPreset = null;
            }
            this.WeaponPresets.Remove(weaponPreset);
            return AcceptanceReport.WasAccepted;
        }

        public WeaponPreset MakeNewWeaponPreset()
        {
            int num;
            if (this.WeaponPresets.Any<WeaponPreset>())
            {
                num = this.WeaponPresets.Max((WeaponPreset o) => o.uniqueId) + 1;
            }
            else
            {
                num = 1;
            }
            int uniqueId = num;
            WeaponPreset _weaponPreset = new WeaponPreset(uniqueId, "WeaponPreset".Translate() + " " + uniqueId.ToString());
            _weaponPreset.filter.SetAllow(ThingCategoryDefOf.Weapons, true);
            this.WeaponPresets.Add(_weaponPreset);
            return _weaponPreset;
        }

        private void GenerateStartingWeaponPresets()
        {
            WeaponPreset _weaponPreset1 = this.MakeNewWeaponPreset();
            _weaponPreset1.label = "WOC.WeaponPresetNone".Translate();
            _weaponPreset1.filter.SetDisallowAll(null, null);
  
        }

        private List<WeaponPreset> WeaponPresets = new List<WeaponPreset>();
        private bool initialized = false;
    }
}
