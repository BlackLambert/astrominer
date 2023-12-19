using PCGToolkit.Sampling;
using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private AsteroidSettings _asteroidSettings;
        [SerializeField]
        private Asteroid _asteroidPrefab;

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_asteroidSettings)
                .WithInjection();

            binder.Bind<Factory<List<Asteroid>, IEnumerable<Vector2>>>()
                .ToNew<AsteroidsFactory>();

            binder.Bind<Factory<Asteroid, Asteroid.Arguments>>().
                ToNew<PrefabFactory<Asteroid, Asteroid.Arguments>>().
                WithArgument(_asteroidPrefab);

            binder.BindToNewSelf<Selection>().AsSingle();

            binder.Bind<ActiveItem<Asteroid>>().ToNew<SelectedAsteroid>().AsSingle();
            
            binder.BindToNewSelf<Map>().AsSingle();
        }
    }
}
