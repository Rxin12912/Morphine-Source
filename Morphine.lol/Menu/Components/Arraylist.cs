using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BepInEx;
using Morphine.Components;

namespace Morphine.Framework.Components
{
    [BepInPlugin("com.rxin.morphinearraylist", "Morphine Arraylist", "2.0.0")]
    public class Arraylist : BaseUnityPlugin
    {
        private GUIStyle titleStyle;
        private GUIStyle textStyle;
        private GUIStyle rectStyle;
        private GUIStyle greyBackgroundStyle;
        private Vector2 windowPosition;
        private System.Random random = new System.Random();

        private Color color;

        private float extraPadding = 15f;
        private static float playtime;

        public static Color32[] Colors = new Color32[]
        {
            new Color32(235, 52, 122, 255),
            new Color32(128, 52, 235, 255),
            new Color32(223, 3, 252, 255),
            new Color32(65, 3, 252, 255)
        };

        private void Awake() { }

        private void OnGUI()
        {
            GUI.skin = null;

            InitializeStyles();

            string[] sortedButtons = MenuComponent.Buttons
                .Where((button, index) => MenuComponent.Buttons[index].Enabled)
                .Select(s => s.buttonText.ToUpper())
                .OrderByDescending(s => s.Length)
                .ThenByDescending(s => s.Sum(c => char.IsLetter(c) ? 0 : 1))
                .ToArray();

            Dictionary<string, float> textWidthDict = new Dictionary<string, float>();

            foreach (var button in sortedButtons)
            {
                float textWidth = textStyle.CalcSize(new GUIContent(button)).x;
                float textHeight = textStyle.CalcSize(new GUIContent(button)).y;
                textWidthDict[button] = textWidth;
            }

            sortedButtons = sortedButtons.OrderByDescending(b => textWidthDict[b]).ToArray();

            float rectHeight = sortedButtons.Length * 20f;
            GUI.Box(new Rect(Screen.width - 5, 0, 5, rectHeight), "", rectStyle);

            for (int i = 0; i < sortedButtons.Length; i++)
            {
                float buttonHeight = 20f;
                float buttonY = 0 + i * buttonHeight;
                float textWidth = textWidthDict[sortedButtons[i]];
                rectStyle.normal.background = MakeTex(2, 2, Color32.Lerp(Colors[random.Next(Colors.Length)], Colors[random.Next(Colors.Length)], Mathf.PingPong(Time.time, 1f)));

                //GUI.Box(new Rect(Screen.width - 7 - textWidth - extraPadding, buttonY, textWidth + 8 * 2, buttonHeight), "", greyBackgroundStyle);
                GUI.Label(new Rect(Screen.width - extraPadding - textWidth, buttonY, textWidth, buttonHeight), sortedButtons[i], textStyle);
            }
        }

        private void DrawRoundedUIText(string content, int textSize, bool isBold, Color textColor, float posX, float posY)
        {
            GUIStyle roundedTextStyle = new GUIStyle(GUI.skin.box);
            roundedTextStyle.normal.background = MakeTex(2, 2, new Color(0.6f, 0.2f, 0.8f, 0.8f));
            roundedTextStyle.fontSize = textSize;
            roundedTextStyle.fontStyle = isBold ? FontStyle.Bold : FontStyle.Normal;
            roundedTextStyle.normal.textColor = textColor;

            float textWidth = roundedTextStyle.CalcSize(new GUIContent(content)).x;
            float textHeight = roundedTextStyle.CalcSize(new GUIContent(content)).y;

            float extraPadding = 5f;

            GUI.Box(new Rect(posX - textWidth / 2 - extraPadding, posY - textHeight / 2, textWidth + extraPadding * 2, textHeight), "", roundedTextStyle);
            GUI.Label(new Rect(posX - textWidth / 2, posY - textHeight / 2, textWidth, textHeight), content, roundedTextStyle);
        }


        private void InitializeStyles()
        {
            titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.fontSize = 45;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.normal.textColor = Color.white;

            textStyle = new GUIStyle(GUI.skin.label);
            textStyle.fontSize = 20;
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.normal.textColor = Color32.Lerp(MenuColors.ArrayListLineColor, MenuColors.ArrayListLineColor2, Mathf.PingPong(Time.time, 1f));
            textStyle.alignment = TextAnchor.MiddleRight;

            rectStyle = new GUIStyle(GUI.skin.box);
            rectStyle.normal.background = MakeTex(2, 2, Color32.Lerp(MenuColors.ArrayListLineColor, MenuColors.ArrayListLineColor2, Mathf.PingPong(Time.time, 1f)));

            greyBackgroundStyle = new GUIStyle(GUI.skin.box);
            greyBackgroundStyle.normal.background = MakeTex(2, 2, new Color(0.2f, 0.2f, 0.2f, 0.5f));

            windowPosition = new Vector2(Screen.width / 2 - 110, Screen.height / 2 - 55);
        }

        private Texture2D MakeTex(int width, int height, Color color)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = color;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}
