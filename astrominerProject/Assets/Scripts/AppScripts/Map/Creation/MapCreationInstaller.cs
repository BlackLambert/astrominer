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

        [SerializeField] 
        private TargetExploitSettings _targetExploitSettings;

        private System.Random _random;

        public void Inject(Resolver resolver)
        {
            _random = resolver.Resolve<System.Random>();
            _random = _random.CreateWithNewSeed();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(new Observable<MapCreationState>() { Value = MapCreationState.AsteroidGeneration });
            
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
            
            binder.BindInstance(_targetExploitSettings)
                .WithInjection();

            binder.Bind<Factory<List<Asteroid>, List<Asteroid.Arguments>>>()
                .ToNew<AsteroidsFactory>();

            binder.BindToNewSelf<MapFactory>();

            binder.Bind<BasePositionGetter>()
                .ToNew<BasicBasePositionGetter>()
                .WithArgument(CreateBasicPositionGetterArguments());

            binder.BindInstance(_shipSettings);
        }

        private PoissonDiskSampling2D CreateSampler()
        {
            return new PoissonDiskSampling2D(_random, _mapCreationSettings.MaxSamplerTries,
                new PoissonDiskSamplingParameterValidator<Vector2>());
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