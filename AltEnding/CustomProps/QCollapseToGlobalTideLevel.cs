using NewHorizons.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.CustomProps
{
    public class QCollapseToGlobalTideLevel : MonoBehaviour
    {
        // if states type:
        // stateObject.currentState.transform.position = ...
        //
        // if sockets type:
        // socketedQuantumObject.transform.position = ...
        Dictionary<Transform, Vector3> originalPos = new Dictionary<Transform, Vector3>();

        GlobalTidesController tides;
        void Start()
        {
            tides = GetComponent<GlobalTidesController>();
        }

        public void OnCollapse(QuantumObject qo)
        {
            if (qo as NHMultiStateQuantumObject)
            {
                if (!originalPos.ContainsKey(qo.transform)) originalPos[qo.transform] = qo.transform.localPosition;
                qo.transform.localPosition = originalPos[qo.transform].normalized * tides.currentTide;
            }
        }
    }
}
