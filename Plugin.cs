using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics;

namespace FNAF
{
    // Load plugin
    [BepInPlugin("com.derp.fnaf", "har har har", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public ConfigFile customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "FNAF.cfg"), true);
        public ConfigEntry<string> FNAFPath; // Change to public

        public static Plugin Instance;

        private void Awake()
        {
            FNAFPath = customFile.Bind("General", // The section under which the option is shown
                "FNAFPath", // The key of the configuration option in the configuration file
                "C:/Program Files (x86)/Steam/steamapps/common/Five Nights at Freddy's 2/FiveNightsatFreddys2.exe", // The default value
                "Where your FNAF game is installed (or any other game ig). HEY READ THIS: MAKE SURE TO USE / SYMBOLS AND NOT \\. THE GAME WILL NOT WORK IF YOU USE THESE"); // Description of the option to show in the config file

            Instance = this;
            // Plugin startup logic
            Logger.LogInfo("Loaded plugin.");
            // I have no idea what I'm doing
            Harmony har = new Harmony("com.derp.fnaf");
            har.PatchAll();
        }
    }

    // Death behavior
    [HarmonyPatch(typeof(LaughingSkull), nameof(LaughingSkull.PlayAudio))]
    class PatchDeath // : MonoBehaviour
    {
        static void Postfix()
        {
            string path = Application.dataPath;

            Plugin plugin = Plugin.Instance; // Accessing the Plugin instance
            string FNAFPath = plugin.FNAFPath.Value; // Accessing the FNAFPath variable

            // Start the process with the FNAFPath
            Process.Start(FNAFPath);

            Application.Quit();
        }
    }
}
