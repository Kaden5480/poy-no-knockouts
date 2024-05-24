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
            Harmony.CreateAndPatchAll(this.GetType());
        }

#elif MELONLOADER

using MelonLoader;

using UnityEngine;

[assembly: MelonInfo(typeof(NoKnockouts.Plugin), "NoKnockouts", "0.1.0", "Kaden5480")]
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
        [HarmonyPrefix]
        static bool PatchKnockout() {
            if (GameManager.control.permaDeathEnabled || GameManager.control.freesoloEnabled) {
                FallingEvent.fallenToDeath = false;
                return true;
            }

            return false;
        }

    }
}
