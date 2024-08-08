using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using BepInEx;

namespace Morphine.Framework.Helpers
{
    [BepInPlugin("com.rxin.notificationss", "Morphine Notifications", "1.0.0")]
    public class Notifications : BaseUnityPlugin
    {
        private static Notifications instance;
        private void Awake() { instance = this; }

        private void Init()
        {
            this.MainCamera = GameObject.Find("Main Camera");
            this.HUDObj = new GameObject();
            this.HUDObj2 = new GameObject();
            this.HUDObj2.name = "NOTIFICATIONLIB_HUD_OBJ";
            this.HUDObj.name = "NOTIFICATIONLIB_HUD_OBJ";
            this.HUDObj.AddComponent<Canvas>();
            this.HUDObj.AddComponent<CanvasScaler>();
            this.HUDObj.AddComponent<GraphicRaycaster>();
            this.HUDObj.GetComponent<Canvas>().enabled = true;
            this.HUDObj.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            this.HUDObj.GetComponent<Canvas>().worldCamera = this.MainCamera.GetComponent<Camera>();
            this.HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
            this.HUDObj.GetComponent<RectTransform>().position = new Vector3(this.MainCamera.transform.position.x + 4.5f, this.MainCamera.transform.position.y - 2.5f, this.MainCamera.transform.position.z - 2.5f);
            this.HUDObj2.transform.position = new Vector3(this.MainCamera.transform.position.x - 1.5f, this.MainCamera.transform.position.y, this.MainCamera.transform.position.z - 4.5f);
            this.HUDObj.transform.parent = this.HUDObj2.transform;
            this.HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
            Vector3 eulerAngles = this.HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
            eulerAngles.y = -270f;
            this.HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
            this.HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles);
            this.Testtext = new GameObject
            {
                transform =
                {
                    parent = this.HUDObj.transform
                }
            }.AddComponent<Text>();
            this.Testtext.text = "";
            this.Testtext.fontSize = 7;
            this.Testtext.font = GameObject.Find("motdtext").GetComponent<Text>().font;
            this.Testtext.rectTransform.sizeDelta = new Vector2(260f, 70f);
            this.Testtext.alignment = TextAnchor.MiddleCenter;
            this.Testtext.rectTransform.localScale = new Vector3(0.01f, 0.01f, 1f);
            this.Testtext.rectTransform.localPosition = new Vector3(-1.10f, -0.9f, -0.16f);
            this.Testtext.material = this.AlertText;
            Notifications.NotifiText = this.Testtext;
        }

        private void FixedUpdate()
        {
            if (!this.HasInit && GameObject.Find("Main Camera") != null)
            {
                this.Init();
                this.HasInit = true;
            }
            this.HUDObj2.transform.position = new Vector3(this.MainCamera.transform.position.x, this.MainCamera.transform.position.y, this.MainCamera.transform.position.z);
            this.HUDObj2.transform.rotation = this.MainCamera.transform.rotation;
            if (this.Testtext.text != "")
            {
                this.NotificationDecayTimeCounter++;
                if (this.NotificationDecayTimeCounter > this.NotificationDecayTime)
                {
                    this.Notifilines = null;
                    this.newtext = "";
                    this.NotificationDecayTimeCounter = 0;
                    this.Notifilines = Enumerable.ToArray<string>(Enumerable.Skip<string>(this.Testtext.text.Split(Environment.NewLine.ToCharArray()), 1));
                    foreach (string text in this.Notifilines)
                    {
                        if (text != "")
                        {
                            this.newtext = this.newtext + text + "\n";
                        }
                    }
                    this.Testtext.text = this.newtext;
                }
            }
            else
            {
                this.NotificationDecayTimeCounter = 0;
            }
        }

        public static void SendNotification(string content, string sender = "MORPHINE")
        {
            if (Notifications.IsEnabled && Notifications.PreviousNotifi != content)
            {
                if (!content.Contains(Environment.NewLine))
                {
                    content += Environment.NewLine;
                }
                Notifications.NotifiText.text = Notifications.NotifiText.text + $"<color=#3492eb>{sender}</color> : {content}";
                Notifications.NotifiText.color = Color.white;
                Notifications.PreviousNotifi = content;
            }
        }

        public static void ClearAllNotifications()
        {
            Notifications.NotifiText.text = "<color=#3492eb>MORPHINE</color>: Cleared Notifications.\n";
        }

        public static void ClearPastNotifications(int amount)
        {
            string text = "";
            foreach (string text2 in Enumerable.ToArray<string>(Enumerable.Skip<string>(Notifications.NotifiText.text.Split(Environment.NewLine.ToCharArray()), amount)))
            {
                if (text2 != "")
                {
                    text = text + text2 + "\n";
                }
            }
            Notifications.NotifiText.text = text;
        }

        private GameObject HUDObj;

        private GameObject HUDObj2;

        private GameObject MainCamera;

        private Text Testtext;

        private Material AlertText = new Material(Shader.Find("GUI/Text Shader"));

        private int NotificationDecayTime = 150;

        private int NotificationDecayTimeCounter;

        public static int NoticationThreshold = 30;

        private string[] Notifilines;

        private string newtext;

        public static string PreviousNotifi;

        private bool HasInit;

        private static Text NotifiText;

        public static bool IsEnabled = true;
    }
}