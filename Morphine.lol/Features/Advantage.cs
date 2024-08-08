using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Morphine.Features
{
    public class Advantage
    {
        public static float SilentAimCooldown;

        public static VRRig StickySilentPlayer;

        public static VRRig GetRandomVRRig()
        {
            System.Random random = new System.Random();
            Photon.Realtime.Player[] PlayerList = PhotonNetwork.PlayerListOthers;
            return GorillaGameManager.instance.FindPlayerVRRig(PlayerList[random.Next(PlayerList.Length)]);
        }

        public static void SlingshotSilentAim()
        {
            var Manager = GameObject.Find("Gorilla Paintbrawl Manager").GetComponent<GorillaPaintbrawlManager>();
            if (Time.time >= SilentAimCooldown + .05f)
            {
                VRRig PlayerV = GetRandomVRRig();

                if (PlayerV.mainSkin.material.name.Contains("bluealive") && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("orangealive")
                    || PlayerV.mainSkin.material.name.Contains("orangealive") && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("bluealive")
                    || PlayerV.mainSkin.material.name.Contains("orangealive") && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("blue")
                    || PlayerV.mainSkin.material.name.Contains("bluealive") && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("orange"))
                {
                    if (Manager.playerLives.TryGetValue(PlayerV.Creator.ActorNumber, out int lives))
                    {
                        if (lives == 0)
                        {
                            PlayerV = GetRandomVRRig();
                            return;
                        }
                    }
                    foreach (SlingshotProjectile sp in GameObject.Find("Environment Objects/PersistentObjects_Prefab/GlobalObjectPools").GetComponentsInChildren<SlingshotProjectile>())
                    {
                        if (sp.projectileOwner == PhotonNetwork.LocalPlayer)
                        {
                            sp.gameObject.transform.position = PlayerV.transform.position;
                        }
                    }
                }
                SilentAimCooldown = Time.time;
            }
        }
    }
}
