using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterInventory.ContextMenu {
	public class SendToStashAction : ItemContextMenuAction {
		// Scene, (QuestEvent id, false if it has to be missing)
		private static readonly Dictionary<string, (string questEvent, bool reqValue)> StashSceneQuestEvents = new Dictionary<string, (string, bool)>{
			{"CierzoNewTerrain", ("qPEx275DTUSPbnv-PnFn7w", false)},
			{"Berg", ("g403vlCU6EG0s1mI6t_rFA", true)},
			{"Monsoon", ("shhCMFa-lUqbIYS9hRcsdg", true)},
			{"Levant", ("LpVUuoxfhkaWOgh6XLbarA", true)},
			{"Harmattan", ("0r087PIxTUqoj6N7z2HFNw", true)},
			{"NewSirocco", (null, true)}
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

		protected bool IsInStashArea() {
			Scene scene = SceneManager.GetActiveScene();
			string sceneName = scene.name;
			if (StashSceneQuestEvents.ContainsKey(sceneName)) {
				(string questEvent, bool reqValue) condition = StashSceneQuestEvents[sceneName];
				return condition.questEvent == null || QuestEventManager.Instance.HasQuestEvent(condition.questEvent) == condition.reqValue;
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