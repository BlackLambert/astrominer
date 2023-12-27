using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Map
    {
        public Observable<List<Asteroid.Arguments>> AsteroidArguments { get; } = new Observable<List<Asteroid.Arguments>>();
        public Observable<List<Asteroid>> Asteroids { get; } = new Observable<List<Asteroid>>();        
        public Observable<AsteroidAmountOption> AsteroidAmountOption { get; } = new Observable<AsteroidAmountOption>();
        public Vector2 CenterPoint => AsteroidAmountOption.Value.MapCenterPoint;
        public Vector2 BottomLeftPoint => Vector2.zero - AsteroidAmountOption.Value.MapSize / 2 + CenterPoint;

        public float GetTotalExploitedPercentage()
        {
            float sum = Asteroids.Value.Sum(asteroid => asteroid.MinedPercentage);
            return sum / Asteroids.Value.Count;
        }
    }
}
