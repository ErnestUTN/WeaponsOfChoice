using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
namespace WeaponsOfChoice
{
    public class WeaponPresetForcedHandler : IExposable
    {
        public bool SomethingIsForced
        {
            get
            {
                return this.forcedWeapons.Count > 0;
            }
        }

        public List<ThingWithComps> ForcedWeapons
        {
            get
            {
                return this.forcedWeapons;
            }
        }

        // Token: 0x060017DD RID: 6109 RVA: 0x000BA76A File Offset: 0x000B8B6A
        public void Reset()
        {
            this.forcedWeapons.Clear();
        }

        // Token: 0x060017DE RID: 6110 RVA: 0x000BA777 File Offset: 0x000B8B77
        public bool AllowedToAutomaticallyDrop(ThingWithComps weapons)
        {
            return !this.forcedWeapons.Contains(weapons);
        }

        // Token: 0x060017DF RID: 6111 RVA: 0x000BA788 File Offset: 0x000B8B88
        public void SetForced(ThingWithComps weapons, bool forced)
        {
            if (forced)
            {
                if (!this.forcedWeapons.Contains(weapons))
                {
                    this.forcedWeapons.Add(weapons);
                }
            }
            else if (this.forcedWeapons.Contains(weapons))
            {
                this.forcedWeapons.Remove(weapons);
            }
        }

        // Token: 0x060017E0 RID: 6112 RVA: 0x000BA7DB File Offset: 0x000B8BDB
        public void ExposeData()
        {
            Scribe_Collections.Look<ThingWithComps>(ref this.forcedWeapons, "forcedWeapons", LookMode.Reference, new object[0]);
        }

        // Token: 0x060017E1 RID: 6113 RVA: 0x000BA7F4 File Offset: 0x000B8BF4
        public bool IsForced(ThingWithComps weapons)
        {
            if (weapons.Destroyed)
            {
                Log.Error("Weapon was forced while Destroyed: " + weapons, false);
                if (this.forcedWeapons.Contains(weapons))
                {
                    this.forcedWeapons.Remove(weapons);
                }
                return false;
            }
            return this.forcedWeapons.Contains(weapons);
        }

        // Token: 0x04000E53 RID: 3667
        private List<ThingWithComps> forcedWeapons = new List<ThingWithComps>();
    }
}