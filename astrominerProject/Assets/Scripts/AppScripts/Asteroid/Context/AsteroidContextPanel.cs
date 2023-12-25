using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidContextPanel : ContextPanel<Asteroid>
    {
        public class Arguments
        {
            public Ship Ship;
            public Asteroid Asteroid;
        }
    }
}
