using Morphine.Components;
using Morphine.Framework.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphine.Utility
{
    public class MenuUtil
    {
        public static ButtonInfo GetButton(string name)
        {
            foreach (ButtonInfo button in MenuComponent.Buttons)
            {
                if (button.buttonText == name || button.buttonText.Contains(name))
                {
                    return button;
                }
            }
            return null;
        }
    }
}
