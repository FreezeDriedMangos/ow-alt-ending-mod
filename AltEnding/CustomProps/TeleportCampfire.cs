using NewHorizons.Builder.Props;
using NewHorizons.Utility;
using UnityEngine;

namespace AltEnding.CustomProps
{
    public class TeleportCampfire : Campfire
    {
        private TeleportCampfire _pairedCampfire;

        public override void Awake()
        {
            base.Awake();
        }

        public override void OnStopSleeping()
        {
            Teleport();
        }

        public void Teleport()
        {
            if (_pairedCampfire == null) return;

            var player = Locator.GetPlayerBody();

            var pos = transform.InverseTransformPoint(player.GetPosition());

            var planet = _pairedCampfire.GetAttachedOWRigidbody();

            var newWorldPos = _pairedCampfire.transform.TransformPoint(pos);
            var forwards = (_pairedCampfire.transform.position - newWorldPos).normalized;
            var upwards = (_pairedCampfire.transform.position - newWorldPos).normalized;
            var newWorldRot = Quaternion.LookRotation(forwards, upwards);

            // Move the player up a tiny bit to avoid falling through the ground as often
            newWorldPos += upwards * 0.1f;

            player.WarpToPositionRotation(newWorldPos, newWorldRot);
            player.SetVelocity(planet.GetPointVelocity(newWorldPos));
        }

        public void LinkCampfire(TeleportCampfire pairedCampfire)
        {
            _pairedCampfire = pairedCampfire;
            pairedCampfire._pairedCampfire = this;
        }

        private static GameObject _prefab;
        public static TeleportCampfire Spawn(GameObject planet, Sector sector, Vector3 position)
        {
            if (_prefab == null)
            {
                var prefabPath = "BrittleHollow_Body/Sector_BH/Sector_Crossroads/Interactables_Crossroads/VisibleFrom_BH/Prefab_HEA_Campfire";
                _prefab = SearchUtilities.Find(prefabPath).InstantiateInactive();
                _prefab.transform.rotation = Quaternion.identity;

                // Original campfire we're going to copy stuff from
                var campfire = _prefab.GetComponentInChildren<Campfire>();

                var controllerObj = new GameObject("Controller_TeleportCampfire");
                var teleporter = controllerObj.AddComponent<TeleportCampfire>();
                controllerObj.transform.SetParent(_prefab.transform);
                controllerObj.transform.localPosition = Vector3.zero;
                controllerObj.transform.localRotation = Quaternion.identity;

                // Copy over fields
                teleporter._initialState = campfire._initialState;
                teleporter._sector = sector;
                teleporter._canSleepHere = campfire._canSleepHere;
                teleporter._lookUpWhileSleeping = campfire._lookUpWhileSleeping;
                teleporter._heatConeBottom = campfire._heatConeBottom;
                teleporter._heatConeTop = campfire._heatConeTop;
                teleporter._heatConeRadius = campfire._heatConeRadius;
                teleporter._heatFalloffDistance = campfire._heatFalloffDistance;
                teleporter._logSphereCenter = campfire._logSphereCenter;
                teleporter._logSphereRadius = campfire._logSphereRadius;
                teleporter._rockHeight = campfire._rockHeight;
                teleporter._audio = campfire._audio;
                teleporter._oneShotAudio = campfire._oneShotAudio;
                teleporter._lightController = campfire._lightController;
                teleporter._litRenderers = campfire._litRenderers;
                teleporter._hideWhileSmolderingRenderers = campfire._hideWhileSmolderingRenderers;
                teleporter._smolderingParticles = campfire._smolderingParticles;
                teleporter._litParticles = campfire._litParticles;
                teleporter._flames = campfire._flames;
                teleporter._embers = campfire._embers;
                teleporter._ash = campfire._ash;
                teleporter._interactVolume = campfire._interactVolume;
                teleporter._attachPoint = campfire._attachPoint;
                teleporter._hazardVolume = campfire._hazardVolume;
                teleporter._burnedSlideReelSocket = campfire._burnedSlideReelSocket;

                // Now get rid of the original controller
                GameObject.Destroy(campfire.gameObject);

                // Making it a different colour for testing
                teleporter._flames.material.color = new Color(1f, 0f, 1f, 1f);
            }

            var rotation = Quaternion.FromToRotation(Vector3.up, position.normalized).eulerAngles;

            var obj = DetailBuilder.MakeDetail(planet, sector, _prefab, position, rotation, 1, false);
            obj.transform.name = "Prefab_HEA_TeleportCampfire";

            return obj.GetComponentInChildren<TeleportCampfire>();
        }
    }
}
