using SBaier.DI;

namespace SBaier.Astrominer
{
	public class AsteroidContextPanelInstaller : MonoInstaller, Injectable
	{
		private AsteroidContextPanel.Arguments _arguments;
		private Bases _bases;
		
		void Injectable.Inject(Resolver resolver)
		{
			_arguments = resolver.Resolve<AsteroidContextPanel.Arguments>();
			_bases = resolver.Resolve<Bases>();
		}
		
		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Asteroid>().And<FlyTarget>().To<Asteroid>().FromInstance(_arguments.Asteroid).WithoutInjection();
			binder.BindInstance(_arguments.Ship.Player).WithoutInjection();
			binder.BindInstance(_arguments.Ship).WithoutInjection();
			binder.BindInstance(_arguments.Ship.Player.IdentifiedAsteroids).WithoutInjection();
			binder.BindInstance(_arguments.Ship.Player.ProspectorDrones).WithoutInjection();
			binder.BindInstance(_bases[_arguments.Ship.Player]).WithoutInjection();
		}
	}
}
