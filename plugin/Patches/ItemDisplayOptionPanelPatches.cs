using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace BetterInventory.Patches {
	
	[HarmonyPatch(typeof(ItemDisplayOptionPanel))]
	public static class ItemDisplayOptionPanelPatches {
		[HarmonyPatch(nameof(ItemDisplayOptionPanel.GetActiveActions)), HarmonyPostfix]
		private static void ItemDisplayOptionPanel_GetActiveActions_Postfix(GameObject pointerPress, ref List<int> __result) { 
			ContextMenuOptionsPatchHelper.GetActiveActions_Postfix(pointerPress, ref __result);
		}
		
		[HarmonyPatch(nameof(ItemDisplayOptionPanel.ActionHasBeenPressed)), HarmonyPrefix]
		private static void ItemDisplayOptionPanel_ActionHasBeenPressed_Prefix(ItemDisplayOptionPanel __instance, int _actionID) {
			ContextMenuOptionsPatchHelper.ActionHasBeenPressed_Prefix(__instance, _actionID);
		}
		
		[HarmonyPatch(nameof(ItemDisplayOptionPanel.GetActionText)), HarmonyPrefix]
		private static bool ItemDisplayOptionPanel_GetActionText_Prefix(ItemDisplayOptionPanel __instance, int _actionID, ref string __result) {
			return ContextMenuOptionsPatchHelper.GetActionText_Prefix(__instance, _actionID, ref __result);
		}
	}
}