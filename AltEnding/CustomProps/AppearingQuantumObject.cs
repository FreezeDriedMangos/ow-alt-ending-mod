using OWML.Common;
using OWML.ModHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltEnding.CustomProps
{
    class AppearingQuantumObject : QuantumObject
    {
        private List<AppearingQuantumObject> entangledObjects = new List<AppearingQuantumObject>();
        private static Random rng = new Random();

        private bool _isPresenting;

        public override void Awake()
        {
            base.Awake();
            _isPresenting = gameObject.activeSelf;
        }

        public bool IsPresenting
        {
            get { return _isPresenting; }
            /*private*/ set { this.gameObject.SetActive(value); _isPresenting = value; }
        }

        public void AddEntangledObject(AppearingQuantumObject aqo)
        {
            entangledObjects.Add(aqo);
            aqo.entangledObjects.Add(aqo);
        }

        public bool CheckIsVisible()
        {
            bool oldIsPresenting = IsPresenting;
            IsPresenting = true;
            
            if (IsPresenting != oldIsPresenting) IsPresenting = oldIsPresenting;

            return !(
                IsPlayerEntangled()
                ? CheckIllumination()
                : (
                    CheckIllumination() 
                    ? CheckVisibilityInstantly() 
                    : CheckPointInside(Locator.GetPlayerCamera().transform.position)
                ) 
            );
        }

        public override bool ChangeQuantumState(bool skipInstantVisibilityCheck)
        {
            AltEnding.Print($"Changing quantum state for {gameObject.name}");


            if (entangledObjects.Count <= 0)
            {
                IsPresenting = !IsPresenting;
                return true;
            }

            if (!IsPresenting)
            {
                return false;
            }

            if (skipInstantVisibilityCheck)
            {
                IsPresenting = false;
                entangledObjects[rng.Next(0, entangledObjects.Count)].IsPresenting = true;
            }

            IsPresenting = false;

            var shuffledObjects = entangledObjects.OrderBy(a => rng.Next()).ToList();
            foreach(AppearingQuantumObject aqo in shuffledObjects)
            {
                if (aqo.IsVisible()) continue;

                aqo.IsPresenting = true;
                return true;
            }

            IsPresenting = true;
            return false;
        }
    }
}
