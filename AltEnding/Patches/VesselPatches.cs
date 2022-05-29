using AltEnding.Utilities.Props;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltEnding.Patches
{
	
    [HarmonyPatch]
    public static class VesselPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(VesselWarpController), nameof(VesselWarpController.OnSlotActivated))]
	    private static bool VesselWarpController_OnSlotActivated(VesselWarpController __instance, NomaiInterfaceSlot slot)
        {

            if (slot == __instance._warpVesselSlot && __instance._hasPower)
            {
                // Check __instance._coordinateInterface eg
                
                // light location option 1 (on top of the console) Log : Raycast hit "position": {"x": 180.414, "y": 17.72599, "z": -19.12227}, "normal": {"x": -0.08293527, "y": 0.9715306, "z": 0.2219237} on [DB_VesselDimension_Body] at [DB_VesselDimension_Body]
                // option 2 (on cieling) Log : Raycast hit "position": {"x": 167.3631, "y": 27.98209, "z": -15.21499}, "normal": {"x": 0.01615728, "y": -0.9992066, "z": 0.03640392} on [DB_VesselDimension_Body] at [DB_VesselDimension_Body]

                // best option (in cieling spot that makes sense) Log : Raycast hit "position": {"x": 191.4552, "y": 46.56067, "z": -15.27039}, "normal": {"x": 0.08293545, "y": -0.9715306, "z": -0.2219237} on [DB_VesselDimension_Body] at [DB_VesselDimension_Body]

                AltEnding.Instance.ModHelper.Console.WriteLine("CHECKING COORDS");

		        bool flag  = __instance._coordinateInterface._nodeControllers[0].CheckCoordinate(new int[] { 0, 4, 1, 2 }); // { 3, 1, 4, 5 }); //[1, 5, 4]
		        bool flag2 = 
                    __instance._coordinateInterface._nodeControllers[1].CheckCoordinate(new int[] { 2, 5 }) ||
                    __instance._coordinateInterface._nodeControllers[1].CheckCoordinate(new int[] { 5, 2 });
                    //[3, 0, 1, 4]
		        bool flag3 = __instance._coordinateInterface._nodeControllers[2].CheckCoordinate(new int[] { 0, 4, 1, 2 }); //[1, 2, 3, 0, 5, 4]
		        //MonoBehaviour.print("coordinate check: " + flag + ", " + flag2 + ", " + flag3);
		        //return flag && flag2 && flag3;
                if (flag && flag2 && flag3)
                {
                    // EMERGENCY CODES ENTERED
                    AltEnding.Instance.ModHelper.Console.WriteLine("EMERGENCY COORDS ENTERED");
                    
                    // open cage
                    if (__instance._cageClosed)
	                {
		                __instance._cageAnimator.TranslateToOriginalLocalPosition(5f);
		                __instance._cageAnimator.RotateToOriginalLocalRotation(5f);
		                __instance._cageAnimator.OnTranslationComplete -= __instance.OnCageAnimationComplete;
		                __instance._cageAnimator.OnTranslationComplete += __instance.OnCageAnimationComplete;
		                __instance._cageLoopingAudio.FadeIn(1f);
		                __instance._cageClosed = false;

                        PropsController.vesselWarningLightController.SetLightOn(true);
	                }
                }
            }

            return true;
        }

     //   [HarmonyPrefix]
     //   [HarmonyPatch(typeof(VesselWarpController), nameof(VesselWarpController.SetPowered))]
	    //private static bool VesselWarpController_SetPowered(VesselWarpController __instance, bool powered)
	    //{
		   // if (!powered)
     //       {
     //           PreEndingPropsController.VesselWarningLight.SetActive(false);
     //       }
     //       else
     //       {
     //           PreEndingPropsController.VesselWarningLight.SetActive(false);
     //       }

     //       return true;
	    //}
    }
}
