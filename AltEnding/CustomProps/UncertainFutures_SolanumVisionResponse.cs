using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.CustomProps
{
    class UncertainFutures_SolanumVisionResponse : MonoBehaviour
    {
        public NomaiConversationManager _nomaiConversationManager;
        public SolanumAnimController _solanumAnimController;
        public NomaiWallText solanumVisionResponse;

        private static readonly int MAX_WAIT_FRAMES = 20;

        private int waitFrames = 0;
        private bool visionEnded = false;
        private bool doneHijacking = false;
        private bool hasStartedWriting = false;

        void Update()
        {
            if (!visionEnded) return;
            if (doneHijacking) return;
            if (waitFrames > 0) { waitFrames--; return; }
                    
            if (!hasStartedWriting)
            {
                // one-time code that runs after waitFrames are up
                _solanumAnimController.OnWriteResponse += (int unused) => solanumVisionResponse.Show();
			    _solanumAnimController.StartWritingMessage();
                hasStartedWriting = true;
            }

            if (!_solanumAnimController.isStartingWrite && !solanumVisionResponse.IsAnimationPlaying())
			{
			    _solanumAnimController.StopWritingMessage(gestureToText: false);
                _solanumAnimController.StopWatchingPlayer();
                doneHijacking = true;
			}
        }

        void OnVisionEnd()
        {
            _nomaiConversationManager.enabled = false;
            waitFrames = MAX_WAIT_FRAMES;
        }
    }
    // hijacking Solanum's conversation controller:

//         // under NomaiConversationManager
    // _activeResponseText.Show();
    // nomaiConversationManager.enabled = false;
	//_solanumAnimController.StartWritingMessage();
//         // then every frame,
	//if (!_solanumAnimController.isStartingWrite && !_activeResponseText.IsAnimationPlaying())
	//{
	//	_solanumAnimController.StopWritingMessage(gestureToText: true);
    //  _solanumAnimController.StopWatchingPlayer();
	//}
}
