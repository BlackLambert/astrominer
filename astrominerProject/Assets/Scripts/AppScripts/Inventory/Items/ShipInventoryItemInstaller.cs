using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ShipInventoryItemInstaller : MonoInstaller, Injectable
	{
		[SerializeField]
		private ShipInventoryItem _item;
		private ExploitMachine _machine;

		public void Inject(Resolver resolver)
		{
			_machine = resolver.Resolve<ExploitMachine>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_machine);
			binder.BindInstance(_item);
		}
	}
}
