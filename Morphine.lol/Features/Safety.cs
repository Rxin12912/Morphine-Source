using GorillaTagScripts;
using HarmonyLib;
using Morphine.Framework.Helpers;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Morphine.Features
{
    public class Safety
    {
        public static void AntiReport()
        {
            foreach (GorillaPlayerScoreboardLine line in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                if (line.linePlayer == NetworkSystem.Instance.LocalPlayer)
                {
                    foreach (VRRig Player in GorillaParent.instance.vrrigs)
                    {
                        float Distance = Vector3.Distance(Player.rightHand.rigTarget.transform.position, line.reportButton.transform.position); // never added a check for left hand so if you want you can paste this and add left hand checks
                        if (Distance <= .3f)
                        {
                            string RoomName = PhotonNetwork.CurrentRoom.Name;
                            PhotonNetwork.Disconnect();
                            Notifications.SendNotification($"Somebody Attempted to Report you. Disconnected from {RoomName}");
                        }
                    }
                }
            }
        }

        public static void AntiModerator()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                if (Player.concatStringOfCosmeticsAllowed.Contains("LBAAK"))
                {
                    string RoomName = PhotonNetwork.CurrentRoom.Name;
                    PhotonNetwork.Disconnect();
                    Notifications.SendNotification($"A Moderator has entered your room, Disconnected from {RoomName}");
                }
            }
        }

        public static void AntiRatelimit()
        {
            // no
        }

    }
}
