using UnityEngine;

namespace BetterInventory.ContextMenu {
	public class SalvageAction : ItemContextMenuAction {
		public SalvageAction() : base("Salvage") {
		}

		protected override bool IsActive(GameObject pointerPress, ItemDisplay itemDisplay, Item item) {
			return item.HasTag(TagSourceManager.GetCraftingIngredient(Recipe.CraftingType.Survival));
		}

		protected override void ExecuteAction(ItemDisplayOptionPanel contextMenu, ItemDisplay itemDisplay, Item item) {
			TryCraft(contextMenu.CharacterUI.CraftingMenu, item.ItemID);
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