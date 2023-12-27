using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "MapCreationSettings", menuName = "ScriptableObjects/MapCreationSettings")]
    public class MapCreationSettings : ScriptableObject
    {
        [field: SerializeField] 
        public int MinAsteroids { get; } = 10;
        
        [field: SerializeField] 
        public int MinAsteroidsAdditionPerPlayer { get; } = 5;
        
        [field: SerializeField] 
        public int MaxAsteroidsAmount { get; private set; } = 150;

        [field: SerializeField] 
        public int StartAsteroidAmount { get; private set; } = 50;

        [field: SerializeField] 
        public Vector2 StartMapSize { get; private set; } = new Vector2Int(34, 20);

        [field: SerializeField] 
        public Vector2 EndMapSize { get; private set; } = new Vector2Int(75, 44);

        [field: SerializeField] 
        public Vector2 CameraSizeRange { get; private set; } = new Vector2(11, 25);
        
        [field: SerializeField]
        public float MinimalAsteroidDistance { get; private set; } = 1.4f;

        [field: SerializeField] 
        public int SamplingRetriesOnFail { get; private set; } = 10;
        
        [field: SerializeField]
        public int MaxSamplerTries { get; private set; } = 3;
    }
}
