using PCGToolkit.Sampling;
using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapSceneInstaller : MonoInstaller, Injectable
    {
        [SerializeField]
        private MapGenerator _generator;
        [SerializeField]
        private int _maxSamplerTries = 5;
        [SerializeField]
        private MapCreationSettings _mapCreationSettings;
        [SerializeField]
        private AsteroidSettings _asteroidSettings;
        [SerializeField]
        private Asteroid _asteroidPrefab;

        private System.Random _random;

        public void Inject(Resolver resolver)
        {
            _random = resolver.Resolve<System.Random>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_generator)
                .WithoutInjection();

            binder.BindInstance(CreateSampler())
                .WithoutInjection();

            binder.BindInstance(_mapCreationSettings)
                .WithInjection();

            binder.BindInstance(_asteroidSettings)
                .WithInjection();

            binder.Bind<Factory<List<Asteroid>, IEnumerable<Vector2>>>()
                .ToNew<AsteroidsFactory>();

            binder.Bind<Factory<Asteroid, Asteroid.Arguments>>().
                ToNew<PrefabFactory<Asteroid, Asteroid.Arguments>>().
                WithArgument(_asteroidPrefab);

            binder.BindToNewSelf<Selection>().AsSingle();

            binder.Bind<ActiveItem<Asteroid>>().ToNew<SelectedAsteroid>().AsSingle();
        }

        private PoissonDiskSampling2D CreateSampler()
        {
            return new PoissonDiskSampling2D(_random, _maxSamplerTries, new PoissonDiskSampling2DParameterValidator());
        }
    }
}
