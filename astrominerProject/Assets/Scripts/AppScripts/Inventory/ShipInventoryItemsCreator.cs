using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventoryItemsCreator : MonoBehaviour, Injectable
    {
		private Factory<ShipInventoryItem, ExploitMachine> _itemFactory;
		private Ship _ship;
		private ShipInventoryPanel _inventoryPanel;

		private List<ShipInventorySlot> _slots => _inventoryPanel.Slots;
		private List<ShipInventoryItem> _items => _inventoryPanel.Items;

		public void Inject(Resolver resolver)
		{
			Debug.Log("ShipInventoryItemsCreator Inject");
			_itemFactory = resolver.Resolve<Factory<ShipInventoryItem, ExploitMachine>>();
			_inventoryPanel = resolver.Resolve<ShipInventoryPanel>();
			_ship = resolver.Resolve<Ship>();
		}

		private void Start()
		{
			Init();
			_ship.Machines.OnItemAdded += AddItem;
			_ship.Machines.OnItemRemoved += RemoveItem;
		}

		private void OnDestroy()
		{
			_ship.Machines.OnItemAdded -= AddItem;
			_ship.Machines.OnItemRemoved -= RemoveItem;
		}

		private void Init()
		{
			if (_inventoryPanel.SlotsInitialized)
				CreateItems();
			else
				_inventoryPanel.OnSlotsInitialized += OnSlotsInitialized;
		}

		private void OnSlotsInitialized()
		{
			_inventoryPanel.OnSlotsInitialized -= OnSlotsInitialized;
			CreateItems();
		}

		private void CreateItems()
		{
			for (int i = 0; i < _ship.Machines.Count; i++)
				AddItemAt(i);
		}

		private void AddItemAt(int index)
		{
			ShipInventoryItem item = _itemFactory.Create(_ship.Machines.GetAt(index));
			while (_items.Count <= index)
				_items.Add(null);
			_items[index] = item;
			_slots[index].SetItem(item);
		}

		private void AddItem(ExploitMachine machine)
		{
			int index = _slots.FindIndex(s => !s.HasItem);
			AddItemAt(index);
		}

		private void RemoveItem(ExploitMachine machine)
		{
			int index = _items.FindIndex(i => i.Machine == machine);
			ShipInventoryItem item = _items[index];
			_slots[index].RemoveItem();
			_items.RemoveAt(index);
		}
	}
}
