using AltEnding.Utilities.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.CustomProps
{
    public class QMAspectExitPrevention : SectoredMonoBehaviour
    {
        [SerializeField]
        public float radius;

        private OWRigidbody player;
        private SpawnPoint _spawnPoint;
        private PlayerSpawner _spawner;

        private bool _isSectorOccupied;

        public void Start()
        {
            player = Locator.GetPlayerBody();
            _spawnPoint = InEndingPropsController.QMEye.gameObject.GetComponentInChildren<SpawnPoint>();
            _spawner = Locator.GetPlayerBody().GetComponent<PlayerSpawner>();
            SetSector(gameObject.GetAttachedOWRigidbody().GetComponent<AstroObject>().GetRootSector());
        }

        public override void OnSectorOccupantsUpdated()
        {
            _isSectorOccupied = _sector.ContainsAnyOccupants(DynamicOccupant.Player);
        }

        public void FixedUpdate()
        {
            if (!_isSectorOccupied) return;

            if ((transform.position - player.transform.position).sqrMagnitude > radius * radius)
            {
                AltEnding.BlinkController.Blink();
                _spawner.DebugWarp(_spawnPoint);
            }
        }
    }
}
