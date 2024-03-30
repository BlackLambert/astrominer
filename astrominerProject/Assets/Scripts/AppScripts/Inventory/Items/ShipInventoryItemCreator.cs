using SBaier.DI;
using UnityEngine;

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
			_ship.Machines.OnItemsChanged += UpdateItem;
			_slot.OnPool += ReturnItem;
		}

		private void OnDisable()
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
			_item = _itemPool.Request(machine);
			Transform trans = _item.transform;
			trans.SetParent(_hook, false);
			trans.localScale = Vector3.one;
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
