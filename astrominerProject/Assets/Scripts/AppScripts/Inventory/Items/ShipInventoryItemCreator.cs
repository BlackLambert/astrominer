using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace SBaier.Astrominer
{
    public class ShipInventoryItemCreator : MonoBehaviour, Injectable
    {
	    [SerializeField] 
	    private Transform _hook;
	    
		private Pool<ShipInventoryItem, ExploitMachine> _itemPool;
		private Ship _ship;
		private ShipInventorySlot.Arguments _slotArguments;
		private ShipInventorySlot _slot;
		private int index => _slotArguments.Index;
		private ShipInventoryItem _item;

		public void Inject(Resolver resolver)
		{
			_itemPool = resolver.Resolve<Pool<ShipInventoryItem, ExploitMachine>>();
			_ship = resolver.Resolve<Ship>();
			_slotArguments = resolver.Resolve<ShipInventorySlot.Arguments>();
			_slot = resolver.Resolve<ShipInventorySlot>();
		}

		private void OnEnable()
		{
			CreateItem();
			_ship.Machines.OnItemAddedAt += TryCreateItemFor;
			_ship.Machines.OnItemRemovedAt += RemoveItemFor;
			_ship.Machines.OnItemReplacedAt += ChangeItem;
			_slot.OnPool += ReturnItem;
		}

		private void OnDisable()
		{
			_ship.Machines.OnItemAddedAt -= TryCreateItemFor;
			_ship.Machines.OnItemRemovedAt -= RemoveItemFor;
			_ship.Machines.OnItemReplacedAt -= ChangeItem;
			_slot.OnPool -= ReturnItem;
		}

		private void CreateItem()
		{
			if (_ship.Machines.Count > index)
			{
				TryCreateItemFor(_ship.Machines[index], index);
			}
		}

		private void TryCreateItemFor(ExploitMachine machine, int i)
		{
			if (i != index || machine == null)
			{
				return;
			}
			
			_item = _itemPool.Request(machine);
			Transform trans = _item.transform;
			trans.SetParent(_hook, false);
			trans.localScale = Vector3.one;
		}

		private void RemoveItemFor(ExploitMachine machine, int i)
		{
			if (i != index)
			{
				return;
			}
			
			ReturnItem();
		}

		private void ChangeItem(ExploitMachine formerItem, ExploitMachine newItem, int i)
		{
			if (i != index)
			{
				return;
			}

			ReturnItem();
			TryCreateItemFor(newItem, index);
		}

		private void ReturnItem()
		{
			if (_item != null)
			{
				_itemPool.Return(_item);
				_item = null;
			}
		}
    }
}
