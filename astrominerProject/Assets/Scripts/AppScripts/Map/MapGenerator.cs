using System.Collections.Generic;
using UnityEngine;
using SBaier.DI;
using PCGToolkit.Sampling;
using System;

namespace SBaier.Astrominer
{
    public class MapGenerator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Transform _hook;

        private PoissonDiskSampling2D _sampler;
        private MapCreationSettings _settings;
        private Factory<List<Asteroid>, IEnumerable<Vector2>> _factory;

        List<Asteroid> _asteroids = new List<Asteroid>();

        public void Inject(Resolver resolver)
        {
            _sampler = resolver.Resolve<PoissonDiskSampling2D>();
            _settings = resolver.Resolve<MapCreationSettings>();
            _factory = resolver.Resolve<Factory<List<Asteroid>, IEnumerable<Vector2>>>();
        }

        public void GenerateMap(AstroidAmountOption amountOption)
        {
            ClearAsteroids();
            Vector2 size = amountOption.MapSize;
            Vector2 leftBottom = new Vector2(size.x / -2, size.y / -2);
            RectangleBounds bounds = new RectangleBounds(leftBottom, size);
            List<Vector2> samples = _sampler.Sample(new PoissonDiskSampling2D.Parameters(
                amountOption.Amount, _settings.MinimalAsteroidDistance, bounds, Vector2.zero));
            List<Asteroid> asteroids = _factory.Create(samples);

            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.Base.SetParent(_hook);
                asteroid.Base.localScale = Vector3.one;
            }

            _asteroids.AddRange(asteroids);
        }

        private void ClearAsteroids()
        {
            foreach (Asteroid asteroid in _asteroids)
            {
                Destroy(asteroid.Base.gameObject);
            }
            _asteroids.Clear();
        }
    }
}
