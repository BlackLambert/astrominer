using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventorySlot : MonoBehaviour
    {
        [SerializeField]
        private Transform _hook;

        public bool HasItem => Item != null;
        public ShipInventoryItem Item { get; private set; }

        public void SetItem(ShipInventoryItem item)
		{
            if (Item != null)
                throw new InvalidOperationException();
            Item = item;
            Item.transform.SetParent(_hook, false);
        }

        public ShipInventoryItem RemoveItem()
		{
            if (Item == null)
                throw new InvalidOperationException();
            ShipInventoryItem item = Item;
            Item = null;
            return item;
        }
    }
}
