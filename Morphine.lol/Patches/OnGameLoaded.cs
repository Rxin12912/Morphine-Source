using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Photon.Pun;
using Steamworks;
using UnityEngine;

namespace Morphine.Patches
{
   [HarmonyPatch(typeof(GorillaLocomotion.Player), "AntiTeleportTechnology")]
    public class AntiTeleportTechnology
    {
        public static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player), "GetSlidePercentage")]
    public class GetSlidePercentage
    {
        public static void Postfix(ref float __result)
        {
            if (AllowGrip)
            {
                __result = 0f;
            }
        }

        public static bool AllowGrip = false;
    }
}
