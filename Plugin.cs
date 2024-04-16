using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Object = UnityEngine.Object;
using Configgy;
using System.Runtime.InteropServices;

namespace FNAF
{
    // Load plugin
    [BepInPlugin("com.derp.fnaf", "UltraFNAF", "1.0.0")]
    [BepInDependency("Hydraxous.ULTRAKILL.Configgy", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        private ConfigBuilder config;

        public static Plugin Instance;

        private void Awake()
        {
            this.config = new ConfigBuilder("com.derp.fnaf", "UltraFNAF");
            this.config.BuildAll();

            Instance = this;
            Logger.LogInfo("Loaded plugin.");
            Harmony har = new Harmony("com.derp.fnaf");
            har.PatchAll();
        }
    }

    // Death behavior
    [HarmonyPatch(typeof(LaughingSkull), "Start")]
    class PatchDeath // : MonoBehaviour
    {
        // Import the necessary function from user32.dll
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_MINIMIZE = 6;

        static void Postfix()
        {   
            if(FNAFConfig.modEnabled.Value) {
                string path = FNAFConfig.gamePath;
                bool close = FNAFConfig.deathClose.Value;

                Process.Start(path);

                if (!close)
                {
                    Application.Quit();
                }
                else
                {
                    // Minimize the window using platform-specific functionality
                    IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
                    ShowWindow(hWnd, SW_MINIMIZE);
                }
            }
        }
    }
    public class FNAFConfig : MonoBehaviour
    {
        [Configgable("", "Enable Mod")]
        public static ConfigToggle modEnabled = new ConfigToggle(true);

        [Configgable("", "Game Path")]
        public static string gamePath = "C:/Program Files (x86)/Steam/steamapps/common/Five Nights at Freddy's 2/FiveNightsatFreddys2.exe";

        [Configgable("", "Minimize Game on Death")]
        public static ConfigToggle deathClose = new ConfigToggle(false);
    }
}
