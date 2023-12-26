using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ContextPanel<T> : MonoBehaviour
    {
        public event Action OnPooling;
        [field: SerializeField]
        public Transform Base { get; private set; }

        public void InvokeOnPooling()
        {
            OnPooling?.Invoke();
        }
    }
}
