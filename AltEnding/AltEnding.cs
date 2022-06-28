using OWML.Common;
using OWML.ModHelper;
using AltEnding.Utilities.ModAPIs;
using AltEnding.Utilities;
using HarmonyLib;
using System.Reflection;
using AltEnding.CustomProps;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using AltEnding.Utilities.Props;
using System;

namespace AltEnding
{
    // looking at the game's source: 1) open ilspy 2) open OuterWilds_Data/Managed/Assembly-CSharp.dll 3) open the "{} -" dropdown



    // TODO: if prisoner has been met, spawn a signal source here: Log : Raycast hit "position": {"x": -174.0865, "y": -134.4167, "z": -189.1588}, "normal": {"x": -0.2941437, "y": 0.9556881, "z": -0.01182878} on [RingWorld_Body] at [RingWorld_Body]
    // this is the location of the hidden memory staff
    // make sure to disable this signal source once the staff has been picked up


    public class AltEnding : ModBehaviour
    {
        public static AltEnding Instance;

        private bool initialized = false;
        private BlinkController blinkController;


        public static INewHorizons newHorizonsAPI;


        private void Awake()
        {
            // You won't be able to access OWML's mod helper in Awake.
            // So you probably don't want to do anything here.
            // Use Start() instead.
            Instance = this;
        }

        // EXAMPLE: https://github.com/Bwc9876/OW-Amogus
        // ANOTHER EXAMPLE: https://github.com/xen-42/outer-wilds-signals-plus/tree/main/planets
        // 

        // BLINK ANIMATION:
        // https://discord.com/channels/929708786027999262/929787137895854100/973267691827769404
        // https://github.com/Vesper-Works/AutoResume/blob/master/ModTemplate/MainBehaviour.cs

        private void Update()
        {
            //if (Keyboard.current[Key.O].wasReleasedThisFrame)
            //{
            //    TimeLoop.SetSecondsRemaining(30);
            //}
            //if (Keyboard.current[Key.L].wasReleasedThisFrame)
            //{
            //    TimeLoop.SetSecondsRemaining(120);
            //}

            if (Keyboard.current[Key.O].wasReleasedThisFrame)
            {
                //_spawner.DebugWarp(_spawner.GetSpawnPoint(SpawnLocation.Comet));
                var spawner = UnityEngine.GameObject.Find("Player_Body").GetComponent<PlayerSpawner>();
                spawner.DebugWarp(InEndingPropsController.stationSpawnPoint);        
            }
        }

        private void Start()
        {
            // =======
            // Make Required Patches
            // =======
            
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

            // =======
            // Initialize New Horizons
            // =======

            newHorizonsAPI = ModHelper.Interaction.GetModApi<INewHorizons>("xen.NewHorizons");
            newHorizonsAPI.LoadConfigs(this);


            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"My mod {nameof(AltEnding)} is loaded!", MessageType.Success);

            newHorizonsAPI.GetChangeStarSystemEvent().AddListener(OnStarSystemChange); 
            newHorizonsAPI.GetStarSystemLoadedEvent().AddListener(OnStarSystemLoaded);

            // Example of accessing game code.
            LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
            {
                // TODO: add a case for OWScene.EyeOfTheUniverse to make the loading zone that teleports you to  clay.AltEnding.PostEndingSolarSystem->timeloopring

                if (loadScene != OWScene.SolarSystem) return;
                var playerBody = FindObjectOfType<PlayerBody>();
                ModHelper.Console.WriteLine($"Found player body, and it's called {playerBody.name}!",
                    MessageType.Success);

                if (initialized) return;
                initialized = true;
                this.blinkController = new BlinkController(FindObjectOfType<PlayerCameraEffectController>());


                //// TESTING AppearingQuantumObject
                //var campsite = UnityEngine.GameObject.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp/Props_StartingCamp/OtherComponentsGroup");
                //List<AppearingQuantumObject> aqos = new List<AppearingQuantumObject>();
            
                //foreach(UnityEngine.Transform prop in campsite.transform)
                //{
                //    aqos.Add(prop.gameObject.AddComponent<AppearingQuantumObject>());
                //}

                //for (int i = 0; i < aqos.Count; i++)
                //{
                //    for (int j = i+1; j < aqos.Count; j++)
                //    {
                //        aqos[i].AddEntangledObject(aqos[j]);
                //    }
                //    aqos[i].IsPresenting = false;
                //}
                //aqos[0].IsPresenting = true;
            };
        }

        // TODO: make a PropsManager to handle all of the below

        public void OnStarSystemChange(string systemName)
        {
            //TODO: on this event, destroy any manual props I created
        }

        public void OnStarSystemLoaded(string systemName)
        {
            //TODO: on this event, check the system name and spawn any manual props (eg, the memory staves)
            ModHelper.Console.WriteLine("LOADED SYSTEM " + systemName);

            PropsController.SpawnProps(systemName);

            if (systemName == "SolarSystem")
            {
                ModHelper.Console.WriteLine("Loading main props!");
            }
            else if (systemName == "clay.AltEnding.PostEndingSolarSystem")
            {
                // TODO: teleport the player to the atp, set spawn point to quantum moon south pole, enable quantum moon eye aspect
                // TODO: delete atp lights
            }
        }

        public static void WriteLine(string s)
        {
            Instance.ModHelper.Console.WriteLine(s);
        }

        public static void FireOnNextUpdate(Action action)
        {
            Instance.ModHelper.Events.Unity.FireOnNextUpdate(action);
        }
    }
}