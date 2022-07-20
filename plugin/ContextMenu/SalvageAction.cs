using UnityEngine;

namespace BetterInventory.ContextMenu {
	public class SalvageAction : ItemContextMenuAction {

		public override string GetText(ContextMenuOptions contextMenu) {
			return "Salvage";
		}
		
		protected override bool IsActive(GameObject pointerPress, ItemDisplay itemDisplay, Item item, bool isCurrency) {
			return !isCurrency && item.IsChildToPlayer && item.HasTag(TagSourceManager.GetCraftingIngredient(Recipe.CraftingType.Survival));
		}

		protected override void ExecuteAction(ContextMenuOptions contextMenu, ItemDisplay itemDisplay, Item item, bool isCurrency) {
			if (!isCurrency) {
				TryCraft(contextMenu.CharacterUI.CraftingMenu, item.ItemID);
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