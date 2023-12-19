using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Map
    {
        public Observable<List<Vector2>> AsteroidPositions { get; } = new List<Vector2>();
    }
}
