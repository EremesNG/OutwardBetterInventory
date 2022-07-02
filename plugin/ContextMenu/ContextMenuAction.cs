using UnityEngine;

namespace BetterInventory.ContextMenu {
	public abstract class ContextMenuAction {
		private string text;

		public virtual string Text => text;
		
		public ContextMenuAction(string text) {
			this.text = text;
		}

		public abstract bool IsActive(GameObject pointerPress);

		public abstract void ExecuteAction(ContextMenuOptions contextMenu);

	}
}