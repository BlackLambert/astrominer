using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ShipInventoryPanelInstaller : MonoInstaller, Injectable
	{
		[SerializeField]
		private ShipInventoryItem _inventoryItemPrefab;
		[SerializeField]
		private ShipInventorySlot _inventorySlotPrefab;
		[SerializeField]
		private ShipInventoryPanel _inventoryPanel;

		private Ship _ship;

		public void Inject(Resolver resolver)
		{
			_ship = resolver.Resolve<ActiveShip>().Value;
		}

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_ship);
			binder.Bind<Factory<ShipInventoryItem, ExploitMachine>>().
				ToNew<PrefabFactory<ShipInventoryItem, ExploitMachine>>().
				WithArgument(_inventoryItemPrefab);
			binder.Bind<Pool<ShipInventoryItem, ExploitMachine>>().
				ToNew<MonoPool<ShipInventoryItem, ExploitMachine>>().
				WithArgument(_inventoryItemPrefab).AsSingle();
			binder.Bind<Factory<ShipInventorySlot>>().
				ToNew<PrefabFactory<ShipInventorySlot>>().
				WithArgument(_inventorySlotPrefab);
			binder.Bind<Pool<ShipInventorySlot>>().
				ToNew<MonoPool<ShipInventorySlot>>().
				WithArgument(_inventorySlotPrefab).AsSingle();
			binder.BindInstance(_inventoryPanel);
			binder.Bind<ActiveItem<ShipInventoryItem>>().ToNew<SelectedInventoryItem>().AsSingle();
			binder.BindToNewSelf<Selection>().AsSingle();
		}
	}
}
