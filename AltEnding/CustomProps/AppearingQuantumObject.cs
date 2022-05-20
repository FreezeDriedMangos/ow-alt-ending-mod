using OWML.Common;
using OWML.ModHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltEnding.CustomProps
{

    // TODO: quantum lighnting object
    // randomly gets hit with lightning to appear for half a second before disapearing
    // This is the real component I want for this mod

    // use the existing QuantumLightningObject
    // make all units (a single game object whose children comprise a collection of game objects that should all appear together)  children of the game object that has the QuantumLightningObject component
    // set quantumLightningObject_instance._models = those children
    // make the quantumLightningObject_instance's game object a child of the sector it belongs to
    // 
    // set quantumLightningObject_instance._light to something
    // set quantumLightningObject_instance._audioSource to something
    // set quantumLightningObject_instance._particleSystem to something
    //
    // when ready for the lightning to start, set quantumLightningObject_instance.SetActivation(true);
    //
    // before parenting gameobjects into a unit, make sure to set the unit's position to an average of all of their positions

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

            return CheckIllumination() 
                ? CheckVisibilityInstantly() 
                : CheckPointInside(Locator.GetPlayerCamera().transform.position);

            //return !(
            //    IsPlayerEntangled()
            //    ? CheckIllumination()
            //    : (
            //        CheckIllumination() 
            //        ? CheckVisibilityInstantly() 
            //        : CheckPointInside(Locator.GetPlayerCamera().transform.position)
            //    ) 
            //);
        }

        public override bool ChangeQuantumState(bool skipInstantVisibilityCheck)
        {
            AltEnding.Instance.ModHelper.Console.WriteLine($"Changing quantum state for {gameObject.name}");
            UnityEngine.Debug.Log($"Changing quantum state for {gameObject.name}");


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
