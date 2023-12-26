using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventoryPanel : MonoBehaviour
    {
        public event Action OnPool;
        public ObservableList<ShipInventorySlot> Slots { get; } = new ObservableList<ShipInventorySlot>();

        public void InvokeOnPool()
        {
            OnPool?.Invoke();
        }
    }
}
