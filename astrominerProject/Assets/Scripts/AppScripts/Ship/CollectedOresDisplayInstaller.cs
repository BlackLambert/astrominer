using SBaier.DI;

namespace SBaier.Astrominer
{
    public class CollectedOresDisplayInstaller : MonoInstaller, Injectable
    {
		private Ship _ship;

		public void Inject(Resolver resolver)
		{
			_ship = resolver.Resolve<Ship>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_ship.CollectedOres);
		}
	}
}
