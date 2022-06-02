using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace BetterInventory {
	[BepInPlugin(GUID, NAME, VERSION)]
	public class BetterInventory : BaseUnityPlugin {
		public const string GUID = "faeryn.betterinventory";
		public const string NAME = "BetterInventory";
		public const string VERSION = "1.0.0";
		private const string DISPLAY_NAME = "Better Inventory";
		internal static ManualLogSource Log;
		
		internal void Awake() {
			Log = this.Logger;
			Log.LogMessage($"Starting {NAME} {VERSION}");
			new Harmony(GUID).PatchAll();
		}
		

	}
}