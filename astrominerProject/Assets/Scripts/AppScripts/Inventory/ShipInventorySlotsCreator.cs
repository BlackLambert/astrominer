using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventorySlotsCreator : MonoBehaviour, Injectable
    {
		[SerializeField]
		private Transform _hook;

		private Factory<ShipInventorySlot> _slotFactory;
		private Ship _ship;
		private ShipInventoryPanel _inventoryPanel;

		private ObservableList<ShipInventorySlot> _slots => _inventoryPanel.Slots;
		private List<ShipInventoryItem> _items => _inventoryPanel.Items;

		public void Inject(Resolver resolver)
		{
			_slotFactory = resolver.Resolve<Factory<ShipInventorySlot>>();
			_inventoryPanel = resolver.Resolve<ShipInventoryPanel>();
			_ship = resolver.Resolve<Ship>();
		}

		private void Start()
		{
			Init();
			_ship.Machines.OnLimitChanged += UpdateSlots;
		}

		private void OnDestroy()
		{
			_ship.Machines.OnLimitChanged -= UpdateSlots;
		}

		private void Init()
		{
			for (int i = 0; i < _ship.Machines.Limit; i++)
				_slots.Add(CreateSlot());
		}

		private ShipInventorySlot CreateSlot()
		{
			ShipInventorySlot slot = _slotFactory.Create();
			slot.transform.SetParent(_hook, false);
			return slot;
		}

		private void UpdateSlots()
		{
			int limit = _ship.Machines.Limit;
			int count = _slots.Count;
			if (limit < count)
				RemoveSlotsTill(limit);
			else if (limit > count)
				AddSlotsTill(limit);
		}

		private void AddSlotsTill(int limit)
		{
			for (int i = _slots.Count; i < limit; i++)
				CreateSlot();
		}

		private void RemoveSlotsTill(int limit)
		{
			for (int i = _slots.Count; i > limit; i--)
				RemoveSlot();
		}

		private void RemoveSlot()
		{
			int last = _slots.Count - 1;
			ShipInventorySlot slot = _slots[last];
			_slots.Remove(slot);
			Destroy(slot.gameObject);
			if (_items.Count > last)
				_items.RemoveAt(last);
		}
	}
}
