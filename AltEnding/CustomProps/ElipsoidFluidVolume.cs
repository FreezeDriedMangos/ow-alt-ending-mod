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
			//Vector3 detectorRelativeLocation = transform.InverseTransformPoint(detector.transform.position);
			//// todo: scale by localScale
			
			//// https://en.wikipedia.org/wiki/Ellipsoid#Parameterization
			//var a = transform.localScale.x * _baseRadius;
			//var b = transform.localScale.y * _baseRadius;
			//var c = transform.localScale.z * _baseRadius;

			//// TODO: test this surfaceXYZ calculation by placing debug spheres at the surface points given by random vectors
			//// eg calculate surfaceXYZ for a bunch of random detectorRelativeLocations

			//var elevation = detectorRelativeLocation.magnitude; // theta and phi calculation blatently stolen from xen's tornado calculations
			
			//var theta = Mathf.Atan2(detectorRelativeLocation.z, detectorRelativeLocation.x); //theta (around the polar axis)
			//var phi   = Mathf.Acos(detectorRelativeLocation.y / elevation); //phi (down from the polar axis)
			
			//var surfaceX = a*Mathf.Sin(theta)*Mathf.Cos(phi);
			//var surfaceY = b*Mathf.Sin(theta)*Mathf.Sin(phi);
			//var surfaceZ = c*Mathf.Cos(theta);
			
			//var radiusAtLocation = Mathf.Sqrt(surfaceX*surfaceX + surfaceY*surfaceY + surfaceZ*surfaceZ);

			//return detector.GetBuoyancyData().CalculateSubmergedFraction(elevation, radiusAtLocation);


			// InverseTransformPoint accounts for scale :D
			Vector3 detectorRelativeLocation = transform.InverseTransformPoint(detector.transform.position);
			var sub = detector.GetBuoyancyData().CalculateSubmergedFraction(detectorRelativeLocation.magnitude, _baseRadius);
			
			AltEnding.Instance.ModHelper.Console.WriteLine("Submerged: " + sub + ", " + detectorRelativeLocation.magnitude);

			return sub;
		}

		protected virtual void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(base.transform.position, _baseRadius); // I don't know if this will work :P
		}
	}
}
