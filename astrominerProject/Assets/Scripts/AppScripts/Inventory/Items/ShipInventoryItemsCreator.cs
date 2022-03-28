using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace SBaier.Astrominer
{
    public class ShipInventoryItemsCreator : MonoBehaviour, Injectable
    {
		private Factory<ShipInventoryItem, ExploitMachine> _itemFactory;
		private Ship _ship;
		private ShipInventoryPanel _inventoryPanel;

		private ObservableList<ShipInventorySlot> _slots => _inventoryPanel.Slots;
		private List<ShipInventoryItem> _items => _inventoryPanel.Items;

		public void Inject(Resolver resolver)
		{
			_itemFactory = resolver.Resolve<Factory<ShipInventoryItem, ExploitMachine>>();
			_inventoryPanel = resolver.Resolve<ShipInventoryPanel>();
			_ship = resolver.Resolve<Ship>();
		}

		private void Start()
		{
			CreateItems();
			_inventoryPanel.Slots.OnItemAdded += AddEmptyItem;
			_inventoryPanel.Slots.OnItemRemoved += RemoveLastItem;
			_ship.Machines.OnItemAdded += AddItem;
			_ship.Machines.OnItemRemoved += RemoveItem;
			_ship.Machines.OnItemReplaced += ChangeItem;
		}

		private void OnDestroy()
		{
			_inventoryPanel.Slots.OnItemAdded -= AddEmptyItem;
			_inventoryPanel.Slots.OnItemRemoved -= RemoveLastItem;
			_ship.Machines.OnItemAdded -= AddItem;
			_ship.Machines.OnItemRemoved -= RemoveItem;
			_ship.Machines.OnItemReplaced -= ChangeItem;
		}

		private void CreateItems()
		{
			for (int i = 0; i < _inventoryPanel.Slots.Count; i++)
			{
				AddEmptyItem();
				if(_ship.Machines.Count > i)
					SetItemAt(_ship.Machines[i], i);
			}
		}

		private void AddEmptyItem(ShipInventorySlot _)
		{
			AddEmptyItem();
		}

		private void AddEmptyItem()
		{
			_inventoryPanel.Items.Add(null);
		}

		private void RemoveLastItem(ShipInventorySlot _)
		{
			_items.RemoveAt(_items.Count - 1);
		}

		private void AddItem(ExploitMachine machine)
		{
			int index = _slots.ToList().FindIndex(s => !s.HasItem);
			SetItemAt(machine, index);
		}

		private void SetItemAt(ExploitMachine machine, int index)
		{
			ShipInventoryItem item = _itemFactory.Create(machine);
			while (_items.Count <= index)
				_items.Add(null);
			_items[index] = item;
			_slots[index].SetItem(item);
		}

		private void RemoveItem(ExploitMachine machine)
		{
			int index = IndexOf(machine);
			if (index < 0)
				throw new InvalidOperationException();
			RemoveItem(index);
		}

		private void ChangeItem(ExploitMachine formerItem, ExploitMachine newItem)
		{
			int index = IndexOf(formerItem);
			if (index < 0)
				throw new InvalidOperationException();
			RemoveItem(index);
			SetItemAt(newItem, index);
		}

		private void RemoveItem(int index)
		{
			_slots[index].RemoveItem();
			_items[index] = null;
		}

		private int IndexOf(ExploitMachine machine)
		{
			return _items.FindIndex(i => i != null && i.Machine == machine);
		}
	}
}
