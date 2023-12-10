using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "MapCreationSettings", menuName = "ScriptableObjects/MapCreationSettings")]
    public class MapCreationSettings : ScriptableObject
    {
        [SerializeField]
        private List<AstroidAmountOption> _astroidAmountOptions = new List<AstroidAmountOption>();
        [field: SerializeField]
        public float MinimalAsteroidDistance = 0.8f;

        public IReadOnlyList<AstroidAmountOption> AstroidAmountOptions => _astroidAmountOptions;
    }
}
