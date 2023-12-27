using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    [Serializable]
    public class AsteroidAmountOption
    {
        [field: SerializeField]
        public int Amount { get; private set; }
        [field: SerializeField]
        public Vector2 MapSize { get; private set; }
        [field: SerializeField]
        public float Zoom { get; private set; }
        [field: SerializeField] 
        public Vector2 MapCenterPoint { get; private set; }

        public AsteroidAmountOption(
            int amount,
            Vector2 mapSize,
            float zoom,
            Vector2 mapCenterPoint)
        {
            Amount = amount;
            MapSize = mapSize;
            Zoom = zoom;
            MapCenterPoint = mapCenterPoint;
        }
    }
}
