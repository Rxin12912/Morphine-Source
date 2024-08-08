using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Morphine.Framework.Helpers;
using GorillaTag;
using Morphine.Utility;

namespace Morphine.Features
{
    // Give credits atleast if your gonna steal my visuals because some of them dont use shitty line renderers
    public class Visual
    {
        public static int[] bones = new int[]
        {
            4,
            3,
            5,
            4,
            19,
            18,
            20,
            19,
            3,
            18,
            21,
            20,
            22,
            21,
            25,
            21,
            29,
            21,
            31,
            29,
            27,
            25,
            24,
            22,
            6,
            5,
            7,
            6,
            10,
            6,
            14,
            6,
            16,
            14,
            12,
            10,
            9,
            7
        };

        public static bool IsPlayerInfected(VRRig player)
        {
            if (player.mainSkin.material.name.Contains("fected"))
                return true;
            return false;
        }

        public static void StartChams()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig)
                    continue;

                Material material = new Material(Shader.Find("GUI/Text Shader"));
                if (PlayerUtil.IsPlayerInfected(vrrig))
                    material.color = Color.red;
                else
                    material.color = vrrig.mainSkin.material.color;
                vrrig.mainSkin.material = material;
            }
        }

        public static void StopChams()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig)
                    continue;
                vrrig.ChangeMaterialLocal(vrrig.currentMatIndex);
            }
        }

        public static void DrawBox3D()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;

                Color color;
                if (IsPlayerInfected(vrrig))
                    color = Color.red;
                else
                    color = vrrig.mainSkin.material.color;
                Drawing.DrawBox(vrrig.transform.position, Quaternion.identity, new Vector3(.5f, .5f, .5f), color);
            }
        }

        public static void DrawBox2D()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                Color color;
                if (IsPlayerInfected(Player))
                    color = Color.red;
                else
                    color = Player.mainSkin.material.color;

                if (Player.gameObject.GetComponent<LineRenderer>() == null)
                {
                    Player.gameObject.AddComponent<LineRenderer>();
                }
                LineRenderer component = Player.gameObject.GetComponent<LineRenderer>();
                component.startWidth = 0.04f;
                component.endWidth = 0.04f;
                component.material.shader = Shader.Find("GUI/Text Shader");
                component.positionCount = 4;
                component.loop = true;
                component.useWorldSpace = true;
                component.forceRenderingOff = false;
                component.startColor = color;
                component.endColor = color;
                Vector3 position = Player.transform.position;
                Vector3 vector = Vector3.Cross((GorillaTagger.Instance.headCollider.transform.position - position).normalized, Vector3.up).normalized * 0.43f;
                Vector3 vector2 = Vector3.up * 0.50f;
                Vector3 vector3 = position - vector - vector2;
                Vector3 vector4 = position + vector - vector2;
                Vector3 vector5 = position + vector + vector2;
                Vector3 vector6 = position - vector + vector2;
                component.SetPosition(0, vector3);
                component.SetPosition(1, vector4);
                component.SetPosition(2, vector5);
                component.SetPosition(3, vector6);
            }
        }

        public static void StopBox2D()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                if (Player.gameObject.GetComponent<LineRenderer>() != null)
                {
                    GameObject.Destroy(Player.gameObject.GetComponent<LineRenderer>());
                }
            }
        }

        public static void Skeleton()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                if (Player == GorillaTagger.Instance.offlineVRRig) continue;

                Color color;
                if (IsPlayerInfected(Player))
                    color = Color.red;
                else
                    color = Player.mainSkin.material.color;

                Material material = new Material(Shader.Find("GUI/Text Shader"));
                material.color = color;
                if (!Player.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                {
                    Player.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                }
                Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().material = material;
                Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                Player.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                for (int b = 0; b < Enumerable.Count<int>(bones); b += 2)
                {
                    if (!Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>())
                    {
                        Player.mainSkin.bones[bones[b]].gameObject.AddComponent<LineRenderer>();
                    }
                    Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().endWidth = 0.025f;
                    Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().startWidth = 0.025f;
                    Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().material = material;
                    Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(0, Player.mainSkin.bones[bones[b]].position);
                    Player.mainSkin.bones[bones[b]].gameObject.GetComponent<LineRenderer>().SetPosition(1, Player.mainSkin.bones[bones[b + 1]].position);
                }
            }
        }

        public static void StopSkeleton()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                for (int j = 0; j < Enumerable.Count<int>(bones); j += 2)
                {
                    if (Player.mainSkin.bones[bones[j]].gameObject.GetComponent<LineRenderer>())
                    {
                        GameObject.Destroy(Player.mainSkin.bones[bones[j]].gameObject.GetComponent<LineRenderer>());
                    }
                    if (Player.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                    {
                        GameObject.Destroy(Player.head.rigTarget.gameObject.GetComponent<LineRenderer>());
                    }
                }
            }
        }

        public static void Names()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                if (Player == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject name = new GameObject($"{Player.playerText.text}'s Nametag");
                TextMesh textMesh = name.AddComponent<TextMesh>();
                textMesh.fontSize = 20;
                textMesh.fontStyle = FontStyle.Normal;
                textMesh.characterSize = 0.1f;
                textMesh.anchor = TextAnchor.MiddleCenter;
                textMesh.alignment = TextAlignment.Center;
                textMesh.color = Color.white;
                textMesh.text = Player.playerText.text;
                float textWidth = textMesh.GetComponent<Renderer>().bounds.size.x;
                name.transform.position = Player.headMesh.transform.position + new Vector3(0f, .90f, 0f);
                name.transform.LookAt(Camera.main.transform.position);
                name.transform.Rotate(0, 180, 0);
                name.GetComponent<TextMesh>().text = Player.playerText.text;
                GameObject.Destroy(name, Time.deltaTime);
            }
        }

        public static void Distance()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                if (Player == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject name = new GameObject($"{Player.playerText.text}'s Distance");
                TextMesh textMesh = name.AddComponent<TextMesh>();
                textMesh.fontSize = 20;
                textMesh.fontStyle = FontStyle.Normal;
                textMesh.characterSize = 0.1f;
                textMesh.anchor = TextAnchor.MiddleCenter;
                textMesh.alignment = TextAlignment.Center;
                textMesh.color = Color.white;
                textMesh.text = Player.playerText.text;
                float textWidth = textMesh.GetComponent<Renderer>().bounds.size.x;
                name.transform.position = Player.headMesh.transform.position + new Vector3(0f, .65f, 0f);
                name.transform.LookAt(Camera.main.transform.position);
                name.transform.Rotate(0, 180, 0);
                name.GetComponent<TextMesh>().text = $"{Convert.ToInt32(Vector3.Distance(GorillaLocomotion.Player.Instance.headCollider.transform.position, Player.transform.position))}m";
                GameObject.Destroy(name, Time.deltaTime);
            }
        }

        public static void Tracers()
        {
            foreach (VRRig Player in GorillaParent.instance.vrrigs)
            {
                if (Player == GorillaTagger.Instance.offlineVRRig) continue;
                Color color;
                if (IsPlayerInfected(Player))
                    color = Color.red;
                else
                    color = Player.mainSkin.material.color;
                Drawing.DrawLine(GorillaLocomotion.Player.Instance.bodyCollider.transform.position - new Vector3(0f, .4f, 0f), Player.transform.position - new Vector3(0f, .33f, 0f), color);
            }
        }
    }
}
