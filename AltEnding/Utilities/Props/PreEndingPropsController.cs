using AltEnding.CustomProps;
using NewHorizons.Builder.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static NewHorizons.External.Modules.PropModule;

namespace AltEnding.Utilities.Props
{
    public static class PreEndingPropsController
    {
        public static void DEBUG_SpawnVisionTorchAtCamp()
        {
			Vector3 _rotation = new Vector3(311.8565f,  287.9388f,  254.72f);
            var _path = "DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem";
            var p = new Vector3( 25.06051f, -42.64357f, 184.141f); 
            GameObject staff1 = DetailBuilder.MakeDetail(Locator._timberHearth.gameObject, Locator._timberHearth.GetRootSector(), _path, p, _rotation, 1, false);
        }

        public static void SpawnProps()
        {
            AltEnding.Instance.ModHelper.Console.WriteLine("LOAIDING MAIN PROPS");

            DEBUG_SpawnVisionTorchAtCamp();

            //
            // Staff signal
            // 

            if (PlayerData._currentGameSave.GetPersistentCondition("MET_PRISONER"))
            {
                SpawnStaffSignals();
            }


            //
            // Vision Staff
            //

            var path = "DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem";
            var position = new Vector3(-174.992325f,-134.213821f,-189.465027f);
            var rotation = new Vector3(2.85523677f,327.847168f,293.827332f);
            GameObject staff = DetailBuilder.MakeDetail(Locator._ringWorld.gameObject, Locator._ringWorld.GetRootSector(), path, position, rotation, 1, false); 

            //
            // Vision Target
            //
            SpawnSolanumProps();
        }

        private static void SpawnStaffSignals()
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
				    audioClip = "OW Finally Set Free 072021_2 AP", //.wav",
				    frequency = "Traveler",
				    detectionRadius = 400,
				    identificationRadius = 3,
				    sourceRadius = 0.5f,
				    name = "Memory Flame",
                    position = new Vector3(-174.0865f, -134.4167f, -189.1588f),
                    onlyAudibleToScope = true,
                    insideCloak = insideCloak
                };
            };
                
            SignalBuilder.Make(Locator._ringWorld.gameObject, interior, makeSignalInfo(false), AltEnding.Instance);
            SignalBuilder.Make(Locator._ringWorld.gameObject, interior, makeSignalInfo(true), AltEnding.Instance);
        }

        private static void SpawnSolanumProps()
        {
            // find all slides for vision1
            string[] files = System.IO.Directory.GetFiles(AltEnding.Instance.ModHelper.Manifest.ModFolderPath + "/images/vision1", "*.png");
            SlideInfo[] slides = files.Select(f => f.Remove(0, AltEnding.Instance.ModHelper.Manifest.ModFolderPath.Length)).Select(f => new SlideInfo() { imagePath=f }).ToArray();

            // slides[0].backdropAudio = "SunStation"; // "OW_NM_SunStation";
            // slides[251].backdropAudio = "SadNomaiTheme"; // "OW NM Nomai Ruins 081718 AP";

            ProjectionInfo info = new ProjectionInfo()
            {
                position=new Vector3(-5.254965f, -70.73996f, 1.607201f),
                rotation=new Vector3(0, 0, 0),
                type=ProjectionInfo.SlideShowType.VisionTorchTarget,
                slides=slides
            };

            GameObject visionTarget = ProjectionBuilder.MakeMindSlidesTarget(Locator._quantumMoon.gameObject, Locator._quantumMoon._sector, info, AltEnding.Instance);
            //GameObject.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE").transform;
            visionTarget.transform.parent = 
                GameObject.Find("QuantumMoon_Body/Sector_QuantumMoon")
                .GetComponentsInChildren<Transform>(true)
                .Where(t => t.gameObject.name == "State_EYE")
                .First(); // All because Find doesn't work on inactive game objects :/
        
        
            // make Solanum have the proper reaction after the vision ends
            //GameObject.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/NomaiConversation/ResponseStone/ArcSocket/Arc_QM_SolanumConvo_Explain+Eye").GetComponent<NomaiWallText>();
            NomaiWallText responseText = 
                Resources.FindObjectsOfTypeAll<NomaiWallText>()
                .Where(text => text.gameObject.name == "Arc_QM_SolanumConvo_Explain+Eye")
                .First();

            var nomaiConversationManager = Resources.FindObjectsOfTypeAll<NomaiConversationManager>().First(); //GameObject.FindObjectOfType<NomaiConversationManager>();
            var myConversationManager = nomaiConversationManager.gameObject.AddComponent<UncertainFutures_SolanumVisionResponse>();
            myConversationManager._nomaiConversationManager = nomaiConversationManager;
            myConversationManager._solanumAnimController = nomaiConversationManager._solanumAnimController;
            myConversationManager.solanumVisionResponse = responseText;

            visionTarget.GetComponent<VisionTorchTarget>().onSlidesComplete = myConversationManager.OnVisionEnd;
        }
        
    }
}
