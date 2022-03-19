using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class AsteroidContextPanelInstaller : MonoInstaller, Injectable
	{
		private Asteroid _asteroid;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_asteroid).WithoutInjection();
			binder.BindInstance(_asteroid.FlyTarget).WithoutInjection();
		}

		void Injectable.Inject(Resolver resolver)
		{
			_asteroid = resolver.Resolve<Asteroid>();
		}
	}
}
