using UnityEngine;

namespace Astrominer.Test
{
    public class DummyMoveable : Moveable
    {

        public Vector2 Position
        {
            get; set;
        }

        public DummyMoveable(Vector2 position)
        {
            Position = position;
        }
    }
}