using System;
using UnityEngine;

namespace Astrominer
{
    public abstract class Mover : MonoBehaviour
    {
        public abstract event Action OnTargetReached;
        public abstract void MoveTo(Vector2 target);
        public abstract Vector2 DistanceVectorToTarget { get; }
    }
}