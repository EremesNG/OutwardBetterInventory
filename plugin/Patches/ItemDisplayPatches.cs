using HarmonyLib;

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
			
			__instance.m_lblValue.text = __instance.RefItem.RawBaseValue.ToString();
		}
		
	}
}