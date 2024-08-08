using UnityEngine;
using Morphine.Framework.Components;
using Morphine.Menu.Components;
using Morphine.Framework.Helpers;
using System.IO;
using System;
using BepInEx;

namespace Morphine.Framework
{
    public class Loader
    {
        public static GameObject Cheat;

        public static void CheckData()
        {
            // Can't show you the sigma wolf checks
        }

        public static void StartPlugin()
        {
            Cheat = GameObject.Find("BepInEx_Manager");
            Cheat.AddComponent<Plugin>();
            Cheat.AddComponent<Notifications>();
            Cheat.AddComponent<Arraylist>();
            GameObject.DontDestroyOnLoad(Cheat);
        }
    }
}
