using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventoryItemCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
	    [SerializeField] 
	    private Transform _hook;
	    
		private Pool<ShipInventoryItem, ExploitMachine, PrefabInstantiationArguments> _itemPool;
		private Ship _ship;
		private ShipInventorySlot.Arguments _slotArguments;
		private ShipInventorySlot _slot;
		private int index => _slotArguments.Index;
		private ShipInventoryItem _item;

		public void Inject(Resolver resolver)
		{
			_itemPool = resolver.Resolve<Pool<ShipInventoryItem, ExploitMachine, PrefabInstantiationArguments>>();
			_ship = resolver.Resolve<Ship>();
			_slotArguments = resolver.Resolve<ShipInventorySlot.Arguments>();
			_slot = resolver.Resolve<ShipInventorySlot>();
		}

		public void Initialize()
		{
			CreateItem();
			_ship.Machines.OnItemsChanged += UpdateItem;
			_slot.OnPool += ReturnItem;
		}

		public void Clean()
		{
			_ship.Machines.OnItemsChanged -= UpdateItem;
			_slot.OnPool -= ReturnItem;
		}

		private void UpdateItem()
		{
			ReturnItem();
			CreateItem();
		}

		private void CreateItem()
		{
			if (_ship.Machines.Count > index)
			{
				CreateItemFor(_ship.Machines[index], index);
			}
		}

		private void CreateItemFor(ExploitMachine machine, int i)
		{
			_item = _itemPool.Request(machine, PrefabInstantiationArguments.CreateFittedUIArgs(_hook));
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
