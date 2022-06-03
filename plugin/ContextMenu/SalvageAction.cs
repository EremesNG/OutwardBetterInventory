using UnityEngine;

namespace BetterInventory.ContextMenu {
	public class SalvageAction : ContextMenuAction {
		public SalvageAction(int id) : base(id, "Salvage") {
		}

		public override bool IsActive(GameObject pointerPress) {
			ItemDisplay itemDisplay = pointerPress.GetComponent<ItemDisplay>();
			if (itemDisplay != null) {
				Item item = itemDisplay.RefItem;
				if (item == null) {
					return false;
				}
				if (itemDisplay.RefItem is Skill) {
					return false;
				}
				return item.HasTag(TagSourceManager.GetCraftingIngredient(Recipe.CraftingType.Survival));
			}
			return false;
		}

		public override void ExecuteAction(ContextMenuOptions contextMenu) {
			ItemDisplayOptionPanel itemDisplayOptionPanel = contextMenu as ItemDisplayOptionPanel;
			if (itemDisplayOptionPanel != null) {
				ItemDisplay activatedItemDisplay = itemDisplayOptionPanel.m_activatedItemDisplay;
				if (activatedItemDisplay!= null && !activatedItemDisplay.IsEmpty) {
					TryCraft(activatedItemDisplay.CharacterUI.CraftingMenu, activatedItemDisplay.LastRefItemID);
				}
			}
		}

		private void TryCraft(CraftingMenu craftingMenu, int itemID) {
			BetterInventory.Log.LogDebug("Trying to salvage item: "+itemID);
			craftingMenu.OnRecipeSelected(-1, true);
			craftingMenu.RefreshAutoRecipe();
			craftingMenu.IngredientSelectorHasChanged(0, itemID);
			if (craftingMenu.m_lastFreeRecipeIndex == -1) {
				craftingMenu.m_characterUI.ShowInfoNotification("This item cannot be salvaged.");
				return;
			}
			float origCraftingTime = craftingMenu.CraftingTime;
			craftingMenu.CraftingTime = 0.0f;
			craftingMenu.OnCookButtonClicked();
			craftingMenu.CraftingTime = origCraftingTime;
		}
	}
}