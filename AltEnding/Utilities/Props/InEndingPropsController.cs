using AltEnding.CustomProps;
using NewHorizons.Builder.Atmosphere;
using NewHorizons.Builder.General;
using NewHorizons.Builder.Props;
using NewHorizons.Components;
using NewHorizons.External.Modules;
using NewHorizons.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static NewHorizons.External.Modules.PropModule;

namespace AltEnding.Utilities.Props
{
    public static class InEndingPropsController
    {
        public static SpawnPoint stationSpawnPoint;

        // Quantum moon aspects
        public static AstroObject QMGiantsDeep { get; private set; }
        public static AstroObject QMDarkBramble { get; private set; }
        public static AstroObject QMEye { get; private set; }
        public static AstroObject QMBrittleHollow { get; private set; }
        public static AstroObject QMHourglassTwins { get; private set; }
        public static AstroObject QMSun { get; private set; }

        // sun aspect clouds
        // QuantumMoon_Body/Clouds_QM


        // TODO: delete this "QMGiantsDeepAspect_Body/Sector/State_GD(Clone)/Interactables_GDState/QuantumDeadNomaiSuit/"
        // TODO: add this component to the fluid volume, and replace RadialFluidVolume: ElipsoidFluidVolume
        public static void SpawnProps()
        {
            AltEnding.WriteLine("Spawning in-ending props");

            QMGiantsDeep = AstroObjectLocator.GetAstroObject("QM Giant's Deep Aspect");
            QMDarkBramble = AstroObjectLocator.GetAstroObject("QM Dark Bramble Aspect");
            QMEye = AstroObjectLocator.GetAstroObject("QM Eye Aspect");
            QMBrittleHollow = AstroObjectLocator.GetAstroObject("QM Brittle Hollow Aspect");
            QMHourglassTwins = AstroObjectLocator.GetAstroObject("QM Hourglass Twins Aspect");
            QMSun = AstroObjectLocator.GetAstroObject("QM Sun Aspect");

            CreateSunAspectClouds();
            CreateSunAspectSpawnPoint();

            CreateGiantsDeepAspectTides();
            CreateGiantsDeepAspectIslands();
        
            CreateHourglassTwinsAspectTides();
            /*
            
			{
				"path": "TowerTwin_Body/Sector_TowerTwin/Geometry_TowerTwin/OtherComponentsGroup/ControlledByProxy_Base/Terrain_HT_TowerTwin_TLD_Shell/TimeLoopShell/outerShell",
				"position": {"x": 0, "y": 0, "z": 0},
				"scale": 2
			},
			{
				"path": "QuantumMoon_Body/Sector_QuantumMoon/State_HT",
				"position": {"x": 0, "y": 0, "z": 0},
				"rotation": {"x": 90, "y": 0, "z": 0},
				"removeChildren": [
					"Interactables_HTState/QuantumDeadNomaiSuit"
				]
			}
             */

            /*
                 "Atmosphere": {
		            "size" : 400,
		            "clouds": {
			            "innerCloudRadius": 330,
			            "outerCloudRadius": 400,

			            "texturePath": "images/Clouds_QM_Top_d.png",
                        "capPath": "Clouds_QM_TopCap_d",
			            "unlit": true
		            }
	            },
             */
        
            // sun station props
            /* TestSunMoonSatellite_Body/Sector/Prefab_HEA_Campfire(Clone)
             * -0.8402 -11.3621 -19.2534
             * -15.1791 20.8606 -0.3655
             * 0 265.6094 0
             * 
             */

            string[] files = System.IO.Directory.GetFiles(AltEnding.Instance.ModHelper.Manifest.ModFolderPath + "/images/vision1", "*.png");
            files = new string[] { files[0], files[1] };
            SlideInfo[] slides = files.Select(f => f.Remove(0, AltEnding.Instance.ModHelper.Manifest.ModFolderPath.Length)).Select(f => new SlideInfo() { imagePath=f }).ToArray();

            // slides[0].backdropAudio = "SunStation"; // "OW_NM_SunStation";
            // slides[251].backdropAudio = "SadNomaiTheme"; // "OW NM Nomai Ruins 081718 AP";

            ProjectionInfo info = new ProjectionInfo()
            {
                position=new Vector3(-5.254965f, -70.73996f, 1.607201f),
                rotation=new Vector3(0, 0, 0),
                type=ProjectionInfo.SlideShowType.StandingVisionTorch,
                slides=slides
            };

            var station = AstroObjectLocator.GetAstroObject("QM Sun Station");
            SpawnVisionTorchAndChair(station.gameObject, station._rootSector, new Vector3(-15.4589f, 20.7242f, -7.8966f), new Vector3(0f, 9.2435f, 0f), info);

        
            GameObject clouds1 = GameObject.Instantiate(GameObject.Find("QuantumMoon_Body/Clouds_QM"));
            clouds1.transform.parent = AstroObjectLocator.GetAstroObject("QM Sun Aspect").transform;
            clouds1.transform.localPosition = Vector3.zero;
            clouds1.transform.localScale = Vector3.one*10;

            SpawnTowerTeleportCampfires();
            SpawnExitPrevention();
        }

