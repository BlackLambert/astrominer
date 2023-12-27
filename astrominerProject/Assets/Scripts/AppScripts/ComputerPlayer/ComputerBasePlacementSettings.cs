using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "ComputerBasePlacementSettings",
        menuName = "ScriptableObjects/ComputerBasePlacementSettings")]
    public class ComputerBasePlacementSettings : ScriptableObject
    {
        [field: SerializeField] 
        public int PositionGetterRetryAmount { get; private set; } = 100;

        [field: SerializeField] 
        public float MinDistanceToObjects { get; private set; } = 1.4f;

        [field: SerializeField] 
        public float MinBaseDistance { get; private set; } = 4f;

        [field: SerializeField] 
        public int MinAmountOfAsteroidsInRange { get; private set; } = 3;
    }
}