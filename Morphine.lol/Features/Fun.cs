using GorillaNetworking;
using Morphine.Framework;
using Morphine.Framework.Helpers;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Security.Cryptography;

namespace Morphine.Features
{
    public class Fun : MonoBehaviour
    {
        public static void ChangeName(string newName)
        {
            PhotonNetwork.LocalPlayer.NickName = newName;
            GorillaTagger.Instance.offlineVRRig.playerName = newName;
            GorillaComputer.instance.currentName = newName;
            GorillaComputer.instance.savedName = newName;
            GorillaComputer.instance.offlineVRRigNametagText.text = newName;
            PlayerPrefs.SetString("playerName", newName);
            PlayerPrefs.Save();
        }

        public static void Helicopter()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);
            GorillaTagger.Instance.offlineVRRig.transform.Rotate(new Vector3(0f, 10f, 0f));
            GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * -1f;
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * 1f;
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
        }

        public static void TPose()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);
            GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * -1f;
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * 1f;
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
        }

        public static void FakeNameTroll()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);
            GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.up ;
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.up;
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
        }

        public static void Spinbot()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.Player.Instance.bodyCollider.transform.position + new Vector3(0f, .15f, 0f);
            GorillaTagger.Instance.offlineVRRig.transform.Rotate(new Vector3(0f, 10f, 0f));
        }

        public static void Spaz()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(UnityEngine.XR.XRNode.RightHand)))
            {
                GorillaLocomotion.Player.Instance.rightControllerTransform.position = new Vector3(UnityEngine.Random.Range(float.MinValue, float.MaxValue), UnityEngine.Random.Range(float.MinValue, float.MaxValue), UnityEngine.Random.Range(float.MinValue, float.MaxValue));
                GorillaLocomotion.Player.Instance.leftControllerTransform.position = new Vector3(UnityEngine.Random.Range(float.MinValue, float.MaxValue), UnityEngine.Random.Range(float.MinValue, float.MaxValue), UnityEngine.Random.Range(float.MinValue, float.MaxValue));
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.position = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
            }
        }

        public static void HeadSpin(string axis, float speed)
        {
            if (axis == "x" || axis == "X")
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x += speed;
            }
            if (axis == "y" || axis == "Y")
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y += speed;
            }
            if (axis == "z" || axis == "Z")
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z += speed;
            }
        }

        public static void HandSpaz(float speed)
        {
            GorillaTagger.Instance.offlineVRRig.rightHand.trackingRotationOffset.x += speed;
            GorillaTagger.Instance.offlineVRRig.leftHand.trackingRotationOffset.x += speed;
            GorillaTagger.Instance.offlineVRRig.rightHand.trackingRotationOffset.y += speed;
            GorillaTagger.Instance.offlineVRRig.leftHand.trackingRotationOffset.y += speed;
            GorillaTagger.Instance.offlineVRRig.rightHand.trackingRotationOffset.z += speed;
            GorillaTagger.Instance.offlineVRRig.leftHand.trackingRotationOffset.z += speed;
        }

        public static void ResetHands()
        {
            GorillaTagger.Instance.offlineVRRig.RemoteRigUpdate();
        }

        public static void ResetHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
        }

        public static void Bees()
        {
            System.Random random = new System.Random();
            Photon.Realtime.Player[] PlayerList = PhotonNetwork.PlayerListOthers;
            Photon.Realtime.Player NetPlayer = PlayerList[random.Next(PlayerList.Length)];
            VRRig Player = GorillaGameManager.instance.FindPlayerVRRig(NetPlayer);
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(0f, 100f, 0f);
            Util.StartDelay(delegate
            {
                GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position;
            }, .05f);
        }

        public static void RapeGun()
        {
            GunTemplate.StartPlayerGun(owner =>
            {
                VRRig Player = GorillaGameManager.instance.FindPlayerVRRig(owner);
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.rotation = Player.transform.rotation;
                GorillaTagger.Instance.offlineVRRig.transform.position = Player.transform.position + Player.transform.forward * -(0.8f + Mathf.Sin(Time.frameCount / 10f) * 0.5f);

                if (Vector3.Distance(GorillaTagger.Instance.offlineVRRig.transform.position, Player.transform.position) <= 0.5f)
                {
                    Overpowered.PlaySplashEffect(
                        GorillaTagger.Instance.offlineVRRig.transform.position - new Vector3(0f, 0.09f, 0f),
                        GorillaTagger.Instance.offlineVRRig.transform.rotation,
                        1f,
                        .5f,
                        false,
                        false
                    );
                }
            }, ResetRig, Settings.PlayerGunLock);
        }

        public static void ChaseGun()
        {
            GunTemplate.StartPlayerGun(owner =>
            {
                VRRig Player = GorillaGameManager.instance.FindPlayerVRRig(owner);
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.LookAt(Player.transform.position);
                GorillaTagger.Instance.offlineVRRig.transform.position = Vector3.MoveTowards(GorillaTagger.Instance.transform.position, Player.transform.position, Mathf.PingPong(Time.time, 1f));
            }, ResetRig, Settings.PlayerGunLock);
        }

        public static void ResetRig()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }
    }
}
