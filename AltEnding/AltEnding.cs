using OWML.Common;
using OWML.ModHelper;
using AltEnding.Utilities.ModAPIs;
using AltEnding.Utilities;
using HarmonyLib;
using System.Reflection;
using AltEnding.CustomProps;
using System.Collections.Generic;

namespace AltEnding
{
    // looking at the game's source: 1) open ilspy 2) open OuterWilds_Data/Managed/Assembly-CSharp.dll 3) open the "{} -" dropdown

    public class AltEnding : ModBehaviour
    {
        public static AltEnding Instance;

        private bool initialized = false;
        private BlinkController blinkController;
        private PropsController propsController;


        public static INewHorizons newHorizonsAPI;

        delegate void Printer(string s, MessageType t);
        private static Printer printer;

        public static void PrintToModConsole(string s)
        {
            if (printer == null) return;

            printer(s, MessageType.Debug);
        }



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
            // newHorizonsAPI.LoadConfigs(this); // TODO: uncomment this


            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"My mod {nameof(AltEnding)} is loaded!", MessageType.Success);
            printer = ModHelper.Console.WriteLine;


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
                this.propsController = new PropsController();

                
                // TESTING AppearingQuantumObject
                var campsite = UnityEngine.GameObject.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp/Props_StartingCamp/OtherComponentsGroup");
                List<AppearingQuantumObject> aqos = new List<AppearingQuantumObject>();
            
                foreach(UnityEngine.Transform prop in campsite.transform)
                {
                    aqos.Add(prop.gameObject.AddComponent<AppearingQuantumObject>());
                }

                for (int i = 0; i < aqos.Count; i++)
                {
                    for (int j = i+1; j < aqos.Count; j++)
                    {
                        aqos[i].AddEntangledObject(aqos[j]);
                    }
                    aqos[i].IsPresenting = false;
                }
                aqos[0].IsPresenting = true;
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

            if (systemName == "SolarSystem")
            {
                ModHelper.Console.WriteLine("Loading main props!");
                propsController.SpawnMainSystemProps(newHorizonsAPI);
            }
            else if (systemName == "clay.AltEnding.PostEndingSolarSystem")
            {
                // TODO: teleport the player to the atp
                // TODO: delete atp lights

                propsController.SpawnEndingProps();
            }
        }
    }
}