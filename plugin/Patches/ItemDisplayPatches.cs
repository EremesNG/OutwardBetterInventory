using HarmonyLib;
using UnityEngine;

namespace BetterInventory.Patches {
	
	[HarmonyPatch(typeof(ItemDisplay))]
	public static class ItemDisplayPatches {
		
		[HarmonyPatch(nameof(ItemDisplay.UpdateValueDisplay)), HarmonyPostfix]
		private static void ItemDisplay_UpdateValueDisplay_Postfix(ItemDisplay __instance) {
			if (!BetterInventory.ShowItemValueEnabled.Value) {
				return;
			}
			
			if (!__instance.RefItem) {
				return;
			}

			if (__instance.CharacterUI && __instance.CharacterUI.GetIsMenuDisplayed(CharacterUI.MenuScreens.Shop)) {
				// Disable in shops
				return;
			}

			if (!__instance.m_valueHolder.activeSelf) {
				__instance.m_valueHolder.SetActive(true);
			}

			string itemPrice = "0";


			switch (BetterInventory.ItemValueType.Value)
            {
				case BetterInventory.ItemValueTypeSetting.BaseValue:
					itemPrice = __instance.RefItem.RawBaseValue.ToString();
					break;
				case BetterInventory.ItemValueTypeSetting.SellValue:
					float sellModif = (1f + __instance.CharacterUI.TargetCharacter.GetItemSellPriceModifier((Merchant)null, __instance.RefItem)) * 0.3f;
					float rawSellPrice = sellModif * (float)__instance.RefItem.RawCurrentValue;
					itemPrice = Mathf.RoundToInt(rawSellPrice).ToString();
					break;
			}

			__instance.m_lblValue.text = itemPrice;
		}
		
	}
}