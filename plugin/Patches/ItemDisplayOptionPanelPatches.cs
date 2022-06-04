using System.Collections.Generic;
using System.Linq;
using BetterInventory.ContextMenu;
using HarmonyLib;
using UnityEngine;

namespace BetterInventory.Patches {

	public static class ItemDisplayOptionPanelConsts {
		// Extra action IDs start at 4242 to avoid any potential conflicts
		public static readonly Dictionary<int, ContextMenuAction> ExtraActions = new Dictionary<int, ContextMenuAction> {
			{4242, new SalvageAction(4242)},
			{4243, new SendToStashAction(4243)}, 
			{4244, new SendToHostStashAction(4244)},
			{4245, new SendToOtherAction(4245)}
		};
	}
	
	[HarmonyPatch(typeof(ItemDisplayOptionPanel), nameof(ItemDisplayOptionPanel.GetActiveActions))]
	public class EquipmentMenu_GetActiveActions {
		[HarmonyPostfix]
		static void Postfix(GameObject pointerPress, ref List<int> __result) {
			__result.AddRange(from action in ItemDisplayOptionPanelConsts.ExtraActions.Values where action.IsActive(pointerPress) select action.ID);
		}
	}
	
	[HarmonyPatch(typeof(ItemDisplayOptionPanel), nameof(ItemDisplayOptionPanel.ActionHasBeenPressed))]
	public class EquipmentMenu_ActionHasBeenPressed {
		[HarmonyPostfix]
		static void Prefix(ItemDisplayOptionPanel __instance, int _actionID) {
			if (ItemDisplayOptionPanelConsts.ExtraActions.ContainsKey(_actionID)) {
				ItemDisplayOptionPanelConsts.ExtraActions[_actionID].ExecuteAction(__instance);
			}
		}
	}
	
	[HarmonyPatch(typeof(ItemDisplayOptionPanel), nameof(ItemDisplayOptionPanel.GetActionText))]
	public class EquipmentMenu_GetActionText {
		[HarmonyPostfix]
		static bool Prefix(ItemDisplayOptionPanel __instance, int _actionID, ref string __result) {
			if (ItemDisplayOptionPanelConsts.ExtraActions.ContainsKey(_actionID)) {
				__result = ItemDisplayOptionPanelConsts.ExtraActions[_actionID].Text;
				return false;
			}
			return true;
		}
	}
}