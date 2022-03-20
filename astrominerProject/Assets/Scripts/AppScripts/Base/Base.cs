using UnityEngine;

namespace SBaier.Astrominer
{
    public class Base : MonoBehaviour
    {
        [field: SerializeField]
        public FlyTarget FlyTarget { get; private set; }
    }
}
