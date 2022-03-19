using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyTarget : MonoBehaviour
    {
        [SerializeField]
        private Transform _landingPointTransform;
        public Vector2 LandingPoint => _landingPointTransform.position;

        public float DistanceTo(Vector2 position)
		{
            return (position - LandingPoint).magnitude;
        }

        public bool IsInRange(Vector2 position, float range)
		{
            return DistanceTo(position) <= range;
        }
    }
}
