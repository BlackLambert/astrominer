using SBaier.DI;

namespace SBaier.Astrominer
{
	public class BaseContextPanelInstaller : MonoInstaller, Injectable
	{
		private Base _base;
		private ActiveShip _activeShip;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_base).WithoutInjection();
			binder.BindInstance(_base.FlyTarget).WithoutInjection();
			binder.BindInstance(_activeShip.Value).WithoutInjection();
		}

		void Injectable.Inject(Resolver resolver)
		{
			_base = resolver.Resolve<Base>();
			_activeShip = resolver.Resolve<ActiveShip>();
		}
	}
}
