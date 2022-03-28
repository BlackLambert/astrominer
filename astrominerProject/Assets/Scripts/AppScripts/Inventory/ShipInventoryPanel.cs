using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventoryPanel : MonoBehaviour
    {
        public ObservableList<ShipInventorySlot> Slots { get; } = new ObservableList<ShipInventorySlot>();
        public List<ShipInventoryItem> Items { get; } = new List<ShipInventoryItem>();
    }
}
