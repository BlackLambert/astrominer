using UnityEngine;

namespace SBaier.Astrominer
{
    public class TouchPointerPosition : PointerPosition
    {
        public bool HasPosition => Input.touchCount > 0;
        public Vector2 CurrentPosition => Input.touches[0].position;
    }
}
