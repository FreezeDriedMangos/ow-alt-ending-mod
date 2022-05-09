using OWML.Common;
using OWML.ModHelper;
using AltEnding.Utilities.ModAPIs;
using AltEnding.Utilities;

namespace AltEnding
{
    public class AltEnding : ModBehaviour
    {
        public static AltEnding Instance;

        private bool initialized = false;
        private BlinkController blinkController;
        private PropsController propsController;

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
            INewHorizons newHorizonsAPI = ModHelper.Interaction.GetModApi<INewHorizons>("xen.NewHorizons");
            // newHorizonsAPI.LoadConfigs(this);


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
                this.propsController = new PropsController();
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
                propsController.SpawnMainSystemProps();
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