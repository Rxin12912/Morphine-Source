using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Morphine.Patches
{
    [HarmonyPatch(typeof(GameObject), "CreatePrimitive")]
    public class CreatePrimitive
    {
        public static void Postfix(GameObject __result)
        {
            __result.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
        }
    }
}
