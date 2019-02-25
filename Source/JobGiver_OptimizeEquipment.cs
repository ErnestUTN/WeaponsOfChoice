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

            bool HasWeaponEquipped = true; //true = Has got  a weapon equipped
            bool HasNonePreset = false; //false = Not using "NONE" PRESET
            bool WeaponNotAllowed = false; //false = The weapon is not allowed by the preset

            if (thingWithComps is null)
                HasWeaponEquipped = false;
            if (CurweaponPreset.filter.AllowedDefCount == 0)
                HasNonePreset = true;
            if (thingWithComps != null && !_equipmentFilter.Allows(thingWithComps.def))
                WeaponNotAllowed = true;

            //Log.Message(pawn + " HasWeaponEquipped = " + HasWeaponEquipped.ToString(), true);
           // Log.Message(pawn + " HasNonePreset = " + HasNonePreset.ToString(), true);
            //Log.Message(pawn + " WeaponNotAllowed = " + WeaponNotAllowed.ToString(), true);


            //Log.Message(pawn + " Has this equipped " + thingWithComps, true);



            if ((!HasWeaponEquipped && !HasNonePreset) || (!HasNonePreset && WeaponNotAllowed))

            {

               // Log.Warning(pawn + " Will Optimize weapons", true);
                Thing newWeapon = null;
                List<Thing> list = pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.Weapon);

                if (list.Count == 0)
                {
                    this.SetNextOptimizeTick(pawn);
                    return null;
                }

                // Top 3 Guns chosen by the pawn first
                bool found = false;
                foreach (ThingDef PListThingDef in CurweaponPreset.SearchPriorityThingDefs) //it's 3 right now
                {
                    if (!(PListThingDef is null))
                    {
                        foreach (Thing searchedWeapon in list)
                        {
                            if (PListThingDef == searchedWeapon.def && searchedWeapon.IsInAnyStorage() && !searchedWeapon.IsForbidden(pawn) && !searchedWeapon.IsBurning() && pawn.CanReserveAndReach(searchedWeapon, PathEndMode.OnCell, pawn.NormalMaxDanger(), 1, -1, null, false))
                            {
                                newWeapon = searchedWeapon;
                                found = true;
                                break;
                            }

                        }
                    }
                    if (found) break;
                }
               // Log.Warning(pawn + " found weapon = " + found.ToString(), true);
                //else just keep browsing the other filter stuff
                if (!found)
                {
                    // Log.Message(pawn + " Looking in the rest of the non priorityized weapons ", true);

                    foreach (Thing searchedWeapon in list)
                    {
                        

                        //Log.Message(searchedWeapon + "is  allowed by filter? = " + _equipmentFilter.Allows(searchedWeapon));
                        if (_equipmentFilter.Allows(searchedWeapon) && searchedWeapon.IsInAnyStorage() && !searchedWeapon.IsForbidden(pawn) && !searchedWeapon.IsBurning() && pawn.CanReserveAndReach(searchedWeapon, PathEndMode.OnCell, pawn.NormalMaxDanger(), 1, -1, null, false))
                        {
                            newWeapon = searchedWeapon;
                            found = true;
                            break;
                        }
                    }
                }

                if (newWeapon == null)
                {
                    this.SetNextOptimizeTick(pawn);
                    return null;
                }


                return new Job(JobDefOf.Equip, newWeapon);
            }
        

            else 
            {
                //Log.Warning("Not doing anything, no need to optimize", true);
                this.SetNextOptimizeTick(pawn);
                return null;
            }
        
          
            



        }

    }
}
