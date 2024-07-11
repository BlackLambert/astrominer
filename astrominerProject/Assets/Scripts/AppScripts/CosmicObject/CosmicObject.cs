using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class CosmicObject : MonoBehaviour, FlyTarget
    {

	    [SerializeField]
		private Transform _landingPointTransform;

		public abstract int Id { get; }
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

		public virtual bool IsValidFlightTargetFor(Ship ship)
		{
			return !ReferenceEquals(ship.Location.Value, this);
		}

		public virtual bool IsAllowedFlightTargetFor(Player player)
		{
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((CosmicObject)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(base.GetHashCode(), Id);
		}
		
		protected bool Equals(CosmicObject other)
		{
			return base.Equals(other) && Id == other.Id;
		}
    }
}
