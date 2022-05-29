using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.CustomProps
{
    [RequireComponent(typeof(Light))]
    public class VesselWarningLightController : MonoBehaviour
    {
        private Light light;

        public float pulseSpeed = 0.05f;
        public int frameTimer = 0;

        public bool on = false;
        public bool turningOff = false;

        public int autoOffTime = 550;

        void Start()
        {
            light = GetComponent<Light>();    
        }

        void Update()
        {
            // TODO: if game paused, return
            if (!on) return;
            if (autoOffTime > 0 && frameTimer >= autoOffTime) SetLightOn(false);

            frameTimer++;
            light.intensity = -0.5f*Mathf.Cos(pulseSpeed*frameTimer) + 0.5f;

            if (turningOff)
            {
                var prevT = (pulseSpeed*(frameTimer-1)) % (2*Mathf.PI);
                var prevFrameWasDimming = Mathf.PI <= prevT && prevT <= 2*Mathf.PI;

                var T = (pulseSpeed*frameTimer) % (2*Mathf.PI);
                var isDimming = Mathf.PI <= T && T <= 2*Mathf.PI;
                if (prevFrameWasDimming && !isDimming)
                {
                    // if we've reached maximum dim already, that means we should turn off
                    on = false;
                    turningOff = false;
                    light.intensity = 0;
                }
            }
        }

        public void SetLightOn(bool lightOn)
        {
            bool isFullyOn = on && !turningOff;

            if (lightOn && !isFullyOn)
            {
                frameTimer = 0;
                on = true;
            }
            else if (!lightOn && on)
            {
                turningOff = true;
            }
        }
    }
}
