using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Map
    {
        public Observable<List<Vector2>> AsteroidPositions { get; } = new Observable<List<Vector2>>();
        public Observable<List<Asteroid>> Asteroids { get; } = new Observable<List<Asteroid>>();
        public Observable<Dictionary<Player, Vector2>> BasePositions { get; } = new Observable<Dictionary<Player, Vector2>>();
        public Observable<Dictionary<Player, Base>> Bases { get; } = new Observable<Dictionary<Player, Base>>();
        public Observable<AsteroidAmountOption> AsteroidAmountOption { get; } = new Observable<AsteroidAmountOption>();
        public Vector2 CenterPoint => AsteroidAmountOption.Value.MapCenterPoint;
    }
}
