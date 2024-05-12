using System.Collections.Generic;
using PCGToolkit.Sampling;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapCreator : Injectable
    {
        private AsteroidArgumentsGenerator _generator;
        private MapCreationSettings _creationSettings;
        private int _retries = 0;

        public void Inject(Resolver resolver)
        {
            _generator = resolver.Resolve<AsteroidArgumentsGenerator>();
            _creationSettings = resolver.Resolve<MapCreationSettings>();
        }

        public List<Asteroid.Arguments> CreateMap(AsteroidAmountOption amountOption)
        {
            try
            {
                _retries = 0;
                return _generator.GenerateMap(amountOption, _creationSettings.MinimalAsteroidDistance);
            }
            catch (PoissonDiskSampling2D.SamplingException)
            {
                if (_retries >= _creationSettings.SamplingRetriesOnFail)
                {
                    throw;
                }

                _retries++;
                Debug.LogWarning("Failed to create asteroid positions. Retrying...");
                return CreateMap(amountOption);
            }
        }
    }
}
