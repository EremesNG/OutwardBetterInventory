using UnityEngine;

namespace BetterInventory.ContextMenu {
	public abstract class ContextMenuAction {
		private int id;
		private string text;

		public string Text => text;
		public int ID => id;

		public ContextMenuAction(int id, string text) {
			this.id = id;
			this.text = text;
		}

		public abstract bool IsActive(GameObject pointerPress);

		public abstract void ExecuteAction(ContextMenuOptions contextMenu);

	}
}