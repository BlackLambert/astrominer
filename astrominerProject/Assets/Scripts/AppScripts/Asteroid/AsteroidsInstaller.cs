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
			binder.Bind<Factory<ContextPanel<Asteroid>, Asteroid>>().
				ToNew<PrefabFactory<ContextPanel<Asteroid>, Asteroid>>().
				WithArgument<ContextPanel<Asteroid>>(_asteroidContextInfoPanelPrefab);
			binder.Bind<Pool<ContextPanel<Asteroid>, Asteroid>>().
				ToNew<MonoPool<ContextPanel<Asteroid>, Asteroid>>().
				WithArgument<ContextPanel<Asteroid>>(_asteroidContextInfoPanelPrefab).
				AsSingle();
			binder.Bind<Factory<Asteroid, Asteroid.Arguments>>().
				ToNew<PrefabFactory<Asteroid, Asteroid.Arguments>>().
				WithArgument(_asteroidPrefab);
			binder.Bind<Factory<List<Asteroid>, IEnumerable<Vector2>>>().ToNew<AsteroidsFactory>();
			binder.Bind<ActiveItem<Asteroid>>().ToNew<SelectedAsteroid>().AsSingle();
			binder.BindInstance(_config).WithoutInjection();
		}
	}
}
