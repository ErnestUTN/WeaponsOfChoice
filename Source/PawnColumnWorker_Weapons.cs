using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;


namespace WeaponsOfChoice
{
    public class PawnColumnWorker_Weapons : PawnColumnWorker
    {
        public override void DoHeader(Rect rect, PawnTable table)
        {
            base.DoHeader(rect, table);
            Rect rect2 = new Rect(rect.x, rect.y + (rect.height - 65f), Mathf.Min(rect.width, 360f), 32f);
            if (Widgets.ButtonText(rect2, "WOC.ManageWeapons".Translate(), true, false, true))
            {
                Find.WindowStack.Add(new Dialog_ManageWeapons(null));
                
            }
            
        }

        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
             Pawn_WeaponPresetTracker pawn_WeaponPresetTracker = pawn.TryGetComp<Pawn_WeaponPresetTracker>();
            if (pawn_WeaponPresetTracker == null)
            {
                return;
            }
            int num = Mathf.FloorToInt((rect.width - 4f) * 0.714285731f);
            int num2 = Mathf.FloorToInt((rect.width - 4f) * 0.2857143f);
            float num3 = rect.x;

            bool somethingIsForced = pawn_WeaponPresetTracker.forcedHandler.SomethingIsForced;

            Rect rect2 = new Rect(num3, rect.y + 2f, (float)num, rect.height - 4f);

            if (somethingIsForced)
                rect2.width -= 4f + (float)num2;

            Rect rect3 = rect2;
            Pawn pawn2 = pawn;
            Func<Pawn, WeaponPreset> getPayload = (Pawn p) => p.TryGetComp<Pawn_WeaponPresetTracker>().CurrentWeaponPreset;
            Func<Pawn, IEnumerable<Widgets.DropdownMenuElement<WeaponPreset>>> menuGenerator = new Func<Pawn, IEnumerable<Widgets.DropdownMenuElement<WeaponPreset>>>(this.Button_GenerateMenu);
            string buttonLabel = pawn_WeaponPresetTracker.CurrentWeaponPreset.label.Truncate(rect2.width, null);
            string label = pawn_WeaponPresetTracker.CurrentWeaponPreset.label;
            Widgets.Dropdown<Pawn, WeaponPreset>(rect3, pawn2, getPayload, menuGenerator, buttonLabel, null, label, null, null, true);
            num3 += rect2.width;
            num3 += 4f;
            Rect rect4 = new Rect(num3, rect.y + 2f, (float)num2, rect.height - 4f);

            Rect rect5 = new Rect(num3, rect.y + 2f, (float)num2, rect.height - 4f);
            if (Widgets.ButtonText(rect5, "AssignTabEdit".Translate(), true, false, true)) //Edit...
            {
                Find.WindowStack.Add(new Dialog_ManageWeapons(pawn_WeaponPresetTracker.CurrentWeaponPreset));
            }
            num3 += (float)num2;
        }

        private IEnumerable<Widgets.DropdownMenuElement<WeaponPreset>> Button_GenerateMenu(Pawn pawn)
        {
            Pawn_WeaponPresetTracker pawn_WeaponPresetTracker = pawn.TryGetComp<Pawn_WeaponPresetTracker>();
            using (List<WeaponPreset>.Enumerator enumerator = Current.Game.GetComponent<WeaponPresetDatabase>().AllWeaponPresets.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    WeaponPreset weaponPreset = enumerator.Current;
                    yield return new Widgets.DropdownMenuElement<WeaponPreset>
                    {
                        option = new FloatMenuOption(weaponPreset.label, delegate ()
                        {
                            pawn_WeaponPresetTracker.CurrentWeaponPreset = weaponPreset;
                        }, MenuOptionPriority.Default, null, null, 0f, null, null),
                        payload = weaponPreset
                    };
                }
            }
            yield break;
        }


        public override int GetMinWidth(PawnTable table)
        {
            return Mathf.Max(base.GetMinWidth(table), Mathf.CeilToInt(194f));
        }


        public override int GetOptimalWidth(PawnTable table)
        {
            return Mathf.Clamp(Mathf.CeilToInt(251f), this.GetMinWidth(table), this.GetMaxWidth(table));
        }


        public override int GetMinHeaderHeight(PawnTable table)
        {
            return Mathf.Max(base.GetMinHeaderHeight(table), 65);
        }


        public override int Compare(Pawn a, Pawn b)
        {
            return this.GetValueToCompare(a).CompareTo(this.GetValueToCompare(b));
        }


        private int GetValueToCompare(Pawn pawn)
        {
            Pawn_WeaponPresetTracker pawn_WeaponPresetTracker = pawn.TryGetComp<Pawn_WeaponPresetTracker>();
            return (pawn_WeaponPresetTracker != null && pawn_WeaponPresetTracker.CurrentWeaponPreset!= null) ? pawn_WeaponPresetTracker.CurrentWeaponPreset.uniqueId : int.MinValue;
        }



        public const int TopAreaHeight = 65;


        public const int ManageOutfitsButtonHeight = 32;
        
    }
}