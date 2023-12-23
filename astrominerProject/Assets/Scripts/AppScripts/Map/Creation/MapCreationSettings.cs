using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "MapCreationSettings", menuName = "ScriptableObjects/MapCreationSettings")]
    public class MapCreationSettings : ScriptableObject
    {
        [SerializeField]
        private List<AsteroidAmountOption> asteroidAmountOptions = new List<AsteroidAmountOption>();
        
        [field: SerializeField]
        public float MinimalAsteroidDistance { get; private set; } = 1.4f;

        [field: SerializeField] 
        public int SamplingRetriesOnFail { get; private set; } = 10;
        
        [field: SerializeField]
        public int MaxSamplerTries { get; private set; } = 3;

        public IReadOnlyList<AsteroidAmountOption> AsteroidAmountOptions => asteroidAmountOptions;
    }
}