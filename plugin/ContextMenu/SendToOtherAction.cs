using UnityEngine;

namespace BetterInventory.ContextMenu {
	public class SendToOtherAction : ItemContextMenuAction {
		
		public SendToOtherAction(int id) : base(id, "Send to Other Player") {
		}
		
		protected override bool IsActive(GameObject pointerPress, ItemDisplay itemDisplay, Item item) {
			return BetterInventory.SendToOtherEnabled.Value && HasOtherPlayerCharacter();
		}

		protected override void ExecuteAction(ItemDisplayOptionPanel contextMenu, ItemDisplay itemDisplay, Item item) {
			TrySendToOther(itemDisplay);
		}

		private bool HasOtherPlayerCharacter() {
			return Global.Lobby.PlayersInLobbyCount > 1;
		}

		private bool TryGetNearestOtherPlayer(Character character, out Character otherCharacter) {
			float maxDistanceSq = BetterInventory.SendToOtherMaxDistance.Value * BetterInventory.SendToOtherMaxDistance.Value;
			foreach (PlayerSystem player in Global.Lobby.PlayersInLobby) {
				Character controlledCharacter = player.ControlledCharacter;
				if (controlledCharacter != character && Vector3.SqrMagnitude(controlledCharacter.transform.position - character.transform.position) <= maxDistanceSq) {
					otherCharacter = controlledCharacter;
					return true;
				}
			}
			otherCharacter = null;
			return false;
		}
		
		private void TrySendToOther(ItemDisplay itemDisplay) {
			if (!HasOtherPlayerCharacter()) {
				return;
			}
			Character character = itemDisplay.m_characterUI.TargetCharacter;
			if (!TryGetNearestOtherPlayer(character, out Character otherCharacter)) {
				character.CharacterUI.ShowInfoNotification("The other player is too far.");
				return;
			}
			itemDisplay.TryMoveTo(otherCharacter.Inventory.Pouch);
		}
	}
}