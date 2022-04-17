using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace Rowing
{
    [HarmonyPatch]
    class Patches
    {
		[HarmonyPatch(typeof(Ship), nameof(Ship.GetSailForce))]
		private class ShipGetSailForce
		{
			private static void Postfix(Ship __instance, ref Vector3 __result)
			{
				int playersToConsider = __instance.m_players.Count - 1;
				if (playersToConsider == 0) return;

				int speedMultipler = playersToConsider * Rowing.PercentageToIncreaseSpeedPerPlayer.Value;
				__result += (__result / 100) * speedMultipler;
			}
		}
	}
}
