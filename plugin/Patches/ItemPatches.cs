using BetterInventory.Extensions;
using HarmonyLib;

namespace BetterInventory.Patches {
	
	[HarmonyPatch(typeof(Item))]
	public static class ItemPatches {
		
		[HarmonyPatch(nameof(Item.DisplayName), MethodType.Getter), HarmonyPostfix]
		private static void Item_DisplayName_Postfix(Item __instance, ref string __result) {
			__instance.PatchTemporaryDisplayName(ref __result);
		}
		
	}
}