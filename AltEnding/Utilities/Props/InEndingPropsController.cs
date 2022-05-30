using NewHorizons.Builder.General;
using NewHorizons.Builder.Props;
using NewHorizons.External.Modules;
using NewHorizons.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.Utilities.Props
{
    public static class InEndingPropsController
    {
        public static SpawnPoint stationSpawnPoint;

        public static void SpawanProps()
        {
            CreateSunAspectSpawnPoint();

        
            // sun station props
            /* TestSunMoonSatellite_Body/Sector/Prefab_HEA_Campfire(Clone)
             * -0.8402 -11.3621 -19.2534
             * -15.1791 20.8606 -0.3655
             * 0 265.6094 0
             * 
             */

            var station = AstroObjectLocator.GetAstroObject("Test Sun Moon Satellite");
            SpawnVisionTorchAndChair(station.gameObject, station._rootSector, new Vector3(-15.4589f, 20.7242f, -7.8966f), new Vector3(0f, 9.2435f, 0f), null);
        }

        public static void FixStationGlass()
        {
            // TODO: I may need to use the Find that works for inactive game objects
            var opcGo = GameObject.Find("OrbitalProbeCannon_Body/Sector_OrbitalProbeCannon/Geo_OrbitalProbeCannon/ControlledByProxy_OrbitalProbeCannon/Structure_NOM_OrbitalProbeCannon/OPC_Hub_Geo");
            var glassMat = opcGo.GetComponent<MeshRenderer>().sharedMaterials[3];

            var glassFloorGo = GameObject.Find("TestSunMoonSatellite_Body/Sector/Quantum Moon Sun Station(Clone)/warp_ext/Glass Floor");
            glassFloorGo.GetComponent<MeshRenderer>().sharedMaterial = glassMat;
        }

        public static void CreateSunAspectSpawnPoint()
        {
            // -16.7344 20.7243 -7.5293
            var station = AstroObjectLocator.GetAstroObject("Test Sun Moon Satellite");
            SpawnModule s = new SpawnModule()
            {
                playerSpawnPoint = new Vector3(-16.7344f, 20.7243f, -7.5293f)
            };
            stationSpawnPoint = SpawnPointBuilder.Make(station.gameObject, s, station._owRigidbody);
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
    }
}
