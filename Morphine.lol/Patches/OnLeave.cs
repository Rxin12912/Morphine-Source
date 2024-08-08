using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Morphine.Framework.Helpers;
using Photon.Pun;

namespace Morphine.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    internal class OnLeave : MonoBehaviourPunCallbacks
    {
        public static void Prefix(Photon.Realtime.Player otherPlayer)
        {
            if (playerLeft != otherPlayer)
            {
                if (otherPlayer == PhotonNetwork.LocalPlayer)
                {
                    playerLeft = otherPlayer;
                    return;
                }
                Notifications.SendNotification($"{otherPlayer.NickName}", "LEAVE");
                playerLeft = otherPlayer;
            }
        }
        public static Photon.Realtime.Player playerLeft;
    }
}
