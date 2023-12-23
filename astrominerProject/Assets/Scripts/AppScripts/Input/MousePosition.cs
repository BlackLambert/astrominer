using UnityEngine;

namespace SBaier.Astrominer
{
    public class MousePosition : PointerPosition
    {
        public bool IsActive => true;
        public Vector2 CurrentPosition => Input.mousePosition;
    }
}
