using System;
using System.Reflection;
using UnityEngine.XR;
using UnityEngine;
using Morphine.Framework.Helpers;
using Morphine.Framework;
using System.Diagnostics;
using Morphine.Menu.Helpers;

namespace Morphine.Features
{
    public class LocalPlayer
    {
        public static bool RightToggle;

        public static bool LeftToggle;

        public static GameObject rPlat;

        public static GameObject lPlat;

        public static Vector3 scale = new Vector3(0.0150f, 0.20f, 0.3190f);

        public static float GhostCooldown;

        public static float InvisCooldown;

        public static bool GhostToggled;

        public static bool InvisToggled;

        public static Vector3 walkPos;

        public static Vector3 walkNormal;

        public static GameObject checkpoint = null;

        public static GameObject blackhole = null;

        public static void Platforms()
        {
            var gripdownright = ControllerInputPoller.instance.rightControllerGripFloat;
            var gripdownleft = ControllerInputPoller.instance.leftControllerGripFloat;
            if (Controller.GetButton(gripdownright) && RightToggle)
            {
                rPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rPlat.GetComponent<Renderer>().material.color = MenuColors.PlatformColor;
                rPlat.transform.localScale = scale;
                rPlat.transform.position = new Vector3(0f, -0.00780f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                rPlat.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                RightToggle = false;
            }
            if (gripdownright != 1f)
            {
                GameObject.Destroy(rPlat);
                RightToggle = true;
            }
            if (Controller.GetButton(gripdownleft) && LeftToggle)
            {
                lPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lPlat.GetComponent<Renderer>().material.color = MenuColors.PlatformColor;
                lPlat.transform.localScale = scale;
                lPlat.transform.position = new Vector3(0f, -0.00780f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                lPlat.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                LeftToggle = false;
            }
            if (gripdownleft != 1f)
            {
                GameObject.Destroy(lPlat);
                LeftToggle = true;
            }
        }

        public static void StickyPlatforms()
        {
            var gripdownright = ControllerInputPoller.instance.rightControllerGripFloat;
            var gripdownleft = ControllerInputPoller.instance.leftControllerGripFloat;
            if (Controller.GetButton(gripdownright) && RightToggle)
            {
                rPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rPlat.GetComponent<Renderer>().material.color = MenuColors.PlatformColor;
                rPlat.transform.localScale = new Vector3(0.0900f, 0.20f, 0.3190f);
                rPlat.transform.position = new Vector3(0f, -0.00780f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                rPlat.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                RightToggle = false;
            }
            if (gripdownright != 1f)
            {
                GameObject.Destroy(rPlat);
                RightToggle = true;
            }
            if (Controller.GetButton(gripdownleft) && LeftToggle)
            {
                lPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lPlat.GetComponent<Renderer>().material.color = MenuColors.PlatformColor;
                lPlat.transform.localScale = new Vector3(0.0900f, 0.20f, 0.3190f);
                lPlat.transform.position = new Vector3(0f, -0.00780f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                lPlat.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                LeftToggle = false;
            }
            if (gripdownleft != 1f)
            {
                GameObject.Destroy(lPlat);
                LeftToggle = true;
            }
        }

        public static void TransformFlight()
        {
            if (ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaLocomotion.Player.Instance.transform.position +=
                    GorillaLocomotion.Player.Instance.headCollider.transform.forward *
                    Time.deltaTime * 25f;
            }
        }

        public static void Noclip()
        {
            foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll(typeof(MeshCollider)))
            {
                if (Controller.GetButton(ControllerInputPoller.TriggerFloat(UnityEngine.XR.XRNode.RightHand)))
                {
                    collider.enabled = false;
                }
                else
                {
                    collider.enabled = true;
                }
            }
        }

        public static void Noclip(bool state)
        {
            foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll(typeof(MeshCollider)))
            {
                collider.enabled = !state;
            }
        }

        public static void TeleportGun()
        {
            GunTemplate.StartNormalGun(pointer =>
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaLocomotion.Player.Instance.transform.position = pointer.transform.position - GorillaLocomotion.Player.Instance.bodyCollider.transform.position + GorillaLocomotion.Player.Instance.transform.position;
            }, null, false);
        }

        public static void ToggleSpeed(float multiplier, float max, bool state)
        {
            if (state)
            {
                GorillaLocomotion.Player.Instance.jumpMultiplier = multiplier;
                GorillaLocomotion.Player.Instance.maxJumpSpeed = max;
            }
        }

        public static void CarMonke()
        {
            if (Controller.GetButton(ControllerInputPoller.TriggerFloat(XRNode.RightHand)))
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity += GorillaLocomotion.Player.Instance.bodyCollider.transform.forward * Time.deltaTime * 22f;
            }
            else if (Controller.GetButton(ControllerInputPoller.TriggerFloat(XRNode.LeftHand)))
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity -= GorillaLocomotion.Player.Instance.bodyCollider.transform.forward * Time.deltaTime * 22f;
            }
        }

