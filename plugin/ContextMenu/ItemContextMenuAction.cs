using UnityEngine;

namespace BetterInventory.ContextMenu {
	public abstract class ItemContextMenuAction : IContextMenuAction {

		public bool IsActive(GameObject pointerPress) {
			ItemDisplay itemDisplay = pointerPress.GetComponent<ItemDisplay>();
			if (itemDisplay == null) {
				return false;
			}
			bool isCurrency = itemDisplay is CurrencyDisplay;
			Item item = itemDisplay.RefItem;
			if (isCurrency || item != null && !(item is Skill)) {
				return IsActive(pointerPress, itemDisplay, item, isCurrency);
			}
			return false;
		}
		
		public void ExecuteAction(ContextMenuOptions contextMenu) {
			ItemDisplayOptionPanel itemDisplayOptionPanel = contextMenu as ItemDisplayOptionPanel;
			CurrencyDisplayOptionPanel currencyDisplayOptionPanel = contextMenu as CurrencyDisplayOptionPanel;
			if (itemDisplayOptionPanel == null && currencyDisplayOptionPanel == null) {
				return;
			}
			ItemDisplay itemDisplay = null;
			if (itemDisplayOptionPanel != null) {
				itemDisplay = itemDisplayOptionPanel.m_activatedItemDisplay;
			} else if (currencyDisplayOptionPanel != null) {
				itemDisplay = currencyDisplayOptionPanel.m_activatedCurrencyDisplay;
			}
			if (itemDisplay == null) {
				return;
			}
			bool isCurrency = itemDisplay is CurrencyDisplay;
			Item item = itemDisplay.RefItem;
			if (isCurrency || item != null && !(item is Skill)) {
				ExecuteAction(itemDisplayOptionPanel, itemDisplay, item, isCurrency);
			}
		}

		public abstract string GetText(ContextMenuOptions contextMenu);

		protected abstract bool IsActive(GameObject pointerPress, ItemDisplay itemDisplay, Item item, bool isCurrency);

		protected abstract void ExecuteAction(ContextMenuOptions contextMenu, ItemDisplay itemDisplay, Item item, bool isCurrency);
	}
}