using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public interface FlyTarget : Location2D
    {
        Vector2 LandingPoint { get; }
        float DistanceTo(Vector2 position);
        bool IsInRange(Vector2 position, float range);
        bool IsAllowedFlightTargetFor(Player player);
    }
}
