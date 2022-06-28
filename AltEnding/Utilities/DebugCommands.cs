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

            // Go to the end of the time loop
            if (Keyboard.current[Key.Numpad0].wasReleasedThisFrame)
            {
                TimeLoop.SetSecondsRemaining(30);
            }

            // Teleport to QM aspects in the "in ending" section
            if (inEnding)
            {
                if (spawner != null)
                {
                    if (Keyboard.current[Key.Numpad1].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Eye Aspect"));
                    }
                    if (Keyboard.current[Key.Numpad2].wasReleasedThisFrame)
                    {
                        spawner.DebugWarp(InEndingPropsController.stationSpawnPoint);
                    }
                    if (Keyboard.current[Key.Numpad3].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Brittle Hollow Aspect"));
                    }
                    if (Keyboard.current[Key.Numpad4].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Dark Bramble Aspect"));
                    }
                    if (Keyboard.current[Key.Numpad5].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Giant's Deep Aspect"));
                    }
                    if (Keyboard.current[Key.Numpad6].wasReleasedThisFrame)
                    {
                        WarpToPlanet(AstroObjectLocator.GetAstroObject("QM Hourglass Twins Aspect"));
                    }
                }
            }
            else
            {
                if (Keyboard.current[Key.Numpad1].wasReleasedThisFrame)
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
