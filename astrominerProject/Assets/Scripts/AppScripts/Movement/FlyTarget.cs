using UnityEngine;

namespace SBaier.Astrominer
{
    public interface FlyTarget
    {
        Vector2 LandingPoint { get; }
        float DistanceTo(Vector2 position);
        bool IsInRange(Vector2 position, float range);
    }
}
