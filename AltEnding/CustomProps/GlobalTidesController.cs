using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.CustomProps
{
    public class GlobalTidesController : MonoBehaviour
    {
        
        float highTide;
        float lowTide;
        float tideSpeed;

        public float currentTide { get; private set; }
        float currentTime;

        public void Initialize(float tideSpeed, float highTide, float lowTide)
        {
            this.highTide = highTide;
            this.lowTide = lowTide;
            this.tideSpeed = tideSpeed;

            currentTide = lowTide;
            currentTime = 0;
        }

        void Update()
        {

            //
            // triangle wave
            //

            //currentTide += tideSpeed;
            
            //if (currentTide >= highTide)
            //{
            //    currentTide = highTide;
            //    tideSpeed *= -1;
            //} else if (currentTide <= lowTide)
            //{
            //    currentTide = lowTide;
            //    tideSpeed *= -1;
            //}

        
            //
            // sin wave
            //

            currentTime += tideSpeed;
            var tideFrac = 0.5f - 0.5f*Mathf.Cos(Mathf.PI*currentTime);
            
            currentTide = (highTide-lowTide)*tideFrac + lowTide;
            
            //
            // apply tide
            //

            transform.localScale = new Vector3(currentTide, currentTide, currentTide);
        }
    }
}
