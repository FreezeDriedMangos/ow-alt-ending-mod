using OWML.Common;
using OWML.ModHelper;
using AltEnding.Utilities.ModAPIs;

namespace AltEnding
{
    public class AltEnding : ModBehaviour
    {
        public static AltEnding Instance;

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

        private void Start()
        {
            INewHorizons newHorizonsAPI = ModHelper.Interaction.GetModApi<INewHorizons>("xen.NewHorizons");
            // newHorizonsAPI.LoadConfigs(this);


            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"My mod {nameof(AltEnding)} is loaded!", MessageType.Success);

            newHorizonsAPI.GetChangeStarSystemEvent().AddListener(OnStarSystemChange) 
            newHorizonsAPI.GetStarSystemLoadedEvent().AddListener(OnStarSystemLoaded) 

            // Example of accessing game code.
            LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
            {
                if (loadScene != OWScene.SolarSystem) return;
                var playerBody = FindObjectOfType<PlayerBody>();
                ModHelper.Console.WriteLine($"Found player body, and it's called {playerBody.name}!",
                    MessageType.Success);
            };
        }

        public void OnStarSystemChange(string systemName)
        {
            //TODO: on this event, destroy any manual props I created
        }

        public void OnStarSystemLoaded(string systemName)
        {
            //TODO: on this event, check the system name and spawn any manual props (eg, the memory staves)
            Debug.Log('LOADED SYSTEM ' + systemName);

            // GameObject memoryStaff = GameObject.Instantiate("RingWorld_Body/Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Experiment_3/VisionTorchApparatus")
        }
    }
}