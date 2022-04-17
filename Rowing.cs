using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using HarmonyLib;
using ServerSync;

namespace Rowing
{
    [BepInPlugin(PluginGUID, PluginGUID, Version)]
    public class Rowing : BaseUnityPlugin
    {
        public const string PluginGUID = "Detalhes.Rowing";
        public const string Name = "Rowing";
        public const string Version = "1.0.0";

        ConfigSync configSync = new ConfigSync("Detalhes.Rowing") { DisplayName = "Rowing", CurrentVersion = Version, MinimumRequiredVersion = Version };

        Harmony _harmony = new Harmony(PluginGUID);

        public static ConfigEntry<int> PercentageToIncreaseSpeedPerPlayer;

        public void Awake()
        {
            PercentageToIncreaseSpeedPerPlayer = config("Server config", "PercentageToIncreaseSpeedPerPlayer", 30,
                   new ConfigDescription("PercentageToIncreaseSpeedPerPlayer", null));
            _harmony.PatchAll();
        }

        ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) => config(group, name, value, new ConfigDescription(description), synchronizedSetting);

    }
}
