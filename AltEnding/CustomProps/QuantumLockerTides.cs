using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.CustomProps
{
    public class QuantumLockerTides : MonoBehaviour
    {
        public GameObject tides;
        public GameObject planet;

        public float minTideFrac = -1;
        public float maxTideFrac = 2;

        public bool ShouldLock()
        {
            var posRelPlanet = planet.transform.InverseTransformPoint(this.transform.position);
            var pos = new Vector2(posRelPlanet.x, posRelPlanet.z);    

            var tidesRot = tides.transform.localEulerAngles.y;
            var tidesVec = new Vector2(Mathf.Cos(tidesRot), Mathf.Sin(tidesRot));

            var tidesFracAtPos = Mathf.Abs(Vector2.Dot(pos, tidesVec));
            return minTideFrac <= tidesFracAtPos && tidesFracAtPos <= maxTideFrac;
        }
    }
}
