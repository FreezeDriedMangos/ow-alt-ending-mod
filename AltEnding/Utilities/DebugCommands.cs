using AltEnding.Utilities.Props;
using NewHorizons.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AltEnding.Utilities
{
    public static class DebugCommands
    {
        public static void Update()
        {

            // Have to press tilde while entering debug commands
            if (!Keyboard.current[Key.Backquote].isPressed) return;

            var spawner = GameObject.Find("Player_Body")?.GetComponent<PlayerSpawner>();
            var inEnding = AltEnding.NewHorizonsAPI.GetCurrentStarSystem() == "clay.AltEnding";

            bool numpad = false; //AltEnding.Instance.ModHelper.Config.GetSettingsValue<bool>("numpad");
            var Numpad0 = numpad ? Key.Numpad0 : Key.Digit0;
            var Numpad1 = numpad ? Key.Numpad1 : Key.Digit1;
            var Numpad2 = numpad ? Key.Numpad2 : Key.Digit2;
            var Numpad3 = numpad ? Key.Numpad3 : Key.Digit3;
            var Numpad4 = numpad ? Key.Numpad4 : Key.Digit4;
            var Numpad5 = numpad ? Key.Numpad5 : Key.Digit5;
            var Numpad6 = numpad ? Key.Numpad5 : Key.Digit5;
            var Numpad7 = numpad ? Key.Numpad7 : Key.Digit7;


            // Go to the end of the time loop
            if (Keyboard.current[Numpad0].wasReleasedThisFrame)
            {
                TimeLoop.SetSecondsRemaining(30);
            }

            // Teleport to QM aspects in the "in ending" section
            if (inEnding)
            {
                if (spawner != null)
                {
                    if (Keyboard.current[Numpad1].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Eye Aspect"));
                    }
                    if (Keyboard.current[Numpad2].wasReleasedThisFrame)
                    {
                        spawner.DebugWarp(InEndingPropsController.stationSpawnPoint);
                    }
                    if (Keyboard.current[Numpad3].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Brittle Hollow Aspect"));
                    }
                    if (Keyboard.current[Numpad4].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Dark Bramble Aspect"));
                    }
                    if (Keyboard.current[Numpad5].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Giant's Deep Aspect"));
                    }
                    if (Keyboard.current[Numpad6].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Hourglass Twins Aspect"));
                    }
                    if (Keyboard.current[Numpad7].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Hourglass Twins Aspect Interior"), 70);
                    }
                }
            }
            else
            {
                if (Keyboard.current[Numpad1].wasReleasedThisFrame)
                {
                    if (!inEnding)
                    {
                        AltEnding.NewHorizonsAPI.ChangeCurrentStarSystem("clay.AltEnding");
                    }
                }
            }
        }

        private static void WarpToPlanet(AstroObject planet, float offset = 100f)
        {
            var player = Locator.GetPlayerBody();
            var newWorldPos = planet.transform.position + Vector3.up * offset;

            player.WarpToPositionRotation(newWorldPos, Quaternion.identity);
            player.SetVelocity(planet.GetAttachedOWRigidbody().GetPointVelocity(newWorldPos));
        }
    }
}
