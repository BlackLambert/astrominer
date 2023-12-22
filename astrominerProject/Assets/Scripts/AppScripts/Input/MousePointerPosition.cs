using UnityEngine;

namespace SBaier.Astrominer
{
    public class MousePointerPosition : PointerPosition
    {
        public bool HasPosition => true;
        public Vector2 CurrentPosition => Input.mousePosition;
    }
}
