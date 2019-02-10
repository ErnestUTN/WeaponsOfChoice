using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;


namespace WeaponsOfChoice
{
    class JobGiver_OptimizeEquipment : ThinkNode_JobGiver
    {
        private void SetNextOptimizeTick(Pawn pawn)
        {
            pawn.TryGetComp<Pawn_WeaponPresetTracker>().nextWeaponPresetOptimizeTick = Find.TickManager.TicksGame + Rand.Range(6000, 9000);

        }

        protected override Job TryGiveJob(Pawn pawn)
        {
            Pawn_WeaponPresetTracker pawn_WeaponPresetTracker = pawn.TryGetComp<Pawn_WeaponPresetTracker>();
            if (pawn_WeaponPresetTracker == null)
            {

                return null;
            }
            if (pawn.Faction != Faction.OfPlayer)
            {

                return null;
            }
            if (!DebugViewSettings.debugApparelOptimize)
            {
                if (Find.TickManager.TicksGame < pawn_WeaponPresetTracker.nextWeaponPresetOptimizeTick)
                {
                    return null;
                }
            }

            WeaponPreset CurweaponPreset = pawn_WeaponPresetTracker.CurrentWeaponPreset;
            ThingFilter _equipmentFilter = CurweaponPreset.filter;
            ThingWithComps thingWithComps = pawn.equipment?.Primary;

            if (!((thingWithComps != null && !_equipmentFilter.Allows(thingWithComps)) || thingWithComps == null))
                return null;

            Thing newWeapon = null;
            List<Thing> list = pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.Weapon);
            
            if (list.Count == 0)
            {
                this.SetNextOptimizeTick(pawn);
                return null;
            }

            // Top 3 Guns chosen by the pawn first
            bool found = false;
            for (int j = 0; j < CurweaponPreset.PriorityWeapons.Length; j++) //it's 3 right now
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Thing searchedWeapon = list[i];

                    if (CurweaponPreset.PriorityWeapons[j] == searchedWeapon.def && searchedWeapon.IsInAnyStorage() && !searchedWeapon.IsForbidden(pawn) && !searchedWeapon.IsBurning() && pawn.CanReserveAndReach(searchedWeapon, PathEndMode.OnCell, pawn.NormalMaxDanger(), 1, -1, null, false))
                    {
                        newWeapon = searchedWeapon;
                        found = true;
                        break;
                    }

                }
            }

            //else just keep browsing the other filter stuff
            if (!found)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    Thing searchedWeapon = list[j];

                    if (_equipmentFilter.Allows(searchedWeapon) && searchedWeapon.IsInAnyStorage() && !searchedWeapon.IsForbidden(pawn) && !searchedWeapon.IsBurning() && pawn.CanReserveAndReach(searchedWeapon, PathEndMode.OnCell, pawn.NormalMaxDanger(), 1, -1, null, false))
                    {
                        newWeapon = searchedWeapon;
                        break;
                    }
                }

                if (newWeapon == null)
                {
                    this.SetNextOptimizeTick(pawn);
                    return null;
                }
            }

            return new Job(JobDefOf.Equip, newWeapon);




        }

    }
}
