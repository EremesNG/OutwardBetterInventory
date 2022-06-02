using System;
using System.Collections.Generic;
using System.Reflection;
using BetterInventory;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace BetterInventory.Patches {

	public class QuickSlotDisplayHelper {
		private static FieldInfo quickSlotDisplay_refQuickSlot = AccessTools.Field(typeof(QuickSlotDisplay), "m_refQuickSlot");
		private static FieldInfo quickSlotDisplay_quickSlotIcon = AccessTools.Field(typeof(QuickSlotDisplay), "m_quickSlotIcon");
		
		private QuickSlotDisplay quickSlotDisplay;

		public QuickSlotDisplayHelper(QuickSlotDisplay quickSlotDisplay) {
			this.quickSlotDisplay = quickSlotDisplay;
		}

		public QuickSlotDisplay QuickSlotDisplay => quickSlotDisplay;

		public QuickSlot GetQuickSlot() {
			return (QuickSlot) quickSlotDisplay_refQuickSlot.GetValue(quickSlotDisplay);
		}
		public Image GetQuickSlotIcon() {
			return (Image) quickSlotDisplay_quickSlotIcon.GetValue(quickSlotDisplay);
		}
	}
	
	[HarmonyPatch(typeof(QuickSlotDisplay), "AwakeInit")]
	public class QuickSlot_AwakeInit {
		[HarmonyPostfix]
		static void Postfix(QuickSlotDisplay __instance) {
			GameObject gameObject = __instance.gameObject;
			/*Image image = gameObject.AddComponent<Image>();
			Texture2D tex = Resources.Load<Texture2D>("tex_men_highlightItem");
			image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));*/
			QuickSlotDisplayDrop quickSlotDisplayDrop = gameObject.AddComponent<QuickSlotDisplayDrop>();
			GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
			quickSlotDisplayDrop.enabled = true;
			graphicRaycaster.enabled = true;
		}
	}
}