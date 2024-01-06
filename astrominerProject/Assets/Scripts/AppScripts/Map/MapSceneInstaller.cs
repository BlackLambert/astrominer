using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private AsteroidSettings _asteroidSettings;

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_asteroidSettings)
                .WithInjection();
            
            binder.BindToNewSelf<Map>()
                .AsSingle();

            binder.BindToNewSelf<Bases>()
                .AsSingle();

            binder.BindToNewSelf<BasePositions>()
                .AsSingle();

            binder.Bind<Provider<IList<CosmicObject>>>()
                .ToNew<CosmicObjectsProvider>()
                .AsSingle();

            binder.Bind<Provider<IList<FlyTarget>>>()
                .ToNew<FlyTargetsProvider>()
                .AsSingle();

            binder.BindToNewSelf<CosmicObjectInRangeGetter>();

            binder.BindToNewSelf<FlightPathFinder>();

            binder.Bind<ActiveItem<FlightPath>>()
                .ToNew<ActiveFlightPath>()
                .AsSingle();
        }
    }
}
