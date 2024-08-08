using UnityEngine;
using Morphine.Framework.Helpers;
using Morphine.Features;
using System.Collections.Generic;
using Morphine.Framework.Elements;
using System.IO;
using BepInEx;
using System.Runtime.InteropServices;

namespace Morphine.Framework
{
    public class Settings
    {
        public static bool PlayerGunLock = false;
        public static bool ShowCham = false;
        public static bool AlwaysShowCham = false;
        public static bool AllowSkyBoxChanger = false;

        // Theme
        public static int CurrentThemeIndex = 0;

        // Configs
        public static List<string> FoundConfigs = new List<string>();

        // Security
        public static bool CheckedAtom = false;
        public static bool CheckedData = false;
        public static bool WasInjected = false;
        public static bool LoggedInWithData = false;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern ushort GlobalFindAtomA(string lpString);

        public static void SkyChanger()
        {
            Material material = new Material(Shader.Find("GorillaTag/UberShader"));
            material.color = Color32.Lerp(MenuColors.BackgroundColor, MenuColors.BackgroundColor2, Mathf.PingPong(Time.time, 1f));
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Standard Sky").GetComponent<Renderer>().material = material;
        }

        public static void SwitchTheme()
        {
            CurrentThemeIndex++;
            if (CurrentThemeIndex > 3)
            {
                CurrentThemeIndex = 0;
            }

            if (CurrentThemeIndex == 0)
            {
                Notifications.SendNotification("Set Theme Default");
                MenuColors.BackgroundColor = new Color32(18, 17, 17, 255);
                MenuColors.BackgroundColor2 = new Color32(18, 17, 17, 255);

                MenuColors.PageButtonColors = new Color32(36, 33, 33, 255);
                MenuColors.ButtonDisabledColor = new Color32(36, 33, 33, 255);
                MenuColors.ButtonEnabledColor = new Color32(97, 37, 194, 255);

                MenuColors.MenuPointerColor = new Color32(97, 37, 194, 255);
                MenuColors.PlatformColor = new Color32(97, 37, 194, 255);
                MenuColors.TransparentRigColor = new Color32(97, 37, 194, 100);

                GunTemplate.PointerColor = new Color32(97, 37, 194, 255);
                GunTemplate.LineColor = new Color32(97, 37, 194, 255);

                MenuColors.ArrayListLineColor = new Color32(97, 37, 194, 255);
                MenuColors.ArrayListLineColor2 = new Color32(126, 37, 194, 255);
            }
            if (CurrentThemeIndex == 1)
            {
                Notifications.SendNotification("Set Theme GorillaWare");
                MenuColors.BackgroundColor = new Color32(235, 52, 122, 255);
                MenuColors.BackgroundColor2 = new Color32(128, 52, 235, 255);

                MenuColors.PageButtonColors = new Color32(25, 25, 25, 255);
                MenuColors.ButtonDisabledColor = new Color32(25, 25, 25, 255);
                MenuColors.ButtonEnabledColor = new Color32(235, 52, 122, 255);

                MenuColors.MenuPointerColor = new Color32(235, 52, 122, 255);
                MenuColors.PlatformColor = new Color32(235, 52, 122, 255);
                MenuColors.TransparentRigColor = new Color32(235, 52, 122, 100);

                GunTemplate.PointerColor = new Color32(235, 52, 122, 255);
                GunTemplate.LineColor = new Color32(235, 52, 122, 255);

                MenuColors.ArrayListLineColor = MenuColors.BackgroundColor;
                MenuColors.ArrayListLineColor2 = MenuColors.BackgroundColor2;
            }
            if (CurrentThemeIndex == 2)
            {
                Notifications.SendNotification("Set Theme Arctic");
                MenuColors.BackgroundColor = new Color32(32, 118, 199, 255);
                MenuColors.BackgroundColor2 = new Color32(0, 0, 0, 255);

                MenuColors.PageButtonColors = new Color32(25, 25, 25, 255);
                MenuColors.ButtonDisabledColor = new Color32(25, 25, 25, 255);
                MenuColors.ButtonEnabledColor = new Color32(32, 118, 199, 255);

                MenuColors.MenuPointerColor = new Color32(32, 118, 199, 255);
                MenuColors.PlatformColor = new Color32(32, 118, 199, 255);
                MenuColors.TransparentRigColor = new Color32(32, 118, 199, 100);

                GunTemplate.PointerColor = new Color32(32, 118, 199, 255);
                GunTemplate.LineColor = new Color32(32, 118, 199, 255);

                MenuColors.ArrayListLineColor = MenuColors.BackgroundColor;
                MenuColors.ArrayListLineColor2 = MenuColors.BackgroundColor2;
            }
            if (CurrentThemeIndex == 3)
            {
                Notifications.SendNotification("Set Theme MonkeModMenu");
                MenuColors.BackgroundColor = new Color32(0, 0, 0, 255);
                MenuColors.BackgroundColor2 = new Color32(0, 0, 0, 255);

                MenuColors.PageButtonColors = Color.grey;
                MenuColors.ButtonDisabledColor = Color.red;
                MenuColors.ButtonEnabledColor = Color.green;

                MenuColors.MenuPointerColor = Color.white;
                MenuColors.PlatformColor = Color.black;
                MenuColors.TransparentRigColor = new Color32(255, 255, 255, 100);

                GunTemplate.PointerColor = Color.white;
                GunTemplate.LineColor = Color.white;

                MenuColors.ArrayListLineColor = Color.blue;
                MenuColors.ArrayListLineColor2 = Color.blue;
            }
            MenuBase.RefreshMenu();
        }

        public static void SetFPC(bool state)
        {
            if (state)
            {
                if (GorillaTagger.Instance.thirdPersonCamera && GorillaTagger.Instance.thirdPersonCamera.transform.GetChild(0).gameObject.activeSelf)
                {
                    GorillaTagger.Instance.thirdPersonCamera.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                if (GorillaTagger.Instance.thirdPersonCamera && !GorillaTagger.Instance.thirdPersonCamera.transform.GetChild(0).gameObject.activeSelf)
                {
                    GorillaTagger.Instance.thirdPersonCamera.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }
}
