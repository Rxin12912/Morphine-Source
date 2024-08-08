using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Morphine.Framework.Elements;
using Morphine.Framework.Helpers;
using Morphine.Framework.Components;
using Morphine.Components;

namespace Morphine.Framework
{
    public class MenuBase
    {
        public static GameObject MenuObj;
        public static GameObject buttonObj;
        public static GameObject canvasObj = null;
        public static GameObject reference = null;
        public static BoxCollider buttonCollider;
        public static int framePressCooldown = 0;
        public static int btnCooldown;
        public static Category currentPage = Category.Base;
        public static int currentCategoryPage = 0;
        public static int pageButtons = 7;
        public static Font Verdana = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        public enum Category
        {
            Base,
            Room,
            Movement,
            Player,
            Fun,
            Exploits,
            Visuals,
            Advantage,
            Settings,
            Safety,
            AtticExploits,
            WaterExploits,
            RopeExploits,
            SoundExploits,
            GliderExploits,
            ProjectileExploits,
            MonsterExploits,
            MiscExploits,
        }
        public static void RefreshMenu()
        {
            GameObject.Destroy(MenuObj);
            MenuObj = null;
            DrawMenu();
        }

        public static void ChangePage(Category page)
        {
            currentCategoryPage = 0;
            currentPage = page;
            RefreshMenu();
        }

        public static List<ButtonInfo> GetButtonInfoByPage(Category page)
        {
            return MenuComponent.Buttons.Where(button => button.Page == page).ToList();
        }

        static void AddHomeButton()
        {
            GameObject newBtn = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(newBtn.GetComponent<Rigidbody>());
            newBtn.GetComponent<BoxCollider>().isTrigger = true;
            newBtn.transform.parent = MenuObj.transform;
            newBtn.transform.rotation = Quaternion.identity;
            newBtn.transform.localScale = new Vector3(0.09f, 0.82f, 0.08f);
            newBtn.transform.localPosition = new Vector3(0.56f, 0f, -0.49f);
            newBtn.AddComponent<ButtonCollider>().btn = new ButtonInfo("Home", Category.Base, false, false, null, null);

            newBtn.GetComponent<Renderer>().material.color = MenuColors.PageButtonColors;

            GameObject titleObj = new GameObject();
            titleObj.transform.parent = canvasObj.transform;
            titleObj.transform.localPosition = new Vector3(0.85f, 0.85f, 0.85f);
            Text title = titleObj.AddComponent<Text>();
            title.font = Verdana;
            title.text = "Return Home";
            title.color = Color.white;
            title.fontSize = 1;
            title.alignment = TextAnchor.MiddleCenter;
            title.fontStyle = FontStyle.Bold;
            title.resizeTextForBestFit = true;
            title.resizeTextMinSize = 0;
            RectTransform titleTransform = title.GetComponent<RectTransform>();
            titleTransform.localPosition = Vector3.zero;
            titleTransform.sizeDelta = new Vector2(0.2f, 0.03f);
            titleTransform.localPosition = new Vector3(.064f, 0f, -0.193f);
            titleTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void DrawPageButtons()
        {
            float num4 = 0f;
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = MenuObj.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.39f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.58f, .20f, 0.24f - num4);
            gameObject.AddComponent<ButtonCollider>().btn = new ButtonInfo("<", Category.Base, false, false, null, null);
            gameObject.GetComponent<Renderer>().material.color = MenuColors.PageButtonColors;
            GameObject gameObject2 = new GameObject();
            gameObject2.transform.parent = canvasObj.transform;
            Text text = gameObject2.AddComponent<Text>();
            text.font = Verdana;
            text.text = "<";
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            text.fontStyle = FontStyle.Bold;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, .06f, 0.098f - num4 / 2.55f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject3.GetComponent<Rigidbody>());
            gameObject3.GetComponent<BoxCollider>().isTrigger = true;
            gameObject3.transform.parent = MenuObj.transform;
            gameObject3.transform.rotation = Quaternion.identity;
            gameObject3.transform.localScale = new Vector3(0.09f, 0.39f, 0.08f);
            gameObject3.transform.localPosition = new Vector3(0.58f, -.20f, 0.24f - num4);
            gameObject3.AddComponent<ButtonCollider>().btn = new ButtonInfo(">", Category.Base, false, false, null, null);
            gameObject3.GetComponent<Renderer>().material.color = MenuColors.PageButtonColors;
            GameObject gameObject4 = new GameObject();
            gameObject4.transform.parent = canvasObj.transform;
            Text text2 = gameObject4.AddComponent<Text>();
            text2.font = Verdana;
            text2.text = ">";
            text2.fontSize = 1;
            text2.alignment = TextAnchor.MiddleCenter;
            text2.resizeTextForBestFit = true;
            text2.fontStyle = FontStyle.Bold;
            text2.resizeTextMinSize = 0;
            RectTransform component2 = text2.GetComponent<RectTransform>();
            component2.localPosition = Vector3.zero;
            component2.sizeDelta = new Vector2(0.2f, 0.03f);
            component2.localPosition = new Vector3(0.064f, -.06f, 0.098f - num4 / 2.55f);
            component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void ToggleButton(Elements.ButtonInfo btn)
        {
            if (btn.NeedsMaster && !PhotonNetwork.IsMasterClient)
            {
                Notifications.SendNotification("You aren't Master.");
                btn.Enabled = false;
                RefreshMenu();
                return;
            }

            if (!btn.isToggle)
            {
                btn.onEnable();

                if (btn.Page == Category.Base)
                    return;
                if (btn.buttonText.Contains("Change Menu Theme"))
                    return;
                Notifications.SendNotification($"Executed {btn.buttonText}");
                return;
            }

            btn.Enabled = !btn.Enabled;
            if (!btn.Enabled)
            {
                if (btn.onDisable != null)
                {
                    btn.onDisable();
                }
            }
            if (btn.Enabled)
            {
                if (btn.Tooltip != null)
                {
                    Notifications.SendNotification($"{btn.Tooltip}", "Tooltip");
                }
            }
            RefreshMenu();
        }

