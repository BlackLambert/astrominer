using System.Collections.Generic;
using PCGToolkit.Sampling;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapCreationInstaller : MonoInstaller, Injectable
    {
        [SerializeField] 
        private MapCreationSettings _mapCreationSettings;

        [SerializeField] 
        private ShipSettings _shipSettings;

        [SerializeField] 
        private ComputerBasePlacementSettings _computerBasePlacementSettings;

        private System.Random _random;

        public void Inject(Resolver resolver)
        {
            _random = resolver.Resolve<System.Random>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(CreateSampler())
                .WithoutInjection();

            binder.BindInstance(_mapCreationSettings)
                .WithInjection();

            binder.BindToNewSelf<MapCreationContext>()
                .AsSingle();

            binder.Bind<ActiveItem<AsteroidAmountOption>>()
                .ToNew<SelectedAsteroidsAmount>()
                .AsSingle();

            binder.BindToNewSelf<AsteroidArgumentsGenerator>().AsSingle();

            binder.BindToNewSelf<BasesPlacementContext>()
                .AsSingle();

            binder.Bind<Factory<List<Asteroid>, List<Asteroid.Arguments>>>()
                .ToNew<AsteroidsFactory>();

            binder.BindToNewSelf<MapCreator>();

            binder.Bind<BasePositionGetter>()
                .ToNew<BasicBasePositionGetter>()
                .WithArgument(CreateBasicPositionGetterArguments());

            binder.BindInstance(_shipSettings);
        }

        private PoissonDiskSampling2D CreateSampler()
        {
            return new PoissonDiskSampling2D(_random, _mapCreationSettings.MaxSamplerTries,
                new PoissonDiskSampling2DParameterValidator());
        }

        private BasicBasePositionGetter.Arguments CreateBasicPositionGetterArguments()
        {
            return new BasicBasePositionGetter.Arguments()
            {
                RetryAmount = _computerBasePlacementSettings.PositionGetterRetryAmount,
                MinDistanceToObjects = _computerBasePlacementSettings.MinDistanceToObjects,
                MinBaseDistance = _computerBasePlacementSettings.MinBaseDistance,
                MinAmountOfAsteroidsInRange = _computerBasePlacementSettings.MinAmountOfAsteroidsInRange
            };
        }
    }
}