        private static void CreateGiantsDeepAspectIslands()
        {
            //var giantsDeepAspectGO = GameObject.Find("QMGiantsDeepAspect_Body");
            //var island = GameObject.Instantiate(GameObject.Find("GabbroIsland_Body"));
            //island.transform.position = giantsDeepAspectGO.transform.position + 75f*(new Vector3(1, 0, 0));
            //island.GetComponent<AlignWithTargetBody>()._targetBody = giantsDeepAspectGO.GetComponent<OWRigidbody>();
            
            //var islandController = island.GetComponent<IslandController>();
            //islandController._barrierRepelFluids = island.GetComponentsInChildren<IslandRepelFluidVolume>();
            //islandController._campfire = island.GetComponentInChildren<Campfire>();
            //islandController._fluidDetector = island.GetComponentInChildren<DynamicFluidDetector>();
            //islandController._inheritanceFluid = island.GetComponentInChildren<InheritibleFluidVolume>();
            //islandController._islandBody = island.GetComponent<OWRigidbody>();
            //islandController._planetTransform = giantsDeepAspectGO.transform;
            //islandController._safetyTractorBeams = island.GetComponentsInChildren<SafetyTractorBeamController>();
            //islandController._transform = island.transform;
            //islandController._zeroGVolume = island.GetComponentInChildren<ZeroGVolume>();

            //island.GetComponent<OWRigidbody>()._origParentBody = giantsDeepAspectGO.GetComponent<OWRigidbody>();
            //var forceDetector = GameObject.Find("GabbroIsland_Body(Clone)/Detector_GabbroIsland").GetComponent<ConstantForceDetector>();
            //forceDetector._activeInheritedDetector = GameObject.Find("QMGiantsDeepAspect_Body/FieldDetector").GetComponent<ConstantForceDetector>();
            //forceDetector._detectableFields = new ForceVolume[]{ GameObject.Find("QMGiantsDeepAspect_Body/GravityWell").GetComponent<GravityVolume>() };

            //// destroy unneeded components
            //GameObject.Find("GabbroIsland_Body(Clone)/Sector_GabbroIsland/Interactables_GabbroIsland/Signal_Flute").SetActive(false);
            //GameObject.Find("GabbroIsland_Body(Clone)/Sector_GabbroIsland/Interactables_GabbroIsland/Traveller_HEA_Gabbro").SetActive(false);
            //GameObject.Find("GabbroIsland_Body(Clone)/GD_GABBRO_ISLAND").SetActive(false);
        }

