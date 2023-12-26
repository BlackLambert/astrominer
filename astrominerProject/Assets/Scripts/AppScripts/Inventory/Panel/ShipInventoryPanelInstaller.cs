using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ShipInventoryPanelInstaller : MonoInstaller, Injectable
	{
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
			binder.BindInstance(_ship.Player);
			binder.BindInstance(_inventoryPanel);
			binder.Bind<ActiveItem<ShipInventoryItem>>().ToNew<SelectedInventoryItem>().AsSingle();
			binder.BindToNewSelf<Selection>().AsSingle();
		}
	}
}
