using System.Collections.Generic;
using UnityEngine;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AsteroidsGenerator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Transform _hook;

        private Factory<List<Asteroid>, IEnumerable<Vector2>> _factory;
        private Pool<Asteroid, Asteroid.Arguments> _asteroidPool;
        private Map _map;

        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _factory = resolver.Resolve<Factory<List<Asteroid>, IEnumerable<Vector2>>>();
            _asteroidPool = resolver.Resolve<Pool<Asteroid, Asteroid.Arguments>>();
        }

        private void OnEnable()
        {
            GenerateMap();
            _map.AsteroidPositions.OnValueChanged += OnAsteroidPositionsChanged;
        }

        private void OnDisable()
        {
            _map.AsteroidPositions.OnValueChanged -= OnAsteroidPositionsChanged;
        }

        private void GenerateMap()
        {
            ClearAsteroids();

            if (_map.AsteroidPositions.Value == null)
            {
                return;
            }
            
            List<Asteroid> asteroids = _factory.Create(_map.AsteroidPositions.Value);

            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.Base.SetParent(_hook);
                asteroid.Base.localPosition = (Vector2)asteroid.Base.localPosition;
                asteroid.Base.localScale = Vector3.one;
            }

            _map.Asteroids.Value = asteroids;
        }

        private void ClearAsteroids()
        {
            if (_map.Asteroids.Value == null)
            {
                return;
            }
            
            foreach (Asteroid asteroid in _map.Asteroids.Value)
            {
                _asteroidPool.Return(asteroid);
            }

            _map.Asteroids.Value = new List<Asteroid>();
        }

        private void OnAsteroidPositionsChanged(List<Vector2> formervalue, List<Vector2> newvalue)
        {
            GenerateMap();
        }
    }
}
