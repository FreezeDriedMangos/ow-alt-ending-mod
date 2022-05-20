using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AltEnding.Utilities.ModAPIs;
using AltEnding.CustomProps;

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
            AltEnding.Instance.ModHelper.Console.WriteLine("LOAIDING MAIN PROPS");
            UnityEngine.Debug.Log($"LOADING MAIN PROPSS");

            // TESTING

                // TODO: wrap all SpawnObject calls
                string path = "QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/Nomai_ANIM_SkyWatching_Idle";
				Vector3 position = new Vector3( 18.06051f, -50.64357f, 183.141f); 
				Vector3 rotation = new Vector3(311.8565f,  287.9388f,  254.72f);
                
                newHorizonsAPI.SpawnObject(Locator._timberHearth.gameObject, Locator._timberHearth.GetRootSector(), path, position, rotation, 1, false);

                // spawn a trigger for the vision torch
                path = "DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Ghosts_PrisonCell/GhostNodeMap_PrisonCell_Lower/Prefab_IP_GhostBird_Prisoner/Ghostbird_IP_ANIM/Ghostbird_Skin_01:Ghostbird_Rig_V01:Base/Ghostbird_Skin_01:Ghostbird_Rig_V01:Root/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine01/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine02/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine03/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine04/Ghostbird_Skin_01:Ghostbird_Rig_V01:Neck01/Ghostbird_Skin_01:Ghostbird_Rig_V01:Neck02/Ghostbird_Skin_01:Ghostbird_Rig_V01:Head/PrisonerHeadDetector";
                position = new Vector3(17.30891f, -41.28941f, 187.1373f);
                GameObject g = newHorizonsAPI.SpawnObject(Locator._timberHearth.gameObject, Locator._timberHearth.GetRootSector(), path, position, rotation, 1, false);
                g.name = "VisionStaffDetector";
                g.tag = "SolanumVisionStaffTarget";

                // TODO: try changing the tag on the above, and then passing that tag to InitializeMemoryStaff to see if that works

            // TODO: spawn these items for testing
            // RingWorld_Body/Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Experiment_3/VisionTorchApparatus
            // DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Interactibles_Underground/Prefab_IP_VisionTorchProjector
            // DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem

            // TODO: spawn this item for actual use, and put it in the sun watching room of the stranger (the top floor of the room accessed via the dam)
            // DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem
            // TODO: modify the above item for actual use purposes

            //Sector thSector = Locator._timberHearth.GetRootSector();
            //GameObject prefab = GameObject.Find("RingWorld_Body/Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Experiment_3/VisionTorchApparatus");
            //GameObject memoryStaff = GameObject.Instantiate(prefab, thSector.transform);


            // path = "RingWorld_Body/Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Experiment_3/VisionTorchApparatus";
            path = "DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem";
            //Sector s = GameObject.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp").GetComponent<Sector>();
            position = new Vector3( 25.06051f, -42.64357f, 184.141f); 
            GameObject staff = newHorizonsAPI.SpawnObject(Locator._timberHearth.gameObject, Locator._timberHearth.GetRootSector(), path, position, rotation, 1, false);
            VisionTorchItemConstructor.InitializeMemoryStaff(staff, "SolanumVisionStaffTarget");

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
            // \/\/ ?Log : Raycast hit "position": {"x": (-?[\d\.]+), "y": (-?[\d\.]+), "z": (-?[\d\.]+)}, "normal": {"x": (-?[\d\.]+), "y": (-?[\d\.]+), "z": (-?[\d\.]+)}.*\n( *)\/\/(.*)
            // Vector3 $8_position = new Vector3($1f, $2f, $3f);\n$7Vector3 $8_normal = new Vector3($4f, $5f, $6f);


            //Log : Raycast hit "position": {"x": 6.812032, "y": -68.9463, "z": 20.49807}, "normal": {"x": -0.1672245, "y": -0.9407958, "z": 0.2948547} on [QuantumMoon_Body] at [QuantumMoon_Body]
            //New Horizons	// Hill center
            //Log : Raycast hit "position": {"x": 5.330701, "y": -68.6851, "z": 20.31562}, "normal": {"x": -0.2203388, "y": -0.928145, "z": 0.2999961} on [QuantumMoon_Body] at [QuantumMoon_Body]
            //New Horizons	// Hill towards path
            //Log : Raycast hit "position": {"x": 6.322139, "y": -68.59979, "z": 22.01736}, "normal": {"x": -0.1490866, "y": -0.946611, "z": 0.2858335} on [QuantumMoon_Body] at [QuantumMoon_Body]
            //New Horizons // Hill right (away from south pole)

            //Log : Raycast hit "position": {"x": -13.821, "y": -66.59717, "z": 21.76712}, "normal": {"x": 0.2873927, "y": -0.8835878, "z": 0.3697002} on [QuantumMoon_Body] at [QuantumMoon_Body]
            //New Horizons	// alcove
            //Log : Raycast hit "position": {"x": -12.66925, "y": -66.53086, "z": 21.61268}, "normal": {"x": -0.07132879, "y": -0.9360703, "z": 0.3445062} on [QuantumMoon_Body] at [QuantumMoon_Body]
            //New Horizons	// alcove towards path 
            //Log : Raycast hit "position": {"x": -13.7245, "y": -65.93216, "z": 23.23672}, "normal": {"x": 0.1729326, "y": -0.8740184, "z": 0.4540775} on [QuantumMoon_Body] at [QuantumMoon_Body]
            //New Horizons // alcove left (away from south pole)

            // Log : Raycast hit "position": {"x": 1.923516, "y": -68.64253, "z": 8.23922}, "normal": {"x": 0.01408709, "y": -0.9995713, "z": 0.02566594} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // near solanum

            // Log : Raycast hit "position": {"x": -13.6357, "y": -68.2438, "z": 7.320133}, "normal": {"x": -0.09635226, "y": -0.9523352, "z": -0.2894375} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // under overhang near solanum


            // Log : Raycast hit "position": {"x": 10.31877, "y": -68.32432, "z": 4.660011}, "normal": {"x": 0.06111236, "y": -0.9976529, "z": -0.0308916} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // large open area behind solanum
            // Log : Raycast hit "position": {"x": 9.192107, "y": -68.39394, "z": 4.963161}, "normal": {"x": 0.05134231, "y": -0.9981113, "z": -0.03373128} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // towards solanum
            // Log : Raycast hit "position": {"x": 10.07008, "y": -68.43001, "z": 3.023415}, "normal": {"x": -0.08432821, "y": -0.9922035, "z": 0.09176683} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // further behind solanum

            // Log : Raycast hit "position": {"x": 2.862122, "y": -59.65821, "z": 38.2527}, "normal": {"x": -0.1388552, "y": -0.8784255, "z": 0.4572614} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // beneath overhang, farther up the path

            // Log : Raycast hit "position": {"x": -14.02003, "y": -53.73377, "z": 44.19434}, "normal": {"x": 0.1887708, "y": -0.8219363, "z": 0.5373886} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // larger area further up path


            // Log : Raycast hit "position": {"x": 9.531834, "y": -2.904995, "z": 67.92155}, "normal": {"x": -0.01273704, "y": 0.0465793, "z": 0.9988335} on [QuantumMoon_Body] at [QuantumMoon_Body]
            //New Horizons	// large area far up path
            //Log : Raycast hit "position": {"x": 21.65301, "y": 16.90595, "z": 66.1318}, "normal": {"x": -0.07133868, "y": 0.3013518, "z": 0.9508407} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // hidden area up path

            //Log : Raycast hit "position": {"x": 13.49709, "y": 33.67895, "z": 60.69045}, "normal": {"x": 0.09631497, "y": 0.436612, "z": 0.8944795} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // another large area

            // Log : Raycast hit "position": {"x": -18.7557, "y": 25.15811, "z": 61.21137}, "normal": {"x": -0.4395696, "y": 0.364402, "z": 0.8209689} on [QuantumMoon_Body] at [QuantumMoon_Body]
            // large area on other side

            

            Vector3 Hill_center_position = new Vector3(6.812032f, -68.9463f, 20.49807f);
            Vector3 Hill_center_normal = new Vector3(-0.1672245f, -0.9407958f, 0.2948547f);
            Vector3 Hill_towards_path_position = new Vector3(5.330701f, -68.6851f, 20.31562f);
            Vector3 Hill_towards_path_normal = new Vector3(-0.2203388f, -0.928145f, 0.2999961f);
            Vector3 Hill_right__away_from_south_pole__position = new Vector3(6.322139f, -68.59979f, 22.01736f);
            Vector3 Hill_right__away_from_south_pole__normal = new Vector3(-0.1490866f, -0.946611f, 0.2858335f);

            Vector3 alcove_position = new Vector3(-13.821f, -66.59717f, 21.76712f);
            Vector3 alcove_normal = new Vector3(0.2873927f, -0.8835878f, 0.3697002f);
            Vector3 alcove_towards_path__position = new Vector3(-12.66925f, -66.53086f, 21.61268f);
            Vector3 alcove_towards_path__normal = new Vector3(-0.07132879f, -0.9360703f, 0.3445062f);
            Vector3 alcove_left__away_from_south_pole__position = new Vector3(-13.7245f, -65.93216f, 23.23672f);
            Vector3 alcove_left__away_from_south_pole__normal = new Vector3(0.1729326f, -0.8740184f, 0.4540775f);

            Vector3 near_solanum_position = new Vector3(1.923516f, -68.64253f, 8.23922f);
            Vector3 near_solanum_normal = new Vector3(0.01408709f, -0.9995713f, 0.02566594f);

            Vector3 under_overhang_near_solanum_position = new Vector3(-13.6357f, -68.2438f, 7.320133f);
            Vector3 under_overhang_near_solanum_normal = new Vector3(-0.09635226f, -0.9523352f, -0.2894375f);


            Vector3 large_open_area_behind_solanum_position = new Vector3(10.31877f, -68.32432f, 4.660011f);
            Vector3 large_open_area_behind_solanum_normal = new Vector3(0.06111236f, -0.9976529f, -0.0308916f);
            Vector3 towards_solanum_position = new Vector3(9.192107f, -68.39394f, 4.963161f);
            Vector3 towards_solanum_normal = new Vector3(0.05134231f, -0.9981113f, -0.03373128f);
            Vector3 further_behind_solanum_position = new Vector3(10.07008f, -68.43001f, 3.023415f);
            Vector3 further_behind_solanum_normal = new Vector3(-0.08432821f, -0.9922035f, 0.09176683f);

            Vector3 beneath_overhang__farther_up_the_path_position = new Vector3(2.862122f, -59.65821f, 38.2527f);
            Vector3 beneath_overhang__farther_up_the_path_normal = new Vector3(-0.1388552f, -0.8784255f, 0.4572614f);

            Vector3 larger_area_further_up_path_position = new Vector3(-14.02003f, -53.73377f, 44.19434f);
            Vector3 larger_area_further_up_path_normal = new Vector3(0.1887708f, -0.8219363f, 0.5373886f);


            Vector3 large_area_far_up_path_position = new Vector3(9.531834f, -2.904995f, 67.92155f);
            Vector3 large_area_far_up_path_normal = new Vector3(-0.01273704f, 0.0465793f, 0.9988335f);
            Vector3 hidden_area_up_path_position = new Vector3(21.65301f, 16.90595f, 66.1318f);
            Vector3 hidden_area_up_path_normal = new Vector3(-0.07133868f, 0.3013518f, 0.9508407f);

            Vector3 another_large_area_position = new Vector3(13.49709f, 33.67895f, 60.69045f);
            Vector3 another_large_area_normal = new Vector3(0.09631497f, 0.436612f, 0.8944795f);

            Vector3 large_area_on_other_side_position = new Vector3(-18.7557f, 25.15811f, 61.21137f);
            Vector3 large_area_on_other_side_normal = new Vector3(-0.4395696f, 0.364402f, 0.8209689f);


        }

        public static string GetNomaiPropPath(string name)
        {
            switch(name)
            {
                case "toys":      return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_SchoolBuilding/Prefab_NOM_Toys";
                case "chair":     return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_SchoolBuilding/Prefab_NOM_SimpleChair_NoSkeleton";
                case "grater":    return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_Building_11/Prefab_NOM_Grater";
                case "tv set":    return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_Building_13/Prefab_NOM_HoloTV";
                case "container": return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_Building_11/Prefab_NOM_Container";
                case "vaseThin":  return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_Building_11/Prefab_NOM_VaseThin";
                case "tree pot":  return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_Building_11/Prefab_NOM_Treepot";
                case "bowl":      return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_Building_10/Prefab_NOM_SmallBowl";
                case "vaseThick": return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_Building_10/Prefab_NOM_VaseThick";
                case "box":       return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_SchoolBuilding/Prefab_NOM_Box (10)";
                case "staff":     return "BrittleHollow_Body/Sector_BH/Sector_NorthHemisphere/Sector_NorthPole/Sector_HangingCity/Sector_HangingCity_District1/Props_HangingCity_District1/OtherComponentsGroup/Props_HangingCity_SchoolBuilding/Prefab_NOM_Staff (1)";
            }

            return null;
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
