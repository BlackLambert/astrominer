using SBaier.DI;

namespace SBaier.Astrominer
{
	public class AsteroidContextPanelInstaller : MonoInstaller, Injectable
	{
		private Asteroid _asteroid;
		private ActiveShip _activeShip;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Asteroid>().And<FlyTarget>().To<Asteroid>().FromInstance(_asteroid).WithoutInjection();
			binder.BindInstance(_activeShip.Value).WithoutInjection();
		}

		void Injectable.Inject(Resolver resolver)
		{
			_asteroid = resolver.Resolve<Asteroid>();
			_activeShip = resolver.Resolve<ActiveShip>();
		}
	}
}
