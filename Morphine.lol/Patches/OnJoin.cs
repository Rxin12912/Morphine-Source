using UnityEngine;
using HarmonyLib;
using Morphine.Framework.Helpers;
using Photon.Pun;

namespace Morphine.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class OnJoin : MonoBehaviour
    {
        public static Photon.Realtime.Player player;

        public static void Prefix(Photon.Realtime.Player newPlayer)
        {
            if (newPlayer != player)
            {
                if (newPlayer == PhotonNetwork.LocalPlayer)
                {
                    player = newPlayer;
                    return;
                }
                Notifications.SendNotification($"{newPlayer.NickName}", "JOIN");
                player = newPlayer;
            }
        }
    }
}
