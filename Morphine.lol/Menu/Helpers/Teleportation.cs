using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Morphine.Menu.Helpers
{
    public class Teleportation
    {
        public static void Teleport(Vector3 Position)
        {
            GorillaLocomotion.Player.Instance.transform.position = Position - GorillaLocomotion.Player.Instance.bodyCollider.transform.position + GorillaLocomotion.Player.Instance.transform.position;
        }
    }
}
