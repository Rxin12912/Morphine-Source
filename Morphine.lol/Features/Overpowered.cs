using HarmonyLib;
using Morphine.Framework.Helpers;
using Morphine.Framework;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using GorillaLocomotion.Gameplay;
using UnityEngine.UIElements;
using GorillaTag;
using System.Net.NetworkInformation;
using GorillaTagScripts;
using JetBrains.Annotations;
using static UnityEngine.UI.GridLayoutGroup;
using ExitGames.Client.Photon;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.ProBuilder.Shapes;
using Steamworks;
using ExitGames.Client.Photon.StructWrapping;
using System.Reflection;
using static UnityEngine.Rendering.DebugUI;
using Photon.Realtime;
using GorillaNetworking;
using GorillaGameModes;
using Morphine.Utility;

namespace Morphine.Features
{
    public class Overpowered
    {
        public static Color colorr;
        public static int SelectedRGBColor;
        private static float angle;

        public static void PlaySplashEffect(Vector3 pos, Quaternion rot, float scale, float rad, bool bigsplash, bool enteringWater) // Working
        {
            Util.StartDelay(delegate
            {
                GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", RpcTarget.All, new object[]
                {
                    pos,
                    rot,
                    scale,
                    rad,
                    bigsplash,
                    enteringWater
                });
            }, .01f);
        }

        public static void PlaySound(int soundIndex, bool isLeftHand, float tapVolume) // Working
        {
            GorillaTagger.Instance.myVRRig.RPC("PlayHandTap", RpcTarget.All, new object[]
            {
                soundIndex,
                isLeftHand,
                tapVolume
            });
        }

