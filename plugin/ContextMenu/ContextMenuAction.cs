using UnityEngine;

namespace BetterInventory.ContextMenu {
	public interface IContextMenuAction {

		bool IsActive(GameObject pointerPress);

		void ExecuteAction(ContextMenuOptions contextMenu);
		
		string GetText(ContextMenuOptions contextMenu);

	}
}