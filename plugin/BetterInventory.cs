using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace BetterInventory {
	[BepInPlugin(GUID, NAME, VERSION)]
	public class BetterInventory : BaseUnityPlugin {
		public const string GUID = "faeryn.betterinventory";
		public const string NAME = "BetterInventory";
		public const string VERSION = "1.0.1";
		private const string DISPLAY_NAME = "Better Inventory";
		internal static ManualLogSource Log;
		
		public static ConfigEntry<bool> SendToStashEnabled;
		
		internal void Awake() {
			Log = this.Logger;
			Log.LogMessage($"Starting {NAME} {VERSION}");
			InitializeConfig();
			new Harmony(GUID).PatchAll();
		}
		
		private void InitializeConfig() {
			SendToStashEnabled = Config.Bind(DISPLAY_NAME, "Send to Stash", false, "Enables the 'Send to Stash' action on items while in town where you own a stash");
		}
	}
}