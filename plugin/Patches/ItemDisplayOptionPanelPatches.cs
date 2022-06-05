using System.Collections.Generic;
using System.Linq;
using BetterInventory.ContextMenu;
using HarmonyLib;
using UnityEngine;

namespace BetterInventory.Patches {
	
	[HarmonyPatch]
	public static class ItemDisplayOptionPanelPatches {
		// Extra action IDs start at 4242 to avoid any potential conflicts
		public static readonly Dictionary<int, ContextMenuAction> ExtraActions = new Dictionary<int, ContextMenuAction> {
			{4242, new SalvageAction(4242)},
			{4243, new SendToStashAction(4243)}, 
			{4244, new SendToHostStashAction(4244)},
			{4245, new SendToOtherAction(4245)}
		};
		
		[HarmonyPatch(typeof(ItemDisplayOptionPanel), nameof(ItemDisplayOptionPanel.GetActiveActions)), HarmonyPostfix]
		private static void EquipmentMenu_GetActiveActions_Postfix(GameObject pointerPress, ref List<int> __result) {
			__result.AddRange(from action in ExtraActions.Values where action.IsActive(pointerPress) select action.ID);
		}
		
		[HarmonyPatch(typeof(ItemDisplayOptionPanel), nameof(ItemDisplayOptionPanel.ActionHasBeenPressed)), HarmonyPrefix]
		private static void EquipmentMenu_ActionHasBeenPressed_Prefix(ItemDisplayOptionPanel __instance, int _actionID) {
			if (ExtraActions.ContainsKey(_actionID)) {
				ExtraActions[_actionID].ExecuteAction(__instance);
			}
		}
		
		[HarmonyPatch(typeof(ItemDisplayOptionPanel), nameof(ItemDisplayOptionPanel.GetActionText)), HarmonyPrefix]
		private static bool EquipmentMenu_GetActionText_Prefix(ItemDisplayOptionPanel __instance, int _actionID, ref string __result) {
			if (ExtraActions.ContainsKey(_actionID)) {
				__result = ExtraActions[_actionID].Text;
				return false;
			}
			return true;
		}
	}
}