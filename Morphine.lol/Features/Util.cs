using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using HarmonyLib;

namespace Morphine.Features
{
    public class Util
    {
        public static float Delay;

        public static void StartDelay(Action action, float time)
        {
            if (Time.time >= Delay + time)
            {
                Delay = Time.time;
                action();
            }
        }

        public static void GetOwnershipOverPhotonView(PhotonView view)
        {
            // no
        }
    }
}
