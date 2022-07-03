using UnityEngine;

namespace BetterInventory.ContextMenu {
	public class SendToHostStashAction : SendToStashAction {

		public override string GetText(ItemDisplayOptionPanel contextMenu) {
			return "Send to Host's Stash";
		}
		
		protected override bool IsActive(GameObject pointerPress, ItemDisplay itemDisplay, Item item) {
			return !IsHost() && BetterInventory.SendToHostStashEnabled.Value && IsInStashArea();
		}

		protected override void ExecuteAction(ItemDisplayOptionPanel contextMenu, ItemDisplay itemDisplay, Item item) {
			TrySendToStash(itemDisplay, CharacterManager.Instance.GetWorldHostCharacter());
		}

		private bool IsHost() {
			return Global.Lobby.IsWorldOwner;
		}
	}
}