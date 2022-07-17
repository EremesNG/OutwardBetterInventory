using System.Collections.Generic;
using System.Linq;
using BetterInventory.ContextMenu;
using UnityEngine;

namespace BetterInventory.Patches {
	public static class ContextMenuOptionsPatchHelper {
		// Extra action IDs start at 4242 to avoid any potential conflicts
		public static readonly int FIRST_ID = 4242;
		public static readonly int MAX_PLAYER_COUNT = 10;

		private static readonly Dictionary<int, IContextMenuAction> ExtraActions = new IContextMenuAction[]{
				new SalvageAction(),
				new SendToStashAction(),
				new SendToHostStashAction()
			}.Concat(Enumerable.Range(0, MAX_PLAYER_COUNT).Select(item => new SendToOtherAction(item))
			).Select((v, i) => (value: v, index: i))
			.ToDictionary(item=>item.index, item=>item.value); 
		
		internal static void GetActiveActions_Postfix(GameObject pointerPress, ref List<int> __result) { 
			__result.AddRange(from action in ExtraActions where action.Value.IsActive(pointerPress) select FIRST_ID+action.Key);
		}
		
		internal static void ActionHasBeenPressed_Prefix(ContextMenuOptions __instance, int _actionID) {
			if (ExtraActions.ContainsKey(_actionID-FIRST_ID)) {
				ExtraActions[_actionID-FIRST_ID].ExecuteAction(__instance);
			}
		}
		
		internal static bool GetActionText_Prefix(ContextMenuOptions __instance, int _actionID, ref string __result) {
			if (ExtraActions.ContainsKey(_actionID-FIRST_ID)) {
				__result = ExtraActions[_actionID-FIRST_ID].GetText(__instance);
				return false;
			}
			return true;
		}
	}
}