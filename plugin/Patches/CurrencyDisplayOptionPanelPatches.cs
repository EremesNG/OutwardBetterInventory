using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace BetterInventory.Patches {
	
	[HarmonyPatch(typeof(CurrencyDisplayOptionPanel))]
	public static class CurrencyDisplayOptionPanelPatches {
		[HarmonyPatch(nameof(CurrencyDisplayOptionPanel.GetActiveActions)), HarmonyPostfix]
		private static void ItemDisplayOptionPanel_GetActiveActions_Postfix(GameObject pointerPress, ref List<int> __result) { 
			ContextMenuOptionsPatchHelper.GetActiveActions_Postfix(pointerPress, ref __result);
		}
		
		[HarmonyPatch(nameof(CurrencyDisplayOptionPanel.ActionHasBeenPressed)), HarmonyPrefix]
		private static void ItemDisplayOptionPanel_ActionHasBeenPressed_Prefix(CurrencyDisplayOptionPanel __instance, int _actionID) {
			ContextMenuOptionsPatchHelper.ActionHasBeenPressed_Prefix(__instance, _actionID);
		}
		
		[HarmonyPatch(nameof(CurrencyDisplayOptionPanel.GetActionText)), HarmonyPrefix]
		private static bool ItemDisplayOptionPanel_GetActionText_Prefix(CurrencyDisplayOptionPanel __instance, int _actionID, ref string __result) {
			return ContextMenuOptionsPatchHelper.GetActionText_Prefix(__instance, _actionID, ref __result);
		}
	}
}