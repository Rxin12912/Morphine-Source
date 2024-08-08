using GorillaLocomotion.Gameplay;
using HarmonyLib;
using Morphine.Framework.Components;
using Morphine.Framework.Helpers;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Morphine.Features
{
    public class GunTemplate
    {
        public static GameObject pointer = null;
        public static GameObject line = null;
        public static Color32 PointerColor = new Color32(108, 66, 245, 255);
        public static Color32 LineColor = new Color32(108, 66, 245, 255);

        public static VRRig LockedPlayer;
        public static bool IsLocked;

        public static void StartPlayerGun(Action<Photon.Realtime.Player> action, Action onDisable, bool PlayerGunLock)
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(UnityEngine.XR.XRNode.RightHand)))
            {
                RaycastHit raycastHit;
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    GameObject.Destroy(pointer.GetComponent<Rigidbody>());
                    GameObject.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    pointer.GetComponent<Renderer>().material.color = PointerColor;
                    pointer.transform.localScale = new Vector3(.13f, .13f, .13f);

                    line = new GameObject("Line");
                    var comp = line.AddComponent<LineRenderer>();
                    comp.material.shader = Shader.Find("GUI/Text Shader");
                    comp.startWidth = 0.025f;
                    comp.endWidth = 0.025f;
                    comp.startColor = LineColor;
                    comp.endColor = LineColor;
                }
                pointer.transform.position = raycastHit.point;
                line.GetComponent<LineRenderer>().SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                line.GetComponent<LineRenderer>().SetPosition(1, pointer.transform.position);
                if (Controller.GetButton(ControllerInputPoller.TriggerFloat(UnityEngine.XR.XRNode.RightHand)))
                {
                    VRRig Player = null;
                    Photon.Realtime.Player Owner = null;

                    if (PlayerGunLock)
                    {
                        if (raycastHit.collider.GetComponentInParent<VRRig>() != null &&
                            raycastHit.collider.GetComponentInParent<VRRig>() != GorillaTagger.Instance.offlineVRRig && LockedPlayer == null && !IsLocked)
                        {
                            LockedPlayer = raycastHit.collider.GetComponentInParent<VRRig>();
                        }
                        if (LockedPlayer != null)
                        {
                            IsLocked = true;
                            pointer.transform.position = LockedPlayer.transform.position;
                            line.GetComponent<LineRenderer>().SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                            line.GetComponent<LineRenderer>().SetPosition(1, pointer.transform.position);
                        }
                        else
                        {
                            IsLocked = false;
                            pointer.transform.position = raycastHit.point;
                        }
                        Player = LockedPlayer;
                        Owner = LockedPlayer.Creator;
                    }
                    else
                    {
                        IsLocked = false;
                        Player = raycastHit.collider.GetComponentInParent<VRRig>();
                        Owner = Player.Creator;
                    }

                    action(Owner);
                }
                else
                {
                    LockedPlayer = null;
                    IsLocked = false;
                    if (onDisable != null)
                    {
                        onDisable();
                    }
                }
            }
            else
            {
                LockedPlayer = null;
                IsLocked = false;
                GameObject.Destroy(line);
                GameObject.Destroy(pointer);
            }
        }

        public static void StartNormalGun(Action<GameObject> onTrigger, Action onTriggerStop, bool AllowRepeat)
        {
            bool useRepeat = false;
            if (Controller.GetButton(ControllerInputPoller.GripFloat(UnityEngine.XR.XRNode.RightHand)))
            {
                RaycastHit raycastHit;
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    GameObject.Destroy(pointer.GetComponent<Rigidbody>());
                    GameObject.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    pointer.GetComponent<Renderer>().material.color = PointerColor;
                    pointer.transform.localScale = new Vector3(.13f, .13f, .13f);

                    line = new GameObject("Line");
                    var comp = line.AddComponent<LineRenderer>();
                    comp.material.shader = Shader.Find("GUI/Text Shader");
                    comp.startWidth = 0.025f;
                    comp.endWidth = 0.025f;
                    comp.startColor = LineColor;
                    comp.endColor = LineColor;
                }
                pointer.transform.position = raycastHit.point;
                line.GetComponent<LineRenderer>().SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                line.GetComponent<LineRenderer>().SetPosition(1, pointer.transform.position);
                if (Controller.GetButton(ControllerInputPoller.TriggerFloat(UnityEngine.XR.XRNode.RightHand)))
                {
                    if (AllowRepeat)
                    {
                        onTrigger(pointer);
                    }
                    else
                    {
                        if (!useRepeat)
                        {
                            useRepeat = true;
                            onTrigger(pointer);
                        }
                    }
                }
                else
                {
                    if (!AllowRepeat)
                    {
                        useRepeat = false;
                    }
                    if (onTriggerStop != null)
                    {
                        onTriggerStop();
                    }
                }
            }
            else
            {
                if (!AllowRepeat)
                {
                    useRepeat = false;
                }
                GameObject.Destroy(line);
                GameObject.Destroy(pointer);
            }
        }
    }
}
