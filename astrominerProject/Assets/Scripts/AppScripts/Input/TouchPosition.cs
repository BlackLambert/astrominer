using UnityEngine;

namespace SBaier.Astrominer
{
    public class TouchPosition : PointerPosition
    {
        public bool IsActive => Input.touchCount > 0;
        public Vector2 CurrentPosition => Input.touches[0].position;
    }
}
