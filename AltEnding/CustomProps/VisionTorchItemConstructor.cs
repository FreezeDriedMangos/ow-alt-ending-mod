using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.CustomProps
{
	public static class VisionTorchItemConstructor
    {
		public static GameObject InitializeMemoryStaff(GameObject nhConstructedVisionTorchItem)
        {
            // CODE FOR CUSTOM MEMORY STAFF ITEM:

            // SPAWNING

			// "DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Interactibles_PrisonCell/PrisonerSequence/VisionTorchWallSocket/Prefab_IP_VisionTorchItem"
            GameObject Prefab_IP_VisionTorchItem = nhConstructedVisionTorchItem;
			Prefab_IP_VisionTorchItem.GetComponent<VisionTorchItem>().enabled = true;
			Prefab_IP_VisionTorchItem.GetComponent<VisionTorchItem>().mindProjectorTrigger.enabled = true;

			Prefab_IP_VisionTorchItem.GetComponent<VisionTorchItem>().mindSlideProjector._mindProjectorImageEffect = GameObject.Find("Player_Body/PlayerCamera").GetComponent<MindProjectorImageEffect>();
			// TODO: MindSlideProjector._mindProjectorImageEffect is null
			// it should be Player_Body/PlayerCamera.GetComponent<MindProjectorImageEffect>()

            // SETTING THE MUSIC

			// TODO: this is not right. one of the mind slide components tries to start music itself
            // Prefab_IP_VisionTorchItem.GetComponent<VisionTorchItem>().mindSlideProjector.OnProjectionStart += () => playMusic()
            // Prefab_IP_VisionTorchItem.GetComponent<VisionTorchItem>().mindSlideProjector.OnProjectionEnd += () => stopMusic()
            // Prefab_IP_VisionTorchItem.GetComponent<VisionTorchItem>().mindSlideProjector.OnProjectionComplete += () => I don't know what the difference is between this and the last one()

            // SETTING THE SLIDES

            SlideCollectionContainer slides = Prefab_IP_VisionTorchItem.GetComponent<VisionTorchItem>().mindSlideProjector.mindSlideCollection.slideCollectionContainer;
            slides.ClearSlides();

            // for loop
                // Slide s = new Slide(){ _image = someTexture2D; };
                // slides.AddSlide(s);
                // UnityEngine.Texture2D slideZeroTexture;
                // slides.SetTextureAt(0, slideZeroTexture);

            // SETTING THE TRIGGER TARGET

            // TODO: check the code for MindProjectorTrigger.OnTriggerVolumeEntry to look for specific references to the Prisoner
            // Prefab_IP_VisionTorchItem.GetComponent<VisionTorchItem>().mindProjectorTrigger.OnTriggerVolumeEntry() // this method is called by the base game I believe whenever the player points it at something. when that something is the prisoner's head, it plays the memory slides

			return Prefab_IP_VisionTorchItem;
        }
    }
}

// From MindSlideProjector
// private void NextSlide()
// {
//  	_slideCollectionItem.IncreaseSlideIndex();
//  	_slideCollectionItem.SetCurrentRead();
//  	_slideCollectionItem.TryPlayMusicForCurrentSlideTransition(forward: true);
// }




        /*
        // from the MindProjectorTrigger class
        
	private void OnTriggerVolumeEntry(GameObject hitObj)
	{
		if (hitObj.CompareTag("PlayerCameraDetector"))
		{
			OnBeamStartHitPlayer.Invoke();
			_mindProjector.Play(reset: true);
			_mindProjector.OnProjectionStart += new OWEvent.OWCallback(OnProjectionStart);
			_mindProjector.OnProjectionComplete += new OWEvent.OWCallback(OnProjectionComplete);
			if (_triggerConeShape != null)
			{
				_triggerConeShape.topRadius = _triggerConeDimensions.x + 0.15f;
				_triggerConeShape.bottomRadius = _triggerConeDimensions.y + 0.15f;
				_triggerConeShape.height = _triggerConeDimensions.z + 0.3f;
			}
			Locator.GetPlayerTransform().GetComponent<PlayerLockOnTargeting>().LockOn(_lockOnTransform, Vector3.zero);
			_playerLockedOn = true;
			if (Locator.GetToolModeSwapper().GetToolMode() == ToolMode.SignalScope && Locator.GetToolModeSwapper().GetSignalScope().InZoomMode())
			{
				Locator.GetToolModeSwapper().UnequipTool();
			}
			OWInput.ChangeInputMode(InputMode.None);
		}
		else if (hitObj.CompareTag("PrisonerDetector"))
		{
			OnBeamStartHitPrisoner.Invoke();
			_mindProjector.Play(reset: true);
			_mindProjector.OnProjectionStart += new OWEvent.OWCallback(OnProjectionStart);
			_mindProjector.OnProjectionComplete += new OWEvent.OWCallback(OnProjectionComplete);
			Locator.GetPlayerTransform().GetComponent<PlayerLockOnTargeting>().LockOn(hitObj.transform, Vector3.zero);
			_playerLockedOn = true;
		}
	}

	private void OnTriggerVolumeExit(GameObject hitObj)
	{
		if (hitObj.CompareTag("PlayerCameraDetector") || hitObj.CompareTag("PrisonerDetector"))
		{
			if (hitObj.CompareTag("PlayerCameraDetector"))
			{
				OnBeamStopHitPlayer.Invoke();
			}
			else
			{
				OnBeamStopHitPrisoner.Invoke();
			}
			_mindProjector.Stop();
			_mindProjector.OnProjectionStart -= new OWEvent.OWCallback(OnProjectionStart);
			_mindProjector.OnProjectionComplete -= new OWEvent.OWCallback(OnProjectionComplete);
			if (OWInput.IsInputMode(InputMode.NomaiRemoteCam | InputMode.None))
			{
				OWInput.ChangeInputMode(InputMode.Character);
			}
			if (_playerLockedOn)
			{
				_playerLockedOn = false;
				Locator.GetPlayerTransform().GetComponent<PlayerLockOnTargeting>().BreakLock();
			}
			if (_triggerConeShape != null)
			{
				_triggerConeShape.topRadius = _triggerConeDimensions.x;
				_triggerConeShape.bottomRadius = _triggerConeDimensions.y;
				_triggerConeShape.height = _triggerConeDimensions.z;
			}
		}
	}

        */