        // MultiStateQuantumObject guide
        // make an empty go and add a MultiStateQuantumObject
        // make some props that are considered different states of this object, and set their parent to the multistatequantumobject. add a QuantumState component to each
        // make a visibility tracker for each state (add a BoxShape and a ShapeVisibilityTracker to each)
        // set the _visibilityTrackers and _states properties on the MultiStateQuantumObject
        // optionally set MultiStateQuantumObject._loop to true
        //
        // if you want to display a sequence, like the campfire on the eye, parent teh visibility trackers to their respective states, so only one will be active at a time
        //
        // if you want an object that appears and disapears, make one of the states just an empty game object
        private static void CreateGiantsDeepAspectTides()
        {
            // delete original water
            GameObject.Find("QMGiantsDeepAspect_Body/Sector/State_GD(Clone)/Volumes_GDState/OceanFluidVolume").SetActive(false);
            GameObject.Find("QMGiantsDeepAspect_Body/Sector/State_GD(Clone)/Effects_GDState/Ocean").SetActive(false);

            // add tides
            GameObject water = GameObject.Find("QMGiantsDeepAspect_Body/Sector/Water");
            water.AddComponent<TidesController>().Initialize(GameObject.Find("QMGiantsDeepAspect_Body").GetComponent<AstroObject>(), 80.5f, 70.5f);

            // set up the water volume type that the tides require in order to function correctly
            GameObject fluidVolumeGO = GameObject.Find("QMGiantsDeepAspect_Body/Sector/Water/WaterVolume");
            GameObject.Destroy(fluidVolumeGO.GetComponent<NHFluidVolume>());
            var fluidVolume = fluidVolumeGO.AddComponent<ElipsoidFluidVolume>();
            fluidVolume._fluidType = FluidVolume.Type.WATER;
            fluidVolume._attachedBody = GameObject.Find("QMGiantsDeepAspect_Body").GetComponent<OWRigidbody>();
            fluidVolume._triggerVolume = fluidVolumeGO.GetComponent<OWTriggerVolume>();
            fluidVolume._layer = LayerMask.NameToLayer("BasicEffectVolume");
        }

        private static void CreateHourglassTwinsAspectTides()
        {
            // add tides
            GameObject sand = GameObject.Find("QMHourglassTwinsAspect_Body/Sector/Sand");
            // sand.AddComponent<TidesController>().Initialize(GameObject.Find("QMHourglassTwinsAspect_Body").GetComponent<AstroObject>(), 2*145, 2*130);
            sand.AddComponent<GlobalTidesController>().Initialize(0.1f, 2*145, 2*130);
        }

        public static void CreateSunAspectClouds()
        {

            // destroy atmosphere.air   "QMSunAspect_Body/Sector/Air"
            GameObject.Find("QMSunAspect_Body/Sector/Air").SetActive(false);

            //var sunAspect = AstroObjectLocator.GetAstroObject("QM Sun Aspect");
            //var cloudsModule = new AtmosphereModule()
            //{
            //    clouds = new AtmosphereModule.CloudInfo()
            //    {
            //        innerCloudRadius = 500,
            //        outerCloudRadius = 600,
            //        useBasicCloudShader = true
            //    }
            //};
            //CloudsBuilder.Make(sunAspect.gameObject, sunAspect._rootSector, cloudsModule, AltEnding.Instance);
        }

        public static void FixStationGlass()
        {
            // TODO: I may need to use the Find that works for inactive game objects
            var opcGo = GameObject.Find("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon/Geo_OrbitalProbeCannon/ControlledByProxy_OrbitalProbeCannon/Structure_NOM_OrbitalProbeCannon/OPC_Hub_Geo");
            var glassMat = opcGo.GetComponent<MeshRenderer>().sharedMaterials[3];

            var glassFloorGo = GameObject.Find("QMSunStation_Body/Sector/Quantum Moon Sun Station(Clone)/warp_ext/Glass Floor");
            glassFloorGo.GetComponent<MeshRenderer>().sharedMaterial = glassMat;
        }

        public static void CreateSunAspectSpawnPoint()
        {
            // -16.7344 20.7243 -7.5293
            var station = AstroObjectLocator.GetAstroObject("QM Sun Station");
            SpawnModule s = new SpawnModule()
            {
                playerSpawnPoint = new Vector3(-16.7344f, 20.7243f, -7.5293f)
            };
            stationSpawnPoint = SpawnPointBuilder.Make(station.gameObject, s, station._owRigidbody);
            stationSpawnPoint.transform.position = stationSpawnPoint.transform.position - stationSpawnPoint.transform.TransformDirection(Vector3.up) * 3.9f;
        }

