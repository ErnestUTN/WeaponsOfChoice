using RimWorld;
using Verse;
namespace WeaponsOfChoice
{
    public class Pawn_WeaponPresetTracker : ThingComp
    {
        Pawn Pawn => (Pawn)parent;


        public WeaponPreset CurrentWeaponPreset
        {
            get
            {
                if (this.curWeaponPreset == null)
                {
                    this.curWeaponPreset = Current.Game.GetComponent<WeaponPresetDatabase>().DefaultWeaponPreset();
                    
                }
                return this.curWeaponPreset;
            }
            set
            {
                if (this.curWeaponPreset == value)
                {
                    return;
                }
                this.curWeaponPreset = value;
                this.nextWeaponPresetOptimizeTick = Find.TickManager.TicksGame;
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref this.nextWeaponPresetOptimizeTick, "nextWeaponPresetOptimizeTick", -99999);
            Scribe_References.Look<WeaponPreset>(ref this.curWeaponPreset, "curWeaponPreset", false);
            Scribe_Deep.Look<WeaponPresetForcedHandler>(ref this.forcedHandler, "overrideHandler", new object[0]);
        }

        
        private WeaponPreset curWeaponPreset;
        public int nextWeaponPresetOptimizeTick = -99999;
      
        public WeaponPresetForcedHandler forcedHandler = new WeaponPresetForcedHandler();

    }
}