        public static void Toggle(Elements.ButtonInfo button)
        {
            int totalPages = (MenuComponent.Buttons.Count + pageButtons - 1) / pageButtons;
            int totalCategoryPages = (GetButtonInfoByPage(currentPage).Count + pageButtons - 1) / pageButtons;

            switch (button.buttonText)
            {
                case ">":
                    {
                        if (totalCategoryPages < currentCategoryPage || (totalCategoryPages - 1) == currentCategoryPage)
                        {
                            currentCategoryPage = 0;
                        }
                        else
                        {
                            currentCategoryPage++;
                        }
                        RefreshMenu();
                        break;
                    }

                case "<":
                    {
                        if (0 > currentCategoryPage || currentCategoryPage == 0)
                        {
                            currentCategoryPage = totalPages;
                        }
                        else
                        {
                            currentCategoryPage--;
                        }
                        RefreshMenu();
                        break;
                    }

                case "Home":
                    {
                        currentPage = Category.Base;
                        currentCategoryPage = 0;
                        RefreshMenu();
                        return;
                    }

                default:
                    {
                        ToggleButton(button);
                        break;
                    }
            }
        }

        public static void CreateButton(float offset, ButtonInfo button)
        {
            buttonObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(buttonObj.GetComponent<Rigidbody>());
            buttonObj.GetComponent<BoxCollider>().isTrigger = true;
            buttonObj.transform.parent = MenuObj.transform;
            buttonObj.transform.rotation = Quaternion.identity;
            buttonObj.transform.localScale = new Vector3(0.09f, 0.79f, 0.08f);
            buttonObj.transform.localPosition = new Vector3(0.58f, 0f, 0.36f - offset);


            buttonObj.AddComponent<ButtonCollider>().btn = button;
            if (button.Enabled)
            {
                buttonObj.GetComponent<Renderer>().material.color = MenuColors.ButtonEnabledColor;
            }
            else
            {
                buttonObj.GetComponent<Renderer>().material.color = MenuColors.ButtonDisabledColor;
            }


            GameObject gameObject2 = new GameObject();
            gameObject2.transform.parent = canvasObj.transform;
            Text text2 = gameObject2.AddComponent<Text>();
            text2.font = Verdana;
            text2.text = button.buttonText;
            text2.fontSize = 1;
            text2.alignment = TextAnchor.MiddleCenter;
            text2.fontStyle = FontStyle.Bold;
            text2.resizeTextForBestFit = true;
            text2.resizeTextMinSize = 0;
            RectTransform component = text2.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, 0f, 0.140f - offset / 2.55f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void BackMessage()
        {
            GameObject gameObject3 = new GameObject();
            gameObject3.transform.parent = canvasObj.transform;
            gameObject3.name = "BackMessage";
            UnityEngine.UI.Text text2 = gameObject3.AddComponent<UnityEngine.UI.Text>();
            text2.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text2.text = "Build: 2.3.0";
            text2.fontSize = (int)0.06f;
            text2.fontStyle = FontStyle.Bold;
            text2.alignment = TextAnchor.MiddleCenter;
            text2.resizeTextForBestFit = true;
            text2.resizeTextMinSize = 0;
            RectTransform component2 = text2.GetComponent<RectTransform>();
            component2.localPosition = Vector3.zero;
            component2.sizeDelta = new Vector2(0.15f, 0.15f);
            component2.position = new Vector3(0.04f, 0f, 0);
            component2.rotation = Quaternion.Euler(new Vector3(0f, 90f, 90f));
        }

        public static void DrawMenu()
        {
            // Frame Parent
            MenuObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(MenuObj.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(MenuObj.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(MenuObj.GetComponent<Renderer>());
            MenuObj.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f);

            // Main Frame
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.transform.parent = MenuObj.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.1f, .93f, 1f);
            //gameObject.GetComponent<Renderer>().material = MenuShader;
            gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            gameObject.GetComponent<Renderer>().material.color = MenuColors.BackgroundColor;
            gameObject.transform.position = new Vector3(0.05f, 0f, 0f);

            GradientColorKey[] ColorKey = new GradientColorKey[3];
            ColorKey[0].color = MenuColors.BackgroundColor;
            ColorKey[0].time = 0.0f;
            ColorKey[1].color = MenuColors.BackgroundColor2;
            ColorKey[1].time = .5f;
            ColorKey[2].color = MenuColors.BackgroundColor;
            ColorKey[2].time = 1f;

            var ColorChanger = gameObject.AddComponent<ColorChanger>();

            ColorChanger.colors = new Gradient
            {
               colorKeys = ColorKey,
            };
            ColorChanger.Start();

            // Canvas
            canvasObj = new GameObject();
            canvasObj.transform.parent = MenuObj.transform;
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;
            GameObject gameObject2 = new GameObject();
            gameObject2.transform.parent = canvasObj.transform;
            Text text = gameObject2.AddComponent<Text>();
            text.font = Verdana;
            text.text = "<size=14>Morphine.lol</size>";
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Bold;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);
            component.position = new Vector3(0.06f, 0f, 0.155f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (currentPage != Category.Base)
            {
                DrawPageButtons();
                AddHomeButton();
            }
            else
            {
                DrawPageButtons();
            }

            BackMessage();
            var Page = GetButtonInfoByPage(currentPage).Skip(currentCategoryPage * pageButtons).Take(pageButtons).ToArray();
            for (int i = 0; i < Page.Length; i++)
            {
                CreateButton(i * 0.09f + 0.21f, Page[i]);
            }
        }
    }
}