        public static void SpawnVisionTorchAndChair(GameObject g, Sector s, Vector3 pos, Vector3 rot, PropModule.ProjectionInfo projectionInfo)
        {
            string chairPath = "RingWorld_Body/Sector_RingInterior/Sector_Zone1/Sector_ProjectionHouse_Zone1/Props_ProjectionHouse_Zone1/OtherComponentsGroup/Prefab_IP_Chair";
            GameObject chair = DetailBuilder.MakeDetail(g, s, chairPath, pos, rot, 1, false);
            
            string stoolPath = "RingWorld_Body/Sector_RingInterior/Sector_Zone1/Sector_ProjectionHouse_Zone1/SmallProps_ProjectionHouse_Zone1/OtherComponentsGroup/Props_IP_Stool";
            GameObject stool = DetailBuilder.MakeDetail(g, s, stoolPath, pos, rot, 1, false);
            stool.transform.parent = chair.transform;
            stool.transform.localEulerAngles = new Vector3(0, 6.8629f, 0);
            stool.transform.localPosition = new Vector3(-1.3179f, 0, 0.1576f);
        
            string helmetPath = "Comet_Body/Prefab_NOM_Shuttle/Sector_NomaiShuttleInterior/Props_NomaiShuttleInterior/Hanging Suit/Props_NOM_Mask_GearNew";
            GameObject helmet = DetailBuilder.MakeDetail(g, s, helmetPath, pos, rot, 1, false);
            helmet.transform.parent = chair.transform;
            helmet.transform.localEulerAngles = new Vector3(333.0699f, 260.7565f, 0f);
            helmet.transform.localPosition = new Vector3(-0.3915f, 1.3813f, 0.047f);

            GameObject staff = ProjectionBuilder.MakeStandingVisionTorch(g, s, projectionInfo, AltEnding.Instance);
            staff.transform.parent = chair.transform;
            staff.transform.localEulerAngles = new Vector3(345.6006f, 26.9245f, 30.0002f);
            staff.transform.localPosition = new Vector3(1.1001f, 0.83f, -0.0708f);

            /*
            * TestSunMoonSatellite_Body/Sector/Prefab_IP_Chair(Clone)
            * -0.9847 -7.48 -25.7127
            * -15.4589 20.7242 -7.8966
            * 0 9.2435 0
            * 
            * TestSunMoonSatellite_Body/Sector/Props_IP_Stool(Clone)
            * -0.8707 -8.7439 -26.1018
            * -16.7344 20.7243 -7.5293
            * 0 16.1064 0
            * AS CHILD OF CHAIR
            * -6.2494 -7.8974 -30.1722
            * -1.3179 0.0001 0.1576
            * 0 6.8629 0
            * 
            * TestSunMoonSatellite_Body/Sector/Prefab_IP_VisionTorchItem(Clone)
            * -1.9051 -6.5219 -25.3397
            * -14.3844 21.5542 -8.1432
            * 345.6006 36.168 30.0002
            * AS CHILD OF CHAIR
            * -7.5012 -5.6945 -29.7622
            * 1.1001 0.83 -0.0708
            * 345.6006 26.9245 30.0002
            * 
            * 
            * Comet_Body/Prefab_NOM_Shuttle/Sector_NomaiShuttleInterior/Props_NomaiShuttleInterior/Hanging Suit/Props_NOM_Mask_GearNew
            * -9.3871 -8.048 -29.5199
            * -15.8378 22.1055 -7.7873
            * 333.0699 270 0
            * AS CHILD OF CHAIR
            * -7.7068 -7.1296 -30.4263
            * -0.3915 1.3813 0.047
            * 333.0699 260.7565 0
            */
        }

        private static void SpawnTowerTeleportCampfires()
        {
            var GDtoDB = TeleportCampfire.Spawn(QMGiantsDeep.gameObject, QMGiantsDeep.GetRootSector(), new Vector3(19.18f, 49.98f, -48.56f));
            var DBtoGD = TeleportCampfire.Spawn(QMDarkBramble.gameObject, QMDarkBramble.GetRootSector(), new Vector3(57.27f, -49.61f, 53.99f));
            GDtoDB.LinkCampfire(DBtoGD);
        }

        private static void SpawnExitPrevention()
        {
            foreach (var planet in new AstroObject[] { QMBrittleHollow, QMDarkBramble, QMEye, QMGiantsDeep, QMHourglassTwins, QMSun })
            {
                var exitPrevention = planet.gameObject.AddComponent<QMAspectExitPrevention>();
                exitPrevention.radius = 220f;
            }
        }
    }
}
