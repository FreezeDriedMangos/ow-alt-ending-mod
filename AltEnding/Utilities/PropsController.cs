﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AltEnding.Utilities.ModAPIs;

namespace AltEnding.Utilities
{
    class PropsController
    {
        private List<UnityEngine.GameObject> props = new List<UnityEngine.GameObject>();

        public void ClearProps()
        {
            foreach(UnityEngine.GameObject prop in props)
            {
                UnityEngine.GameObject.Destroy(prop);
            }
        }

        public void SpawnMainSystemProps(INewHorizons newHorizonsAPI)
        {
            AltEnding.PrintToModConsole("LOAIDING MAIN PROPS");
            UnityEngine.Debug.Log($"LOADING MAIN PROPSS");

            // TESTING

                string path = "QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/Nomai_ANIM_SkyWatching_Idle";
				Vector3 position = new Vector3( 18.06051f, -50.64357f, 183.141f); 
				Vector3 rotation = new Vector3(311.8565f,  287.9388f,  254.72f);
                
                newHorizonsAPI.SpawnObject(Locator._timberHearth.gameObject, Locator._timberHearth.GetRootSector(), path, position, rotation, 1, false);

            // TODO: spawn these items for testing
            // RingWorld_Body/Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Experiment_3/VisionTorchApparatus
            // DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Interactibles_Underground/Prefab_IP_VisionTorchProjector
            // DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem

            // TODO: spawn this item for actual use, and put it in the sun watching room of the stranger (the top floor of the room accessed via the dam)
            // DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem
            // TODO: modify the above item for actual use purposes

            Sector thSector = Locator._timberHearth.GetRootSector();
            GameObject prefab = GameObject.Find("RingWorld_Body/Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Experiment_3/VisionTorchApparatus");
            GameObject memoryStaff = GameObject.Instantiate(prefab, thSector.transform);


            path ="RingWorld_Body/Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Experiment_3/VisionTorchApparatus";
            Sector s = GameObject.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp").GetComponent<Sector>();
            newHorizonsAPI.SpawnObject(Locator._timberHearth.gameObject, Locator._timberHearth.GetRootSector(), path, Vector3.zero, Vector3.zero, 1, false);
            

            // "path" : "QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/Nomai_ANIM_SkyWatching_Idle", 
			// "position" : {"x": 18.06051, "y": -50.64357, "z": 183.141},  
			// "rotation" : {"x":311.8565, "y": 287.9388, "z": 254.72}
        }
        
        public void SpawnEndingProps()
        {
            // TODO: spawn this item I think? and place it on timber hearth
            // DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Interactibles_Underground/Prefab_IP_VisionTorchProjector
            // TODO: modify this item to play the ending cutscene

        
            // GameObject memoryStaff = GameObject.Instantiate("RingWorld_Body/Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Experiment_3/VisionTorchApparatus")


            
        }

        // quantum objects:
        // TimberHearth_Body/Sector_TH/Sector_Village/Sector_Observatory/Interactables_Observatory/VisibleFrom_Village/QuantumShard_Exhibit/QuantumShard_Root/
        // QuantumSocket
        // SocketedQuantumObject
            // SetQuantumSockets()
            // NOTE: SocketedQuantumObjects CAN SHARE QuantumSockets, and they handle it robustly
            // ChangeQuantumState(true) // pass true for initialization
        // Custom quantum object behavior:
            // extend QuantumObject, implement `protected abstract bool ChangeQuantumState(bool skipInstantVisibilityCheck);`

    }
}
