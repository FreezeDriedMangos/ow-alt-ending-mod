using System;
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
                
                // newHorizonsAPI.SpawnObject(Locator._timberHearth.gameObject, Locator._timberHearth.GetRootSector(), path, position, rotation, 1, false);

                path = "DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem";
                //Sector s = GameObject.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp").GetComponent<Sector>();
                position = new Vector3( 25.06051f, -42.64357f, 184.141f); 
                GameObject staff1 = newHorizonsAPI.SpawnObject(Locator._timberHearth.gameObject, Locator._timberHearth.GetRootSector(), path, position, rotation, 1, false);
                VisionTorchItemConstructor.InitializeMemoryStaff(staff1);

                // spawn a trigger for the vision torch
                path = "DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Ghosts_PrisonCell/GhostNodeMap_PrisonCell_Lower/Prefab_IP_GhostBird_Prisoner/Ghostbird_IP_ANIM/Ghostbird_Skin_01:Ghostbird_Rig_V01:Base/Ghostbird_Skin_01:Ghostbird_Rig_V01:Root/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine01/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine02/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine03/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine04/Ghostbird_Skin_01:Ghostbird_Rig_V01:Neck01/Ghostbird_Skin_01:Ghostbird_Rig_V01:Neck02/Ghostbird_Skin_01:Ghostbird_Rig_V01:Head/PrisonerHeadDetector";
                position = new Vector3(17.30891f, -41.28941f, 187.1373f);
                GameObject g = newHorizonsAPI.SpawnObject(Locator._timberHearth.gameObject, Locator._timberHearth.GetRootSector(), path, position, rotation, 1, false);
                g.name = "VisionStaffDetector";
                VisionTorchTarget t = g.AddComponent<VisionTorchTarget>();
                SlideCollectionContainer c = g.AddComponent<SlideCollectionContainer>();
                t.slideCollection = new MindSlideCollection();
                t.slideCollection._slideCollectionContainer = c;

                SlideInfo[] slides = new SlideInfo[0];
                var slidesCount = slides.Length >= 15 ? 15 : slides.Length
                t.slideCollection._slideCollectionContainer.slideCollection = new SlideCollection(slidesCount);
                var slideCollection = t.slideCollection._slideCollectionContainer.slideCollection;
                // The base game ones only have 15 slides max
                var textures = new Texture2D[slidesCount];

                for (int i = 0; i < slidesCount; i++)
                {
                    var slide = new Slide();
                    var slideInfo = slides[i];

                    var texture = ImageUtilities.GetTexture(AltEnding.Instance, slideInfo.imagePath);
                    slide.textureOverride = ImageUtilities.Invert(texture);

                    // Track the first 15 to put on the slide reel object
                    if (i < 15) textures[i] = texture;

                    AddModules(slideInfo, ref slide);

                    slideCollection.slides[i] = slide;
                }

                // Else when you put them down you can't pick them back up
                slideReelObj.GetComponent<OWCollider>()._physicsRemoved = false;

                slideCollectionContainer.slideCollection = slideCollection;

                // Idk why but it wants reveals to be comma delimited not a list
                if (info.reveals != null) slideCollectionContainer._shipLogOnComplete = string.Join(",", info.reveals);

                

                //g.tag = "SolanumVisionStaffTarget";

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

            if (PlayerData._currentGameSave.GetPersistentCondition("MET_PRISONER"))
            {
                // public static void Make(GameObject planetGO, Sector sector, SignalModule.SignalInfo info, IModBehaviour mod)
                Sector interior = null;
                foreach (Transform s in Locator._ringWorld.gameObject.transform)
                {
                    if (s.gameObject.name == "Sector_RingInterior")
                    {
                        interior = s.GetComponent<Sector>();
                        break;
                    }
                }

                Func<bool, NewHorizons.External.Modules.SignalModule.SignalInfo> makeSignalInfo = (insideCloak) => {
                    return  new NewHorizons.External.Modules.SignalModule.SignalInfo(){ 
				        AudioClip = "OW Finally Set Free 072021_2 AP", //.wav",
				        Frequency = "Traveler",
				        DetectionRadius = 400,
				        IdentificationRadius = 10,
				        SourceRadius = 0.5f,
				        Name = "Vision Torch",
                        Position = new Vector3(-174.0865f, -134.4167f, -189.1588f),
                        OnlyAudibleToScope = true,
                        InsideCloak = insideCloak
                    };
                };
                
                SignalBuilder.Make(Locator._ringWorld.gameObject, interior, makeSignalInfo(false), AltEnding.Instance);;
                SignalBuilder.Make(Locator._ringWorld.gameObject, interior, makeSignalInfo(true), AltEnding.Instance);

                
            }

            path = "DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem";
            position = new Vector3(-174.992325f,-134.213821f,-189.465027f);
            rotation = new Vector3(2.85523677f,327.847168f,293.827332f);
            GameObject staff = newHorizonsAPI.SpawnObject(Locator._ringWorld.gameObject, Locator._ringWorld.GetRootSector(), path, position, rotation, 1, false);
            VisionTorchItemConstructor.InitializeMemoryStaff(staff);

            // config file isn't working for some reason, so I'll just spawn the chair and table manually
            path = "RingWorld_Body/Sector_RingInterior/Sector_Zone1/Sector_ProjectionHouse_Zone1/SmallProps_ProjectionHouse_Zone1/OtherComponentsGroup/Props_IP_Stool";
            position = new Vector3( -174.408813f, -132.208527f, -190.9654f );
            rotation = new Vector3( 29.48426f, 306.406647f, 270.6115f );
            newHorizonsAPI.SpawnObject(Locator._ringWorld.gameObject, Locator._ringWorld.GetRootSector(), path, position, rotation, 1, false);
					 
            path = "RingWorld_Body/Sector_RingInterior/Sector_Zone1/Sector_ProjectionHouse_Zone1/Props_ProjectionHouse_Zone1/OtherComponentsGroup/Prefab_IP_Chair/";
            position = new Vector3( -175.350372f, -133.245285f, -190.25087f );
            rotation = new Vector3( 336.052f, 306.467468f, 270.643555f );
			newHorizonsAPI.SpawnObject(Locator._ringWorld.gameObject, Locator._ringWorld.GetRootSector(), path, position, rotation, 1, false);
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
