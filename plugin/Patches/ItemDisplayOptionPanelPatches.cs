using System.Collections.Generic;
using System.Linq;
using BetterInventory.ContextMenu;
using HarmonyLib;
using UnityEngine;

namespace BetterInventory.Patches {
	
	[HarmonyPatch(typeof(ItemDisplayOptionPanel))]
	public static class ItemDisplayOptionPanelPatches {
		// Extra action IDs start at 4242 to avoid any potential conflicts
		public static readonly int FIRST_ID = 4242;
		public static readonly int MAX_PLAYER_COUNT = 10;

		public static readonly Dictionary<int, ContextMenuAction> ExtraActions = new ContextMenuAction[]{
				new SalvageAction(),
				new SendToStashAction(),
				new SendToHostStashAction()
			}.Concat(Enumerable.Range(0, MAX_PLAYER_COUNT).Select(item => new SendToOtherAction(item))
			).Select((v, i) => (value: v, index: i))
			.ToDictionary(item=>item.index, item=>item.value); 

		[HarmonyPatch(nameof(ItemDisplayOptionPanel.GetActiveActions)), HarmonyPostfix]
		private static void EquipmentMenu_GetActiveActions_Postfix(GameObject pointerPress, ref List<int> __result) { 
			__result.AddRange(from action in ExtraActions where action.Value.IsActive(pointerPress) select FIRST_ID+action.Key);
		}
		
		[HarmonyPatch(nameof(ItemDisplayOptionPanel.ActionHasBeenPressed)), HarmonyPrefix]
		private static void EquipmentMenu_ActionHasBeenPressed_Prefix(ItemDisplayOptionPanel __instance, int _actionID) {
			if (ExtraActions.ContainsKey(_actionID-FIRST_ID)) {
				ExtraActions[_actionID-FIRST_ID].ExecuteAction(__instance);
			}
		}
		
		[HarmonyPatch(nameof(ItemDisplayOptionPanel.GetActionText)), HarmonyPrefix]
		private static bool EquipmentMenu_GetActionText_Prefix(ItemDisplayOptionPanel __instance, int _actionID, ref string __result) {
			if (ExtraActions.ContainsKey(_actionID-FIRST_ID)) {
				__result = ExtraActions[_actionID-FIRST_ID].Text;
				return false;
			}
			return true;
		}
	}
}