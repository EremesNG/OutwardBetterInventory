using System;
using HarmonyLib;

namespace BetterInventory {
	public static class Utilities {
		public static bool IsVheosLegacyStashesActive() {
			Type stashesMod = AccessTools.TypeByName("Vheos.Mods.Outward.Stashes");
			if (stashesMod == null) {
				BetterInventory.Log.LogDebug("Vheos Modpack not found");
				return false;
			}
			object stashType = Traverse.Create(stashesMod).Field("_stashType").Property("Value").GetValue();
			BetterInventory.Log.LogDebug($"Vheos Modpack stashType: '{stashType}'");
			if (stashType == null) {
				BetterInventory.Log.LogDebug("Unable to get Vheos Modpack stashType");
				return false;
			}
			return (int)stashType == 1; // CityBound
		}
	}
}