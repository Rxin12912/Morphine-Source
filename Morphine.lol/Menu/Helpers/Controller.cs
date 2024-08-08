using System;

namespace Morphine.Framework.Helpers
{
    public class Controller
    {
        public static bool GetButton(float grabValue)
        {
            return grabValue >= 0.75f;
        }
    }
}
