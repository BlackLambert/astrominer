using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventoryPanel : MonoBehaviour
    {
        public List<ShipInventorySlot> Slots { get; private set; }
        public List<ShipInventoryItem> Items { get; } = new List<ShipInventoryItem>();
        public bool SlotsInitialized = false;
        public event Action OnSlotsInitialized;

        public void InitSlots(List<ShipInventorySlot> slot)
		{
            Slots = slot;
            SlotsInitialized = true;
            OnSlotsInitialized?.Invoke();
        }
    }
}
