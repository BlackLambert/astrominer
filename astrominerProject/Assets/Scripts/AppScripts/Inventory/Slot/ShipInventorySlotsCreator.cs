using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventorySlotsCreator : MonoBehaviour, Injectable
    {
        [SerializeField] private Transform _hook;

        private Pool<ShipInventorySlot, ShipInventorySlot.Arguments> _slotsPool;
        private Ship _ship;
        private ShipInventoryPanel _inventoryPanel;

        private ObservableList<ShipInventorySlot> _slots => _inventoryPanel.Slots;

        public void Inject(Resolver resolver)
        {
            _slotsPool = resolver.Resolve<Pool<ShipInventorySlot, ShipInventorySlot.Arguments>>();
            _inventoryPanel = resolver.Resolve<ShipInventoryPanel>();
            _ship = resolver.Resolve<Ship>();
        }

        private void OnEnable()
        {
            Init();
            _ship.Machines.OnLimitChanged += UpdateSlots;
            _inventoryPanel.OnPool += ReturnSlots;
        }

        private void OnDisable()
        {
            _ship.Machines.OnLimitChanged -= UpdateSlots;
            _inventoryPanel.OnPool -= ReturnSlots;
        }

        private void ReturnSlots()
        {
            foreach (ShipInventorySlot slot in _slots)
            {
                slot.InvokeOnPool();
                _slotsPool.Return(slot);
            }
            _slots.Clear();
        }

        private void Init()
        {
            for (int i = 0; i < _ship.Machines.Limit; i++)
                _slots.Add(CreateSlot(i));
        }

        private ShipInventorySlot CreateSlot(int index)
        {
            ShipInventorySlot slot = _slotsPool.Request(new ShipInventorySlot.Arguments() { Index = index });
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
                CreateSlot(i);
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
            _slotsPool.Return(slot);
        }
    }
}