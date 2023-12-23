using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BasePlacementPreviewInstaller : MonoInstaller, Injectable
	{
		[SerializeField]
		private BasePlacementPreview _base;

		[SerializeField] 
		private AsteroidsInRangeDetector _detector;

		[SerializeField] 
		private ShipSettings _shipSettings;

		[SerializeField] 
		private Transform _startPoint;

		[SerializeField] 
		private CollisionDetector2D _collisionDetector;

		private Player _player;
		private Map _map;

		public void Inject(Resolver resolver)
		{
			_player = resolver.Resolve<Player>();
			_map = resolver.Resolve<Map>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.BindToNewSelf<BasePlacementContext>().AsSingle();
			binder.BindInstance(new AsteroidsInRangeDetector.Arguments
				{ Distance = _shipSettings.ActionRadius, StartPoint = _startPoint });
			binder.Bind<ActionRange>().ToNew<ShipSettingsActionRange>().WithArgument(_shipSettings);
			binder.BindInstance(_base).WithoutInjection();
			binder.BindInstance(_player).WithoutInjection();
			binder.BindInstance(_detector).WithoutInjection();
			binder.Bind<Provider<List<Asteroid>>>().ToInstance(CreateAsteroidsProvider()).WithoutInjection();
			binder.BindInstance(_collisionDetector, nameof(Base)).WithoutInjection();
		}

		private Provider<List<Asteroid>> CreateAsteroidsProvider()
		{
			BasicProvider<List<Asteroid>> result = new BasicProvider<List<Asteroid>>();
			result.Value.Value = _map.Asteroids;
			return result;
		}
	}
}
