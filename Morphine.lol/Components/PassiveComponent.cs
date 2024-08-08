using Morphine.Framework;
using Morphine.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace Morphine.Components
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player), "FixedUpdate", MethodType.Normal)]
    public class PassiveComponent
    {
        public static VRRig Overlay = null;

        static bool NotDestroyed;

        static bool ChangedBack;
        private static void Prefix()
        {
            if (Settings.ShowCham)
            {
                if (!GorillaTagger.Instance.offlineVRRig.enabled)
                {
                    NotDestroyed = true;
                    Material mat = new Material(Shader.Find("GUI/Text Shader"));
                    mat.color = MenuColors.TransparentRigColor;
                    if (Overlay == null)
                    {
                        Overlay = UnityEngine.Object.Instantiate<VRRig>(GorillaTagger.Instance.offlineVRRig, GorillaLocomotion.Player.Instance.transform.position, GorillaLocomotion.Player.Instance.transform.rotation);
                        Overlay.enabled = true;
                        OnDisable.Prefix(GorillaTagger.Instance.offlineVRRig);
                    }
                    Overlay.mainSkin.material = mat;
                    Overlay.headConstraint.transform.position = GorillaLocomotion.Player.Instance.headCollider.transform.position;
                    Overlay.headConstraint.transform.rotation = GorillaLocomotion.Player.Instance.headCollider.transform.rotation;
                    Overlay.head.rigTarget.transform.position = GorillaLocomotion.Player.Instance.headCollider.transform.position;
                    Overlay.head.rigTarget.transform.rotation = GorillaLocomotion.Player.Instance.headCollider.transform.rotation;
                    Overlay.rightHandTransform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                    Overlay.rightHandTransform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                    Overlay.leftHandTransform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                    Overlay.leftHandTransform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                    Overlay.transform.position = GorillaLocomotion.Player.Instance.transform.position;
                    Overlay.transform.rotation = GorillaLocomotion.Player.Instance.transform.rotation;
                }
                else
                {
                    if (NotDestroyed)
                    {
                        NotDestroyed = false;
                        GameObject.Destroy(Overlay.gameObject);
                    }
                }
            }

            if (Settings.AlwaysShowCham)
            {
                ChangedBack = false;
                Material mat = new Material(Shader.Find("GUI/Text Shader"));
                mat.color = MenuColors.TransparentRigColor;
                GorillaTagger.Instance.offlineVRRig.mainSkin.material = mat;
            }
            else
            {
                if (!ChangedBack)
                {
                    GorillaTagger.Instance.offlineVRRig.ChangeMaterialLocal(GorillaTagger.Instance.offlineVRRig.currentMatIndex);
                    ChangedBack = true;
                }
            }
        }
    }
}
