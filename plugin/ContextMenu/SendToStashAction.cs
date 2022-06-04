using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterInventory.ContextMenu {
	public class SendToStashAction : ItemContextMenuAction {
		private static readonly Dictionary<string, string> StashSceneQuestEvents = new Dictionary<string, string>{
			{"CierzoNewTerrain", null},
			{"Berg", "g403vlCU6EG0s1mI6t_rFA"},
			{"Monsoon", "shhCMFa-lUqbIYS9hRcsdg"},
			{"Levant", "LpVUuoxfhkaWOgh6XLbarA"},
			{"Harmattan", "0r087PIxTUqoj6N7z2HFNw"},
			{"NewSirocco", null}
		};
		
		public SendToStashAction(int id, string text = "Send to Stash") : base(id, text) {
		}

		protected override bool IsActive(GameObject pointerPress, ItemDisplay itemDisplay, Item item) {
			return BetterInventory.SendToStashEnabled.Value && IsInStashArea();
		}

		protected override void ExecuteAction(ItemDisplayOptionPanel contextMenu, ItemDisplay itemDisplay, Item item) {
			TrySendToStash(itemDisplay, itemDisplay.m_characterUI.TargetCharacter);
		}

		/*private bool IsNearOwnedStash(Character character) { // TODO This could be useful for mod compatibility
			foreach (TreasureChest treasureChest in Object.FindObjectsOfType<TreasureChest>()) {
				if (treasureChest.SpecialType == ItemContainer.SpecialContainerTypes.Stash) {
					return true;
				}
			}
			return false;
		}*/

		private bool IsInStashArea() {
			Scene scene = SceneManager.GetActiveScene();
			string sceneName = scene.name;
			if (StashSceneQuestEvents.ContainsKey(sceneName)) {
				string questEvent = StashSceneQuestEvents[sceneName];
				return questEvent == null || QuestEventManager.Instance.HasQuestEvent(questEvent);
			}
			return false;
		}
		
		protected void TrySendToStash(ItemDisplay itemDisplay, Character stashOwner) {
			if (IsInStashArea()) {
				itemDisplay.TryMoveTo(stashOwner.Stash);
			}
		}
	}
}