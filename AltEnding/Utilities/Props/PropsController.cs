﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AltEnding.Utilities.ModAPIs;
using AltEnding.CustomProps;
using NewHorizons.Builder.Props;
using static NewHorizons.External.Modules.PropModule;
using NewHorizons.Utility;
using NewHorizons.Handlers;
using NewHorizons.External.Configs;
using NewHorizons.External.Modules;

namespace AltEnding.Utilities.Props
{
    public class PropsController
    {
        public static VesselWarningLightController vesselWarningLightController;

        public static void SpawnProps(string system)
        {
            // DEBUG:
            // SkinReplacer.ReplaceSkin(GameObject.Find("Player_Body/Traveller_HEA_Player_v2/Traveller_Mesh_v01:Traveller_Geo"), "Feldspar");
            SkinReplacer.ReplaceSkin(GameObject.Find("Player_Body/Traveller_HEA_Player_v2/Traveller_Mesh_v01:Traveller_Geo"), "Solanum");
            // Player_Body/Traveller_HEA_Player_v2/Traveller_Mesh_v01:Traveller_Geo/Traveller_Mesh_v01:Props_HEA_Jetpack

            // Test teleport campfires
            var th = Locator.GetAstroObject(AstroObject.Name.TimberHearth);

            var thFire = TeleportCampfire.Spawn(th.gameObject, th.GetRootSector(), new Vector3(14.3f, -50.2f, 183.6f));
            var thFire2 = TeleportCampfire.Spawn(th.gameObject, th.GetRootSector(), new Vector3(-6, -109.1f, 229.5f));

            thFire.LinkCampfire(thFire2);
            // END DEBUG

            if (system == "SolarSystem")
            {
                PreEndingPropsController.SpawnProps();
            }
            else if (system == "Clay.AltEnding")
            {
                InEndingPropsController.SpawnProps();
            }

            SpawnUniversalProps();
        }

        public static void SpawnUniversalProps()
        {
            //
            // Vessel Warning Light  
            //

            var vesselSectorGameObject = GameObject.Find("DB_VesselDimension_Body");
            var lightObject = new GameObject("Warning Light");
            lightObject.transform.parent = vesselSectorGameObject.transform;
            lightObject.transform.localPosition = new Vector3(191.4552f, 45.0478f, -15.27039f);
            lightObject.name = "Warning Light";
            var light = lightObject.AddComponent<Light>();
            light.color = Color.red;
            light.range = 100;
            light.intensity = 0;
            vesselWarningLightController = lightObject.AddComponent<VesselWarningLightController>();
            //vesselWarningLightController.SetLightOn(true);
        }

        public void SpawnDysonSwarm()
        { 
            // look at AsteroidBeltBuilder.Make
            // make a random with a set hardcoded seed
            // set semiMajorAxis to baseDistance + satelliteDistance*i
            // set inclination and longitudeofascending to random
            // set eccentricity to 0

            //PlanetConfig conf = new PlanetConfig()
            //{
            //    name="swarmsatellite",
            //    Orbit = 
            //        new OrbitModule()
            //        {
            //            semiMajorAxis= 1300,
            //            inclination= 0,
            //            primaryBody= "TIMBER_HEARTH",
            //            isMoon= true,
            //            isTidallyLocked= true,
            //            longitudeOfAscendingNode= 0,
            //            eccentricity= 0,
            //            argumentOfPeriapsis= 0
            //        },

            //};
            //PlanetCreationHandler.LoadBody(new NewHorizonsBody(conf, AltEnding.Instance, null));

            
            UnityEngine.Random.InitState(10);
            var count = 20;
            var size = 10;
            var startingOrbit = 2500; // 2000 seems to be the radius of the sun
            var orbitScale = 1.2f;
            for (int i = 0; i < count; i++)
            {
                var config = new PlanetConfig();
                config.name = $"Swarm Satellite {i}";
                // config.starSystem = "uncertainFuturesEndingSystem";

                config.Base = new BaseModule()
                {
                    hasMapMarker = true, // fasle
                    surfaceGravity = 0,
                    surfaceSize = size,
                    hasReferenceFrame = false,
                    gravityFallOff = GravityFallOff.InverseSquared,

                    groundSize = size
                };

                config.Orbit = new OrbitModule()
                {
                    isMoon = true,
                    inclination = UnityEngine.Random.Range(-180f, 180f),
                    longitudeOfAscendingNode = UnityEngine.Random.Range(-180f, 180f),
                    trueAnomaly = 360f * (i + UnityEngine.Random.Range(-0.2f, 0.2f)) / (float)count,
                    primaryBody = "Timber Hearth",
                    semiMajorAxis = startingOrbit + (size*orbitScale)*i,
                    showOrbitLine = true // false
                };

                config.Props = new PropModule()
                {
                    details = new DetailInfo[]
                    {
                        new DetailInfo() { 
				            assetBundle = "planets/swarmsatellite",
				            path = "Assets/uncertainFutures/SwarmSatellite.prefab",
				            position = Vector3.zero,
                            rotation = Vector3.zero,
                            scale = size
			            },
                    }
                };

                var satellite = new NewHorizonsBody(config, AltEnding.Instance);
                // PlanetCreationHandler.NextPassBodies.Add(satellite);
                PlanetCreationHandler.GenerateBody(satellite); // I added this, original was above line
            }
        }

        public void SpawnEndingProps()
        {
            // TODO: spawn this item I think? and place it on timber hearth
            // DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Interactibles_Underground/Prefab_IP_VisionTorchProjector
            // TODO: modify this item to play the ending cutscene

        
            // GameObject memoryStaff = GameObject.Instantiate("RingWorld_Body/Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Experiment_3/VisionTorchApparatus")


            
        }

        public void SpawnQM_PreEndingProps()
        {

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
