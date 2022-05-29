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

            // TODO: display emergency coords in bottom left corner if the fact has been revealed
            if (slot == __instance._warpVesselSlot && __instance._hasPower)
            {

                /* emergency coords 
                 *                                               
                 *    0    0            o    o           0    0                                       
                 *    *  *  *                            *  *  *                                    
                 * o  * *     0      0**********0     o  * *     0                                   
                 *    **                                 **                                         
                 *    0    o            o    o           0    o                                      
                 */                 

		        bool flag  = __instance._coordinateInterface._nodeControllers[0].CheckCoordinate(new int[] { 0, 4, 1, 2 }); //[1, 5, 4]
		        bool flag2 = //[3, 0, 1, 4]
                    __instance._coordinateInterface._nodeControllers[1].CheckCoordinate(new int[] { 2, 5 }) ||
                    __instance._coordinateInterface._nodeControllers[1].CheckCoordinate(new int[] { 5, 2 }); 
		        bool flag3 = __instance._coordinateInterface._nodeControllers[2].CheckCoordinate(new int[] { 0, 4, 1, 2 }); //[1, 2, 3, 0, 5, 4]
		        
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
