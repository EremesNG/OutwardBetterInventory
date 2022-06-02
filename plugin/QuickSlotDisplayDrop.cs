using BetterInventory.Patches;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BetterInventory {
	public class QuickSlotDisplayDrop : ItemDisplayDrop {
		private Image imgHighlight;
		private CharacterUI parentCharUI;
		private QuickSlotDisplayHelper helper;
		

		public override void Init() {
			base.Init();
			helper = new QuickSlotDisplayHelper(GetComponent<QuickSlotDisplay>());
			imgHighlight = helper.GetQuickSlotIcon();
			HighlightColor.a = 1.0f;
			NormalColor.a = 1.0f;
			DeniedColor.a = 1.0f;
			parentCharUI = GetComponentInParent<CharacterUI>();
			BetterInventory.Log.LogInfo("QuickSlotDisplayDrop Init");
		}

		public override bool IsDropValid() {
			BetterInventory.Log.LogInfo("IsDropValid");
			if (!m_draggedDisplay) {
				BetterInventory.Log.LogInfo("IsDropValid false no m_draggedDisplay");
				return false;
			}
			return !m_draggedDisplay.IsCurrencyDisplay && m_draggedDisplay.RefItem && !(m_draggedDisplay.RefItem.ParentContainer is MerchantPouch);
		}

		public override bool IsDraggingValid(ItemDisplay draggedDisplay) => draggedDisplay != null && draggedDisplay.Movable;

		public override void OnConfirmDrop(PointerEventData data) {
			BetterInventory.Log.LogInfo("OnConfirmDrop");
			ItemDisplay draggedElement = GetDraggedElement(data);
			if (draggedElement == null || draggedElement.RefItem == null) {
				return;
			}
			BetterInventory.Log.LogInfo("Setting slot");
			helper.GetQuickSlot().SetQuickSlot(draggedElement.RefItem);
		}

		public override void OnPointerEnter(PointerEventData _eventData) {
			BetterInventory.Log.LogInfo("Enter");
			base.OnPointerEnter(_eventData);
		}	


		public override CharacterUI ParentCharUI => parentCharUI;

		public override Image HighlightedImage => imgHighlight;
	
	}
}