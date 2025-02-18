using HarmonyLib;

#if BEPINEX

using BepInEx;

using UnityEngine;

namespace NoKnockouts {
    [BepInPlugin("com.github.Kaden5480.poy-no-knockouts", "NoKnockouts", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        public void Awake() {
            Harmony.CreateAndPatchAll(typeof(PatchKnockout));
        }

#elif MELONLOADER

using MelonLoader;

using UnityEngine;

[assembly: MelonInfo(typeof(NoKnockouts.Plugin), "NoKnockouts", PluginInfo.PLUGIN_VERSION, "Kaden5480")]
[assembly: MelonGame("TraipseWare", "Peaks of Yore")]

namespace NoKnockouts {
    public class Plugin: MelonMod {

#endif

        /**
         * <summary>
         * Patches the knockout animation out in normal mode.
         * </summary>
         */
        [HarmonyPatch(typeof(FallingEvent), "FellToDeath")]
        static class PatchKnockout {
            static bool Prefix(FallingEvent __instance) {
                if (GameManager.control.permaDeathEnabled || GameManager.control.freesoloEnabled) {
                    return true;
                }

                __instance.HurtSound();

                __instance.falls++;
                GameManager.control.fallTimes++;
                GameManager.control.global_stats_falls++;

                FallingEvent.fallenToDeath = false;

                return false;
            }
        }
    }
}
