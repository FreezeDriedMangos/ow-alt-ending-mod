using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltEnding.CustomProps
{
    public class ElipsoidFluidVolume : FluidVolume
	{
		[SerializeField]
		public float _buoyancyDensity = 1f;
		
		[SerializeField]
		public float _baseRadius; // the radius if the game object's scale was <1, 1, 1>
		

		protected virtual void OnValidate()
		{
			SphereCollider component = GetComponent<SphereCollider>();
			if (component != null)
			{
				component.radius = _baseRadius;
			}
		}

		public override Vector3 GetBuoyancy(FluidDetector detector, float fractionSubmerged)
		{
			if (detector.GetAttachedOWRigidbody().GetAttachedForceDetector() != null)
			{
				Vector3 relativeAcceleration = detector.GetAttachedOWRigidbody().GetAttachedForceDetector().GetForceAcceleration() - _attachedBody.GetAttachedForceDetector().GetForceAcceleration();
				return Vector3.Project(onNormal: detector.transform.position - base.transform.position, vector: -relativeAcceleration) * fractionSubmerged * _buoyancyDensity / detector.GetBuoyancyData().density;
			}
			return Vector3.zero;
		}

		public override bool IsSpherical()
		{
			return true; // lol, apparently this is the correct return value
		}

		public override float GetFractionSubmerged(FluidDetector detector)
		{
			// InverseTransformPoint accounts for scale :D
			Vector3 detectorRelativeLocation = transform.InverseTransformPoint(detector.transform.position);
			return detector.GetBuoyancyData().CalculateSubmergedFraction(detectorRelativeLocation.magnitude, _baseRadius);
		}

		protected virtual void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(base.transform.position, _baseRadius); // I don't know if this will work :P
		}
	}
}
