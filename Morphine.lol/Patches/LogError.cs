using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Morphine.Patches
{
    [HarmonyPatch(typeof(GorillaNot), "LogErrorCount")]
    public class LogError : MonoBehaviour
    {
        public static bool Prefix(string logString, string stackTrace, LogType type)
        {
            return false;
        }
    }
}
