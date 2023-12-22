using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Map
    {
        public Observable<List<Vector2>> AsteroidPositions { get; } = new List<Vector2>();
        public Observable<List<Asteroid>> Asteroids { get; } = new List<Asteroid>();
        public Observable<Dictionary<Player, Vector2>> BasePositions { get; } = new Dictionary<Player, Vector2>();
    }
}
