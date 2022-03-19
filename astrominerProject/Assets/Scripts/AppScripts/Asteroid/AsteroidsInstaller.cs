using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class AsteroidsInstaller : MonoInstaller
	{
		[SerializeField]
		private AsteroidContextPanel _asteroidContextInfoPanelPrefab;
		[SerializeField]
		private Asteroid _asteroidPrefab;
		[SerializeField]
		private AsteroidSettings _config;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Factory<AsteroidContextPanel, Asteroid>>().ToNew<PrefabFactory<AsteroidContextPanel, Asteroid>>().WithArgument(_asteroidContextInfoPanelPrefab);
			binder.Bind<Factory<Asteroid, Asteroid.Arguments>>().ToNew<PrefabFactory<Asteroid, Asteroid.Arguments>>().WithArgument(_asteroidPrefab);
			binder.Bind<Factory<List<Asteroid>, int>>().ToNew<AsteroidsFactory>();
			binder.BindToNewSelf<SelectedAsteroid>().AsSingle();
			binder.BindInstance(_config).WithoutInjection();
		}
	}
}
