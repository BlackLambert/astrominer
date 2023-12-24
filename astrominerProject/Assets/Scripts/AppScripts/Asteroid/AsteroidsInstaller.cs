using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class AsteroidsInstaller : MonoInstaller
	{
		[SerializeField]
		private AsteroidSettings _config;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Factory<List<Asteroid>, IEnumerable<Vector2>>>().ToNew<AsteroidsFactory>();
			binder.Bind<ActiveItem<Asteroid>>().ToNew<SelectedAsteroid>().AsSingle();
			binder.BindInstance(_config).WithoutInjection();
		}
	}
}
