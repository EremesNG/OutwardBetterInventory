using HarmonyLib;
using UnityEngine;

namespace BetterInventory.Patches {
	
	[HarmonyPatch(typeof(ItemDisplay))]
	public static class ItemDisplayPatches {

		[HarmonyPrefix, HarmonyPatch(typeof(ItemDisplay), nameof(ItemDisplay.UpdateValueDisplay))]
		private static bool ItemDisplay_UpdateValueDisplay_Postfix(ItemDisplay __instance) {

			if (!BetterInventory.ShowItemValueEnabled.Value
				|| !__instance.CharacterUI
				|| !__instance.RefItem)
				return true;

			// Disable for waterskin, etc.
			if (__instance.RefItem is WaterContainer) {
				return true;
			}

			// Disable if cant sell
			if (!__instance.RefItem.IsSellable)
				return true;

			if (__instance.CharacterUI.GetIsMenuDisplayed(CharacterUI.MenuScreens.Shop)) {
				// Disable in shops
				return true;
			}

			if (!__instance.m_valueHolder.activeSelf) {
				__instance.m_valueHolder.SetActive(!__instance.m_valueHolder.activeSelf);
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
			return false;
		}
		
	}
}