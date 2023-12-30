using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class CosmicObject : MonoBehaviour, FlyTarget
    {
		[SerializeField]
		private Transform _landingPointTransform;
		public Vector2 LandingPoint => _landingPointTransform.position;
		public Vector2 Position2D => transform.position;

		public float DistanceTo(Vector2 position)
		{
			return (position - LandingPoint).magnitude;
		}
		
		public float DistanceTo(CosmicObject cosmicObject)
		{
			return (cosmicObject.LandingPoint - LandingPoint).magnitude;
		}
		
		public float DistanceTo(FlyTarget flyTarget)
		{
			return (flyTarget.LandingPoint - LandingPoint).magnitude;
		}

		public bool IsInRange(Vector2 position, float range)
		{
			return DistanceTo(position) <= range;
		}
    }
}
