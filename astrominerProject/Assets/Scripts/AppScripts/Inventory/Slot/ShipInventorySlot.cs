using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventorySlot : MonoBehaviour
    {
        public event Action OnPool;

        public void InvokeOnPool()
        {
            OnPool?.Invoke();
        }
        
        public class Arguments
        {
            public int Index;
        }
    }
}
