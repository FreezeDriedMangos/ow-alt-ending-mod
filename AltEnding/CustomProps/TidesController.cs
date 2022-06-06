using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.CustomProps
{
    class TidesController : MonoBehaviour
    {
        private Transform gravityObject;

        public void Initialize(AstroObject owningAstroObject, float highTide, float lowTide)
        {
            gravityObject = owningAstroObject._primaryBody.transform;
            this.transform.localScale = new Vector3(lowTide, 0.5f*(lowTide+highTide), highTide);
        }

        void Update()
        {
            this.transform.rotation = Quaternion.LookRotation(gravityObject.transform.position - this.transform.position, new Vector3(0, 1, 0));
        }
    }
}
