using UnityEngine;

namespace SBaier.Astrominer
{
    public interface BasePositionGetter
    {
        public Vector2 GetFor(Player player);
    }
}