        public static void Checkpoint()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                if (checkpoint == null)
                {
                    checkpoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    GameObject.Destroy(checkpoint.GetComponent<Rigidbody>());
                    GameObject.Destroy(checkpoint.GetComponent<SphereCollider>());
                    GameObject.Destroy(checkpoint.GetComponent<Collider>());
                    checkpoint.transform.localScale = new Vector3(.2f, .2f, .2f);
                    checkpoint.GetComponent<Renderer>().material.color = Color.red;
                }
                checkpoint.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            }
            if (Controller.GetButton(ControllerInputPoller.TriggerFloat(XRNode.RightHand)))
            {
                checkpoint.GetComponent<Renderer>().material.color = Color.green;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaLocomotion.Player.Instance.transform.position = checkpoint.transform.position - GorillaLocomotion.Player.Instance.bodyCollider.transform.position + GorillaLocomotion.Player.Instance.transform.position;
            }
        }

        public static void Blackhole()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                if (blackhole == null)
                {
                    blackhole = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    GameObject.Destroy(blackhole.GetComponent<Rigidbody>());
                    GameObject.Destroy(blackhole.GetComponent<SphereCollider>());
                    GameObject.Destroy(blackhole.GetComponent<Collider>());
                    blackhole.transform.localScale = new Vector3(.3f, .3f, .3f);
                    blackhole.GetComponent<Renderer>().material.color = Color.black;
                }
                blackhole.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            }
            if (Controller.GetButton(ControllerInputPoller.TriggerFloat(XRNode.RightHand)))
            {
                Noclip(true);
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Teleportation.Teleport(Vector3.MoveTowards(GorillaLocomotion.Player.Instance.transform.position, blackhole.transform.position, Time.deltaTime * 6f));
                if (GorillaLocomotion.Player.Instance.transform.position == blackhole.transform.position)
                {
                    Noclip(false);
                }
            }
            else
            {
                Noclip(false);
            }
        }

        public static void WallWalk()
        {
            if ((GorillaLocomotion.Player.Instance.wasLeftHandTouching || GorillaLocomotion.Player.Instance.wasRightHandTouching) && ControllerInputPoller.instance.rightGrab)
            {
                FieldInfo fieldInfo = typeof(GorillaLocomotion.Player).GetField("lastHitInfoHand", BindingFlags.NonPublic | BindingFlags.Instance);
                RaycastHit ray = (RaycastHit)fieldInfo.GetValue(GorillaLocomotion.Player.Instance);
                walkPos = ray.point;
                walkNormal = ray.normal;
            }

            if (!ControllerInputPoller.instance.rightGrab)
            {
                walkPos = Vector3.zero;
            }

            if (walkPos != Vector3.zero)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(walkNormal * -10, ForceMode.Acceleration);
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (9.81f / Time.deltaTime)), ForceMode.Acceleration);
            }
        }

        public static void UpDown()
        {
            if (Controller.GetButton(ControllerInputPoller.TriggerFloat(XRNode.RightHand)))
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 20f, 0f), ForceMode.Impulse);
            }
            else if (Controller.GetButton(ControllerInputPoller.TriggerFloat(XRNode.LeftHand)))
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -20f, 0f), ForceMode.Impulse);
            }
        }

        public static void IronMonke()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(20f * GorillaLocomotion.Player.Instance.rightControllerTransform.right.x, 20f * GorillaLocomotion.Player.Instance.rightControllerTransform.right.y, 20f * GorillaLocomotion.Player.Instance.rightControllerTransform.right.z), ForceMode.Acceleration);
            }
            else if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.LeftHand)))
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(-20f * GorillaLocomotion.Player.Instance.leftControllerTransform.right.x, -20f * GorillaLocomotion.Player.Instance.leftControllerTransform.right.y, -20f * GorillaLocomotion.Player.Instance.leftControllerTransform.right.z), ForceMode.Acceleration);
            }
        }

        public static void GhostMonke()
        {
            if (ControllerInputPoller.instance.rightControllerSecondaryButton && Time.time >= GhostCooldown + .2f)
            {
                GhostCooldown = Time.time;
                if (!GhostToggled)
                {
                    GhostToggled = !GhostToggled;
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                }
                else if (GhostToggled)
                {
                    GhostToggled = !GhostToggled;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }

        public static void InvisibleMonke()
        {
            if (ControllerInputPoller.instance.rightControllerSecondaryButton && Time.time >= InvisCooldown + .2f)
            {
                InvisCooldown = Time.time;
                if (!InvisToggled)
                {
                    InvisToggled = !InvisToggled;
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(100f, 0f, 100f);
                }
                else if (InvisToggled)
                {
                    InvisToggled = !InvisToggled;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }
    }
}
