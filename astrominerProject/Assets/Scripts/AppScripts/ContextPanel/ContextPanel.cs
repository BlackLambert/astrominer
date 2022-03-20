using UnityEngine;

namespace SBaier.Astrominer
{
    public class ContextPanel<T> : MonoBehaviour
    {
        [field: SerializeField]
        public Transform Base { get; private set; }
    }
}
