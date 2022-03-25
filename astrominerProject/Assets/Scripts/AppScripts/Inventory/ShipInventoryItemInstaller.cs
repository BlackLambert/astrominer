using SBaier.DI;

namespace SBaier.Astrominer
{
	public class ShipInventoryItemInstaller : MonoInstaller, Injectable
	{
		private ExploitMachine _machine;

		public void Inject(Resolver resolver)
		{
			_machine = resolver.Resolve<ExploitMachine>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_machine);
		}
	}
}
