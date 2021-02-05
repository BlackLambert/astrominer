using System;
using UnityEngine;

namespace Astrominer
{
    public interface Mover
    {
        public abstract event Action OnTargetReached;
        public void MoveTo(Vector2 target);
        
    }
}