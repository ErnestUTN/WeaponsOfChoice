using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace WeaponsOfChoice
{
	public class Dialog_ManageWeapons : Window
	{
		public Dialog_ManageWeapons(WeaponPreset selectedWeaponPreset)
		{
			this.forcePause = true;
			this.doCloseX = true;
			this.doCloseButton = true;
			this.closeOnClickedOutside = true;
			this.absorbInputAroundWindow = true;
			if (Dialog_ManageWeapons.WeaponPresetGlobalFilter == null)
			{
				Dialog_ManageWeapons.WeaponPresetGlobalFilter = new ThingFilter();
				Dialog_ManageWeapons.WeaponPresetGlobalFilter.SetAllow(ThingCategoryDefOf.Weapons, true, null, null);
			}
			this.SelectedWeaponPreset = selectedWeaponPreset;
		}


		private WeaponPreset SelectedWeaponPreset
		{
			get
			{
				return this.selWeaponPresetInt;
			}
			set
			{
				this.CheckSelectedWeaponPresetHasName();
				this.selWeaponPresetInt = value;
			}
		}


		public override Vector2 InitialSize
		{
			get
			{
				return new Vector2(700f, 700f);
			}
		}

		private void CheckSelectedWeaponPresetHasName()
		{
			if (this.SelectedWeaponPreset != null && this.SelectedWeaponPreset.label.NullOrEmpty())
			{
				this.SelectedWeaponPreset.label = "Unnamed";
			}
		}

		public override void DoWindowContents(Rect inRect)
		{
            
            float num = 0f;
			Rect rect = new Rect(0f, 0f, 150f, 35f);
			num += 150f;
			if (Widgets.ButtonText(rect, "WOC_SelectWeaponPreset".Translate(), true, false, true))
			{
				List<FloatMenuOption> list = new List<FloatMenuOption>();
				foreach (WeaponPreset localOut3 in Current.Game.GetComponent<WeaponPresetDatabase>().AllWeaponPresets)
				{
					WeaponPreset localOut = localOut3;
					list.Add(new FloatMenuOption(localOut.label, delegate()
					{
						this.SelectedWeaponPreset = localOut;
					}, MenuOptionPriority.Default, null, null, 0f, null, null));
				}
				Find.WindowStack.Add(new FloatMenu(list));
			}
			num += 10f;
			Rect rect2 = new Rect(num, 0f, 150f, 35f);
			num += 150f;
			if (Widgets.ButtonText(rect2, "WOC_NewWeaponPreset".Translate(), true, false, true))
			{
				this.SelectedWeaponPreset = Current.Game.GetComponent<WeaponPresetDatabase>().MakeNewWeaponPreset();
			}
			num += 10f;
			Rect rect3 = new Rect(num, 0f, 150f, 35f);
			num += 150f;
			if (Widgets.ButtonText(rect3, "WOC_DeleteWeaponPreset".Translate(), true, false, true))
			{
				List<FloatMenuOption> list2 = new List<FloatMenuOption>();
				foreach (WeaponPreset localOut2 in Current.Game.GetComponent<WeaponPresetDatabase>().AllWeaponPresets)
				{
					WeaponPreset localOut = localOut2;
					list2.Add(new FloatMenuOption(localOut.label, delegate()
					{
						AcceptanceReport acceptanceReport = Current.Game.GetComponent<WeaponPresetDatabase>().TryDelete(localOut);
						if (!acceptanceReport.Accepted)
						{
							Messages.Message(acceptanceReport.Reason, MessageTypeDefOf.RejectInput, false);
						}
						else if (localOut == this.SelectedWeaponPreset)
						{
							this.SelectedWeaponPreset = null;
						}
					}, MenuOptionPriority.Default, null, null, 0f, null, null));
				}
				Find.WindowStack.Add(new FloatMenu(list2));
			}
			Rect rect4 = new Rect(0f, 40f, inRect.width, inRect.height - 40f - this.CloseButSize.y).ContractedBy(10f);
			if (this.SelectedWeaponPreset == null)
			{
				GUI.color = Color.grey;
				Text.Anchor = TextAnchor.MiddleCenter;
				Widgets.Label(rect4, "WOC_NoWeaponPresetSelected".Translate());
				Text.Anchor = TextAnchor.UpperLeft;
				GUI.color = Color.white;
				return;
			}
			GUI.BeginGroup(rect4);
			Rect rect5 = new Rect(0f, 0f, 200f, 30f);
			Dialog_ManageWeapons.DoNameInputRect(rect5, ref this.SelectedWeaponPreset.label);
			Rect rect6 = new Rect(0f, 40f, 300f, rect4.height - 45f - 10f);
			Rect rect7 = rect6;
			ref Vector2 ptr = ref this.scrollPosition;
			ThingFilter filter = this.SelectedWeaponPreset.filter;
			ThingFilter parentFilter = Dialog_ManageWeapons.WeaponPresetGlobalFilter;
			int openMask = 16;

			ThingFilterUI.DoThingFilterConfigWindow(rect7, ref ptr, filter, parentFilter, openMask, null, null, false, null, null);
            //Creacion de los 3 priority buttons. Verificar que al menos ya tiene alguno seleccionado
            if (this.selWeaponPresetInt.filter.AllowedDefCount > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    float yoffset = 50f + i * (35f + 10f);
                    Rect Priorityrect = new Rect(325f, yoffset, 150f, 35f);
                    if (Widgets.ButtonText(Priorityrect, "WOC_WeaponPriority".Translate() + " " + (i+1), true, false, true))
                    {
                        Find.WindowStack.Add(GeneratePriorityFloatMenuFromFilteredWeapons(i, this.SelectedWeaponPreset));
                    }
                    Widgets.TextField(new Rect(325f+150f+10f, yoffset, 120f, 35f), this.SelectedWeaponPreset.PriorityWeapons[i]?.label.ToString() ?? "None");
                }
            }
            else
            {
                Widgets.TextArea(new Rect(325f, 50f, 180f, 60f), "No filters selected",true);
            }
            
            GUI.EndGroup();
		}

        private FloatMenu GeneratePriorityFloatMenuFromFilteredWeapons(int PriorityLevel, WeaponPreset weaponPreset)
        {

            List<FloatMenuOption> list = new List<FloatMenuOption>();
            
            
                foreach (ThingDef weapons in weaponPreset.filter.AllowedThingDefs)
                {
                    ThingDef weapon = weapons;
                    list.Add(new FloatMenuOption(weapon.label, delegate ()
                    {
                        this.SelectedWeaponPreset.PriorityWeapons[PriorityLevel] = weapon;
                        
                        
                    }, MenuOptionPriority.Default, null, null, 0f, null, null));

                }
            
           
                
            return new FloatMenu(list);


         
            
        }



        public override void PreClose()
		{
			base.PreClose();
			this.CheckSelectedWeaponPresetHasName();
		}

		public static void DoNameInputRect(Rect rect, ref string name)
		{
			name = Widgets.TextField(rect, name, 30, WeaponPreset.ValidNameRegex);
		}

		private Vector2 scrollPosition;

		private WeaponPreset selWeaponPresetInt;

		public const float TopAreaHeight = 40f;

		public const float TopButtonHeight = 35f;

		public const float TopButtonWidth = 170f;

		private static ThingFilter WeaponPresetGlobalFilter;
	}
}
