using UnityEngine;

namespace BetterInventory.ContextMenu {
	public abstract class ItemContextMenuAction : ContextMenuAction {
		protected ItemContextMenuAction(int id, string text) : base(id, text) {
		}
		
		public override bool IsActive(GameObject pointerPress) {
			ItemDisplay itemDisplay = pointerPress.GetComponent<ItemDisplay>();
			if (itemDisplay == null) {
				return false;
			}
			Item item = itemDisplay.RefItem;
			if (item != null && !(item is Skill)) {
				return IsActive(pointerPress, itemDisplay, item);
			}
			return false;
		}
		
		public override void ExecuteAction(ContextMenuOptions contextMenu) {
			ItemDisplayOptionPanel itemDisplayOptionPanel = contextMenu as ItemDisplayOptionPanel;
			if (itemDisplayOptionPanel == null) {
				return;
			}
			ItemDisplay itemDisplay = itemDisplayOptionPanel.m_activatedItemDisplay;
			if (itemDisplay == null) {
				return;
			}
			Item item = itemDisplay.RefItem;
			if (item != null && !(item is Skill)) {
				ExecuteAction(itemDisplayOptionPanel, itemDisplay, item);
			}
		}
		
		protected abstract bool IsActive(GameObject pointerPress, ItemDisplay itemDisplay, Item item);

		protected abstract void ExecuteAction(ItemDisplayOptionPanel contextMenu, ItemDisplay itemDisplay, Item item);
	}
}