        public static void SetRopeVelocity(Vector3 velocity)
        {
		// removed ropes so you gremlins don't throw it onto your pastes
        }
        public static void SetPlayerColor(Color color) // Working
        {
            PlayerPrefs.SetFloat("redValue", color.r);
            PlayerPrefs.SetFloat("greenValue", color.g);
            PlayerPrefs.SetFloat("blueValue", color.b);
            GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
            GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[]
            {
                color.r,
                color.g,
                color.b
            });
        }

        public static void SplashGun() // Working
        {
            GunTemplate.StartNormalGun(pointer =>
            {
                PlaySplashEffect(pointer.transform.position, pointer.transform.rotation, 25f, 1f, true, false);
                PlaySplashEffect(pointer.transform.position, pointer.transform.rotation, 25f, 1f, false, false);
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = pointer.transform.position - new Vector3(0f, 1f, 0f);
            }, Fun.ResetRig, true);
        }

        public static void SplashSelf() // Working
        {
            if (ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.bodyCollider.transform.position, Quaternion.identity, 25f, 1f, true, false);
            }
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.bodyCollider.transform.position, Quaternion.identity, 25f, 1f, false, false);
            }
        }

        public static void SplashBarrage() // Working
        {
            Vector3 pos = GorillaLocomotion.Player.Instance.bodyCollider.transform.position + new Vector3(
                UnityEngine.Random.Range(-1.25f, 1.25f),
                UnityEngine.Random.Range(-.5f, 2f),
                UnityEngine.Random.Range(-1.0f, 1.5f)
            );
            PlaySplashEffect(pos, Quaternion.identity, 25f, 1f, true, false);
            PlaySplashEffect(pos, Quaternion.identity, 25f, 1f, false, false);
        }

        public static void WaterBending() // Working
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(UnityEngine.XR.XRNode.RightHand)))
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.rightControllerTransform.position, GorillaLocomotion.Player.Instance.rightControllerTransform.rotation, 25f, 1f, true, false);
                PlaySplashEffect(GorillaLocomotion.Player.Instance.rightControllerTransform.position, GorillaLocomotion.Player.Instance.rightControllerTransform.rotation, 25f, 1f, false, false);
            }
            if (Controller.GetButton(ControllerInputPoller.GripFloat(UnityEngine.XR.XRNode.LeftHand)))
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.leftControllerTransform.position, GorillaLocomotion.Player.Instance.leftControllerTransform.rotation, 25f, 1f, true, false);
                PlaySplashEffect(GorillaLocomotion.Player.Instance.leftControllerTransform.position, GorillaLocomotion.Player.Instance.leftControllerTransform.rotation, 25f, 1f, false, false);
            }
        }

        public static void WaterCum() // Working
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.bodyCollider.transform.position - new Vector3(0f, .09f), GorillaLocomotion.Player.Instance.bodyCollider.transform.rotation, 25f, 1f, false, false);
            }
        }

        public static void WaterIronMonke() // Working
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.rightControllerTransform.position, Quaternion.identity, 25f, 1f, false, false);
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(20f * GorillaLocomotion.Player.Instance.rightControllerTransform.right.x, 20f * GorillaLocomotion.Player.Instance.rightControllerTransform.right.y, 20f * GorillaLocomotion.Player.Instance.rightControllerTransform.right.z), ForceMode.Acceleration);
            }
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.LeftHand)))
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.leftControllerTransform.position, Quaternion.identity, 25f, 1f, false, false);
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(-20f * GorillaLocomotion.Player.Instance.leftControllerTransform.right.x, -20f * GorillaLocomotion.Player.Instance.leftControllerTransform.right.y, -20f * GorillaLocomotion.Player.Instance.leftControllerTransform.right.z), ForceMode.Acceleration);
            }
        }

        public static void HandTapSpam() // Working
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(1, false, float.MaxValue);
            }
        }

        public static void WoodTapSpam() // Working
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(10, false, float.MaxValue);
            }
        }

        public static void DuckSpam() // Working
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(75, false, float.MaxValue);
            }
        }

        public static void RandomSpam1() // Working
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(53, false, float.MaxValue);
            }
        }

        public static void RandomSpam2() // Working
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(23, false, float.MaxValue);
            }
        }

        public static void RandomSpam3() // Working
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(67, false, float.MaxValue);
            }
        }

        public static void TagGun() // Working
        {
            GunTemplate.StartPlayerGun(player =>
            {
                VRRig vrrig = GorillaGameManager.instance.FindPlayerVRRig(player);
                if (!Visual.IsPlayerInfected(vrrig))
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position + new Vector3(0f, -1f, 0f);
                    GameMode.ReportTag(player);
                }

            }, delegate { GorillaTagger.Instance.offlineVRRig.enabled = true; }, Settings.PlayerGunLock);
        }

        public static void TagAll() // Working
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
            {
                VRRig vrrig = GorillaGameManager.instance.FindPlayerVRRig(player);
                if (!Visual.IsPlayerInfected(vrrig) && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position + new Vector3(0f, -1f, 0f);
                    GameMode.ReportTag(player);
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }

        public static void TagAura() // Working
        {
            VRRig Player = PlayerUtil.GetClosestPlayer();
            if (!PlayerUtil.IsPlayerInfected(Player))
            {
                GameMode.ReportTag(Player.Creator);
            }
        }

        public static void TagSelf() // Working
        {
            System.Random random = new System.Random();
            VRRig vrrig = GorillaGameManager.instance.FindPlayerVRRig(PhotonNetwork.PlayerListOthers[random.Next(PhotonNetwork.PlayerListOthers.Length)]);
            if (!PlayerUtil.IsPlayerInfected(GorillaTagger.Instance.offlineVRRig) && PlayerUtil.IsPlayerInfected(vrrig))
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.rightHand.rigTarget.transform.position;
            }
            else if (!PlayerUtil.IsPlayerInfected(vrrig))
            {
                vrrig = GorillaGameManager.instance.FindPlayerVRRig(PhotonNetwork.PlayerListOthers[random.Next(PhotonNetwork.PlayerListOthers.Length)]);
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
            else if (PlayerUtil.IsPlayerInfected(GorillaTagger.Instance.offlineVRRig))
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                MenuUtil.GetButton("Tag Self").Enabled = false;
                MenuBase.RefreshMenu();
            }
        }

        public static void FlickTagAura() // Working
        {
            VRRig vrrig = PlayerUtil.GetClosestPlayer();

            if (Controller.GetButton(ControllerInputPoller.TriggerFloat(XRNode.RightHand)))
            {
                if (!PlayerUtil.IsPlayerInfected(vrrig))
                {
                    GorillaLocomotion.Player.Instance.rightControllerTransform.position = vrrig.head.rigTarget.transform.position;
                }
            }
        }

        public static void AntiTag() // Working
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
            {
                VRRig vrrig = GorillaGameManager.instance.FindPlayerVRRig(player);
                if (Visual.IsPlayerInfected(vrrig) && Vector3.Distance(GorillaLocomotion.Player.Instance.bodyCollider.transform.position, vrrig.transform.position) >= GorillaTagManager.instance.tagDistanceThreshold)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.Player.Instance.bodyCollider.transform.position - new Vector3(0f, 100f, 0f);
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }


        public static void RGB() // Working
        {
            if (SelectedRGBColor == 0)
            {
                colorr = Color.red;
            }
            if (SelectedRGBColor == 1)
            {
                colorr = Color.green;
            }
            if (SelectedRGBColor == 2)
            {
                colorr = Color.blue;
            }
            if (SelectedRGBColor == 3)
            {
                colorr = Color.cyan;
            }
            if (SelectedRGBColor == 4)
            {
                colorr = Color.white;
            }
            Util.StartDelay(delegate
            {
                SelectedRGBColor++;
                SetPlayerColor(colorr);
                if (SelectedRGBColor > 4)
                {
                    SelectedRGBColor = 0;
                }
            }, .15f);
        }

        public static void Strobe() // Working
        {
            if (SelectedRGBColor == 0)
            {
                colorr = Color.red;
            }
            if (SelectedRGBColor == 1)
            {
                colorr = Color.green;
            }
            if (SelectedRGBColor == 2)
            {
                colorr = Color.blue;
            }
            if (SelectedRGBColor == 3)
            {
                colorr = Color.cyan;
            }
            if (SelectedRGBColor == 4)
            {
                colorr = Color.white;
            }
            Util.StartDelay(delegate
            {
                SelectedRGBColor++;
                SetPlayerColor(colorr);
                if (SelectedRGBColor > 4)
                {
                    SelectedRGBColor = 0;
                }
            }, .09f);
        }

	// Removed the ropes so you gremlins don't take it

        public static List<int> GetBounceAndWindPieces() // Broken
        {
            List<int> result = new List<int>();
            foreach (BuilderPiece piece in BuilderTable.instance.pieces)
            {
                if (piece.pieceType == 1120512569 || piece.pieceType == 532163265)
                {
                    result.Add(piece.pieceId);
                }
            }
            return result;
        }

        public static void FlingGun() // Broken
        {
            GunTemplate.StartPlayerGun(owner =>
            {
                System.Random random = new System.Random();
                Vector3 Velocity = new Vector3(UnityEngine.Random.Range(-10000f, 10000f), UnityEngine.Random.Range(-10000f, 10000f), UnityEngine.Random.Range(-10000f, 10000f));
                VRRig player = GorillaGameManager.instance.FindPlayerVRRig(owner);
                BuilderTableNetworking.instance.RequestDropPiece(BuilderTable.instance.GetPiece(random.Next(0, 642)), player.transform.position - new Vector3(0f, .06f, 0f), Quaternion.identity, Velocity, Vector3.zero);
            }, null, Settings.PlayerGunLock);
        }

        public static void FlingAll() // Broken
        {
            foreach (Photon.Realtime.Player owner in PhotonNetwork.PlayerListOthers)
            {
                System.Random random = new System.Random();
                Vector3 Velocity = new Vector3(UnityEngine.Random.Range(-10000f, 10000f), UnityEngine.Random.Range(-10000f, 10000f), UnityEngine.Random.Range(-10000f, 10000f));
                VRRig player = GorillaGameManager.instance.FindPlayerVRRig(owner);
                BuilderTableNetworking.instance.RequestDropPiece(
                    BuilderTable.instance.GetPiece(random.Next(0, 642)),
                    player.transform.position - new Vector3(0f, .06f, 0f),
                    Quaternion.identity,
                    Velocity,
                    Vector3.zero);
            }
        }

        public static void BlockOrbitGun() // Broken
        {
            GunTemplate.StartPlayerGun(owner =>
            {
                angle += 25f * Time.deltaTime;
                float x = GorillaTagger.Instance.offlineVRRig.transform.position.x + 0.5f * Mathf.Cos(angle);
                float y = GorillaTagger.Instance.offlineVRRig.transform.position.y + 1.5f;
                float z = GorillaTagger.Instance.offlineVRRig.transform.position.z + 0.5f * Mathf.Sin(angle);
                System.Random random = new System.Random();
                VRRig player = GorillaGameManager.instance.FindPlayerVRRig(owner);
                Vector3 pos = new Vector3(x, y, z);
                BuilderTableNetworking.instance.RequestDropPiece(
                    BuilderTable.instance.GetPiece(random.Next(0, 642)),
                    pos,
                    Quaternion.identity,
                    Vector3.zero,
                    Vector3.zero);
            }, null, Settings.PlayerGunLock);
        }

        public static void ExplodePieces() // Broken
        {
            System.Random rand = new System.Random();
            BuilderPiece SelectedPiece = BuilderTable.instance.GetPiece(rand.Next(0, 642));
            Vector3 Velocity = new Vector3(UnityEngine.Random.Range(0f, 10000f), UnityEngine.Random.Range(0f, 10000f), UnityEngine.Random.Range(0f, 10000f));
            BuilderTableNetworking.instance.RequestDropPiece(SelectedPiece, SelectedPiece.transform.position, Quaternion.identity, Velocity, Vector3.zero);
        }

        public static void BrickSpammer() // Broken
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                System.Random rand = new System.Random();
                BuilderPiece SelectedPiece = BuilderTable.instance.GetPiece(rand.Next(0, 642));
                Vector3 Velocity = GorillaLocomotion.Player.Instance.rightControllerTransform.forward * Time.deltaTime * 750f;
                BuilderTableNetworking.instance.RequestDropPiece(SelectedPiece, GorillaLocomotion.Player.Instance.rightControllerTransform.position, Quaternion.identity, Velocity, Vector3.zero);
            }
        }

        public static void ForceParty()
        {
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(Strings.RandomString(8), JoinType.JoinWithParty); // Still works im pretty sure lmao get into a party with them and it should still work - rxin
        }
    }
}
