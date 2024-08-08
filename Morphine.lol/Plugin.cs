using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using Morphine.Components;
using Morphine.Features;
using Morphine.Framework;
using Morphine.Framework.Elements;
using Morphine.Framework.Helpers;
using UnityEngine;

namespace Morphine
{
    /*
	Made by Rxin
	Goodbye lol.
     */
	
    public struct Metadata
    {
        public const string Name = "Morphine.lol";
        public const string GUID = "com.rxin.morphinelol";
        public const string Version = "2.0.0";
    }

    [BepInPlugin(Metadata.GUID, Metadata.Name, Metadata.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        public List<string> Feature = new List<string>();

        public static GameObject Components;

        private void Awake()
        {
            Instance = this;
            var harmony = new Harmony(Metadata.GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        private void Update()
        {
        }

        public void GetFeatures()
        {
            foreach (ButtonInfo info in MenuComponent.Buttons)
            {
                Feature.Add(info.buttonText);
            }
        }
    }
}
