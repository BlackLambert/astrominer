using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class MinedOresPanelInstaller : MonoInstaller, Injectable
	{
		private Asteroid _asteroid;

		public void Inject(Resolver resolver)
		{
			_asteroid = resolver.Resolve<Asteroid>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_asteroid.MinedOres);
		}
	}
}
