using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltEnding.Utilities
{
    class BlinkController
    {
        private UnityEngine.Texture2D originalEyeMaskTexture;
        private UnityEngine.Texture2D threeEyeMaskTexture;
        private PlayerCameraEffectController PlayerCameraEffectController;

        public BlinkController(PlayerCameraEffectController pcec)
        {
            PlayerCameraEffectController = pcec;
            originalEyeMaskTexture = pcec._owCamera.postProcessingSettings.eyeMask.eyeMask;

            // TODO: load the 3 eye texture
        }

        public void SetBlinkNumEyes(int numEyes)
        {
            UnityEngine.Texture2D eyeMaskTexture = null;
        
            switch(numEyes)
            {
                case 3:
                    eyeMaskTexture = threeEyeMaskTexture;
                    break;
                case 4:
                default:
                    eyeMaskTexture = this.originalEyeMaskTexture;
                    break;
            }

            PlayerCameraEffectController._owCamera.postProcessingSettings.eyeMask.eyeMask = eyeMaskTexture;
        }

        public void Blink()
        {
            PlayerCameraEffectController.OpenEyes(1, true);
        }
    }
}
