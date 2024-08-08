using System;
using static Morphine.Framework.MenuBase;

namespace Morphine.Framework.Elements
{
    public class ButtonInfo
    {
        public string buttonText { get; set; }
        public bool isToggle { get; set; }
        public bool NeedsMaster { get; set; }
        public bool Enabled { get; set; }
        public Action onEnable { get; set; }
        public Action onDisable { get; set; }
        public Category Page;

        public string Tooltip { get; set; }
        public ButtonInfo(string lable, Category page, bool isToggle, bool isActive, Action OnClick, Action OnDisable = null, string tooltip = null, bool DoesNeedMaster = false)
        {
            buttonText = lable;
            this.isToggle = isToggle;
            Enabled = isActive;
            onEnable = OnClick;
            Page = page;
            this.onDisable = OnDisable;
            Tooltip = tooltip;
            NeedsMaster = DoesNeedMaster;
        }
    }
}
