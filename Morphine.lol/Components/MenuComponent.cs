using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using Morphine.Framework.Elements;
using static Morphine.Framework.MenuBase;
using Morphine.Framework;
using Morphine.Features;
using Photon.Pun;
using Morphine.Patches;
using GorillaNetworking;
using Morphine.Framework.Helpers;

namespace Morphine.Components
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player), "LateUpdate", MethodType.Normal)]
    public class MenuComponent : MonoBehaviour
    {
        public static List<ButtonInfo> Buttons = new List<ButtonInfo>
        {
            // Base
            new ButtonInfo("Settings", Category.Base, false, false, ()=>ChangePage(Category.Settings)),
            new ButtonInfo("Room", Category.Base, false, false, ()=>ChangePage(Category.Room)),
            new ButtonInfo("Movement", Category.Base, false, false, ()=>ChangePage(Category.Movement)),
            new ButtonInfo("Player", Category.Base, false, false, ()=>ChangePage(Category.Player)),
            new ButtonInfo("Advantage", Category.Base, false, false, ()=>ChangePage(Category.Advantage)),
            new ButtonInfo("Visuals", Category.Base, false, false, ()=>ChangePage(Category.Visuals)),
            new ButtonInfo("Safety", Category.Base, false, false, ()=>ChangePage(Category.Safety)),
            new ButtonInfo("Water Exploits", Category.Base, false, false, ()=>ChangePage(Category.WaterExploits)),
            new ButtonInfo("Rope Exploits", Category.Base, false, false, ()=>ChangePage(Category.RopeExploits)),
            new ButtonInfo("Misc Exploits", Category.Base, false, false, ()=>ChangePage(Category.MiscExploits)),

            // Settings
            new ButtonInfo("Change Menu Theme", Category.Settings, false, false, ()=>Settings.SwitchTheme()),
            new ButtonInfo("Sky Changer", Category.Settings, true, true, delegate {Settings.AllowSkyBoxChanger = true; }, delegate { Settings.AllowSkyBoxChanger = false; }),
            new ButtonInfo("Player Gun Lock", Category.Settings, true, true, delegate { Settings.PlayerGunLock = true; }, delegate { Settings.PlayerGunLock = false; }),
            new ButtonInfo("Show Self Cham", Category.Settings, true, true, delegate { Settings.ShowCham = true; }, delegate { Settings.ShowCham = false; }),
            new ButtonInfo("Always Show Self Cham", Category.Settings, true, false, delegate { Settings.AlwaysShowCham = true; }, delegate { Settings.AlwaysShowCham = false; }),

            // Room
            new ButtonInfo("Disconnect", Category.Room, false, false, ()=>PhotonNetwork.Disconnect()),
            new ButtonInfo("Quit Application", Category.Room, false, false, ()=>Application.Quit()),

            // Movement
            new ButtonInfo("Platforms", Category.Movement, true, false, ()=>LocalPlayer.Platforms(), null, "Lets you walk on Air"),
            new ButtonInfo("Sticky Platforms", Category.Movement, true, false, ()=>LocalPlayer.StickyPlatforms(), null, "Lets you walk on Air"),
            new ButtonInfo("Transform Flight", Category.Movement, true, false, ()=>LocalPlayer.TransformFlight(), null, "Press A to Fly"),
            new ButtonInfo("Noclip", Category.Movement, true, false, ()=>LocalPlayer.Noclip(), null, "Right Trigger to Noclip Through Any Object"),
            new ButtonInfo("Car Monke", Category.Movement, true, false, ()=>LocalPlayer.CarMonke(), null, "Right/Left Trigger to Driver Around Like a Car"),
            new ButtonInfo("Speed Boost", Category.Movement, true, false, ()=>LocalPlayer.ToggleSpeed(2.0f, 100.0f, true), null, "Go really fast!"),
            new ButtonInfo("Teleport Gun", Category.Movement, true, false, ()=>LocalPlayer.TeleportGun(), null, "Teleport to anywhere you desire"),
            new ButtonInfo("Up & Down", Category.Movement, true, false, ()=>LocalPlayer.UpDown(), null, "Right/Left Trigger to go Up & Down"),
            new ButtonInfo("Spider Monke", Category.Movement, true, false, ()=>SpiderMonke.ExecuteSpiderMonke(), null, "Become spider man by using Left/Right Grips"),
            new ButtonInfo("Iron Monke", Category.Movement, true, false, ()=>LocalPlayer.IronMonke(), null, "Fly around like Iron man by Press your Grips"),
            new ButtonInfo("Checkpoint", Category.Movement, true, false, ()=>LocalPlayer.Checkpoint(), ()=>GameObject.Destroy(LocalPlayer.checkpoint), "Place a checkpoint and teleport to it"),
            new ButtonInfo("Blackhole", Category.Movement, true, false, ()=>LocalPlayer.Blackhole(), ()=>GameObject.Destroy(LocalPlayer.blackhole), "Place a blackhole and get gravitated towards it"),
            new ButtonInfo("No Gravity", Category.Movement, true, false, delegate
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.useGravity = false;
            }, delegate {GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.useGravity = true; }, "Zero Gravity float around like your in space"),

            // Player
            new ButtonInfo("Ghost Monke", Category.Player, true, false, ()=>LocalPlayer.GhostMonke(), null, "Move around outside of your Gorilla"),
            new ButtonInfo("Invisible Monke", Category.Player, true, false, ()=>LocalPlayer.InvisibleMonke(), null, "Become invisible"),
            new ButtonInfo("RGB (STUMP)", Category.Player, true, false, ()=>Overpowered.RGB(), null, "Makes you change colors"),
            new ButtonInfo("Strobe (STUMP)", Category.Player, true, false, ()=>Overpowered.Strobe(), null, "Makes you change colors fast"),
            new ButtonInfo("Helicopter", Category.Player, true, false, ()=>Fun.Helicopter(), ()=>Fun.ResetRig(), "Spin around like a helicopter"),
            new ButtonInfo("T-Pose", Category.Player, true, false, ()=>Fun.TPose(), ()=>Fun.ResetRig()),
            new ButtonInfo("Namo Pose", Category.Player, true, false, ()=>Fun.FakeNameTroll(), ()=>Fun.ResetRig(), "Pose like Namo/Name"),
            new ButtonInfo("Spinbot", Category.Player, true, false, ()=>Fun.Spinbot(), ()=>Fun.ResetRig(), "Spin around"),
            new ButtonInfo("Spaz (G)", Category.Player, true, false, ()=>Fun.Spaz(), ()=>Fun.ResetRig(), "Start Tweaking"),
            new ButtonInfo("Head Spin X", Category.Player, true, false, ()=>Fun.HeadSpin("x", 20f), ()=>Fun.ResetHead()),
            new ButtonInfo("Head Spin Y", Category.Player, true, false, ()=>Fun.HeadSpin("y", 20f), ()=>Fun.ResetHead()),
            new ButtonInfo("Head Spin Z", Category.Player, true, false, ()=>Fun.HeadSpin("z", 20f), ()=>Fun.ResetHead()),
            new ButtonInfo("Spaz Hands", Category.Player, true, false, ()=>Fun.HandSpaz(15f), ()=>Fun.ResetHands()),
            new ButtonInfo("Bees", Category.Player, true, false, ()=>Fun.Bees(), ()=>Fun.ResetRig(), "Makes you teleport around to people like Bees"),
            new ButtonInfo("Rape Gun", Category.Player, true, false, ()=>Fun.RapeGun(), null, "Rape the fuck out of somebody"),
            new ButtonInfo("Chase Player Gun", Category.Player, true, false, ()=>Fun.ChaseGun(), null, "Chase anyone you want."),
            
            // Advantage
            new ButtonInfo("Slingshot Silent Aim", Category.Advantage, true, false, ()=>Advantage.SlingshotSilentAim(), null, "always hit somebody in battle"),
            new ButtonInfo("Wall Walk", Category.Advantage, true, false, ()=>LocalPlayer.WallWalk(), null, "Grip to walk on walls"),
            new ButtonInfo("Comp Boost", Category.Advantage, true, false, ()=>LocalPlayer.ToggleSpeed(1.05f, 7.6f, true)),
            new ButtonInfo("Long Arms", Category.Advantage, true, false, delegate
            {
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }, delegate { GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f); }),
            new ButtonInfo("No Slip", Category.Advantage, true, false, delegate { GetSlidePercentage.AllowGrip = true; }, delegate {GetSlidePercentage.AllowGrip = false; }),
            new ButtonInfo("Anti-Tag", Category.Advantage, true, false, ()=>Overpowered.AntiTag(), null, "Never get tagged"),
            new ButtonInfo("Flick Tag Aura", Category.Advantage, true, false, ()=>Overpowered.FlickTagAura(), null, "Grip to flick tag people near you"),
            

            // Visuals
            new ButtonInfo("Box 3D", Category.Visuals, true, false, ()=>Visual.DrawBox3D()),
            new ButtonInfo("Box 2D", Category.Visuals, true, false, ()=>Visual.DrawBox2D(), ()=>Visual.StopBox2D()),
            new ButtonInfo("Chams", Category.Visuals, true, false, ()=>Visual.StartChams(), ()=>Visual.StopChams()),
            new ButtonInfo("Skeletons", Category.Visuals, true, false, ()=>Visual.Skeleton(), ()=>Visual.StopSkeleton()),
            new ButtonInfo("Names", Category.Visuals, true, false, ()=>Visual.Names()),
            new ButtonInfo("Distance", Category.Visuals, true, false, ()=>Visual.Distance()),
            new ButtonInfo("Tracers", Category.Visuals, true, false, ()=>Visual.Tracers()),
            new ButtonInfo("Low Graphics", Category.Visuals, true, false, delegate { Texture.globalMipmapLimit = 2; }, delegate { Texture.globalMipmapLimit = 0; }),
            new ButtonInfo("First Person Camera", Category.Visuals, true, false, ()=>Settings.SetFPC(true), ()=>Settings.SetFPC(false)),

            // Safety
            new ButtonInfo("Anti Report", Category.Safety, true, true, ()=>Safety.AntiReport(), null, "Disconnects you before anybody will be able to report you"),
            new ButtonInfo("Anti Moderator", Category.Safety, true, false, ()=>Safety.AntiModerator(), null, "Disconnects you after a moderator joins your current room"),
            new ButtonInfo("Anti Kick (RPC,USE)", Category.Safety, true, true, ()=>Safety.AntiRatelimit(), null, "Helps you not get RPC kicked"),            

            // Water Exploits
            new ButtonInfo("Water Gun", Category.WaterExploits, true, false, ()=>Overpowered.SplashGun()),
            new ButtonInfo("Water Self", Category.WaterExploits, true, false, ()=>Overpowered.SplashSelf()),
            new ButtonInfo("Water Barrage", Category.WaterExploits, true, false, ()=>Overpowered.SplashBarrage()),
            new ButtonInfo("Water Bender (G)", Category.WaterExploits, true, false, ()=>Overpowered.WaterBending()),
            new ButtonInfo("Bust (A)", Category.WaterExploits, true, false, ()=>Overpowered.WaterCum()),
            new ButtonInfo("Water Iron Monke", Category.WaterExploits, true, false, ()=>Overpowered.WaterIronMonke()),

            // Rope Exploits
            new ButtonInfo("Rope Spaz", Category.RopeExploits, true, false, ()=>Overpowered.RopeSpaz()),
            new ButtonInfo("Ropes to Self", Category.RopeExploits, true, false, ()=>Overpowered.RopesToSelf()),
            new ButtonInfo("Rope Gun", Category.RopeExploits, true, false, ()=>Overpowered.RopesGun()),
            new ButtonInfo("Ropes to Player Gun", Category.RopeExploits, true, false, ()=>Overpowered.RopesToPlayerGun()),
            new ButtonInfo("Ropes Up", Category.RopeExploits, true, false, ()=>Overpowered.RopesUp()),
            new ButtonInfo("Ropes Down", Category.RopeExploits, true, false, ()=>Overpowered.RopesDown()),

            // Sound Exploits
            new ButtonInfo("Play Hand Tap (G)", Category.SoundExploits, true, false, ()=>Overpowered.HandTapSpam()),
            new ButtonInfo("Play Wood Tap (G)", Category.SoundExploits, true, false, ()=>Overpowered.WoodTapSpam()),
            new ButtonInfo("Play Duck Sound (G)", Category.SoundExploits, true, false, ()=>Overpowered.DuckSpam()),
            new ButtonInfo("Crystal Spam (G)", Category.SoundExploits, true, false, ()=>Overpowered.RandomSpam1()),
            new ButtonInfo("Glass Spam (G)", Category.SoundExploits, true, false, ()=>Overpowered.RandomSpam2()),
            new ButtonInfo("Button Spam (G)", Category.SoundExploits, true, false, ()=>Overpowered.RandomSpam3()),

            // Misc Exploits
            new ButtonInfo("Tag Gun", Category.MiscExploits, true, false, ()=>Overpowered.TagGun(), ()=>Fun.ResetRig(), "Tag anybody, from any distance"),
            new ButtonInfo("Tag All", Category.MiscExploits, true, false, ()=>Overpowered.TagAll(), ()=>Fun.ResetRig(), "Tag everyone!"),
            new ButtonInfo("Tag Aura", Category.MiscExploits, true, false, ()=>Overpowered.TagAura(), null, "Tags the closest player."),
            new ButtonInfo("Tag Self", Category.MiscExploits, true, false, ()=>Overpowered.TagSelf(), null),
            new ButtonInfo("Force Party", Category.MiscExploits, false, false, ()=>Overpowered.ForceParty(), null),
        };

        private static void Prefix(GorillaLocomotion.Player __instance)
        {
            try
            {
                if (ControllerInputPoller.instance.leftControllerSecondaryButton && MenuObj == null)
                {
                    DrawMenu();
                    if (reference == null)
                    {
                        Material mat = new Material(Shader.Find("GUI/Text Shader"));
                        mat.color = MenuColors.MenuPointerColor;
                        reference = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        reference.transform.parent = __instance.rightControllerTransform;
                        reference.GetComponent<Renderer>().material = mat;
                        reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                        reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                        buttonCollider = reference.GetComponent<BoxCollider>();
                    }
                }
                else if (!ControllerInputPoller.instance.leftControllerSecondaryButton && MenuObj != null)
                {
                    GameObject.Destroy(reference);
                    reference = null;
                    GameObject.Destroy(MenuObj);
                    MenuObj = null;
                }
                if (ControllerInputPoller.instance.leftControllerSecondaryButton && MenuObj != null)
                {
                    MenuObj.transform.position = __instance.leftControllerTransform.transform.position;
                    MenuObj.transform.rotation = __instance.leftControllerTransform.transform.rotation;
                }
                if (btnCooldown > 0)
                {
                    if (Time.frameCount > btnCooldown)
                    {
                        btnCooldown = 0;
                        GameObject.Destroy(MenuObj);
                        MenuObj = null;
                        DrawMenu();
                    }
                }

                foreach (ButtonInfo btn in Buttons)
                {
                    if (btn.Enabled)
                    {
                        if (btn.onEnable != null)
                        {
                            btn.onEnable();
                        }
                    }
                }
            }
            catch { }
        }
    }
}
