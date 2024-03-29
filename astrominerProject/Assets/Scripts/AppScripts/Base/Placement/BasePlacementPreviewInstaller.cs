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
			binder.BindInstance(CreateBasePlacementContext());
			binder.BindInstance(CreateDetectorArguments());
			binder.Bind<ActionRange>().ToNew<ShipSettingsActionRange>().WithArgument(_shipSettings);
			binder.BindInstance(_base).WithoutInjection();
			binder.BindInstance(_player).WithoutInjection();
			binder.Bind<InRangeDetector2D<Asteroid>>().ToInstance(_detector).WithoutInjection();
			binder.Bind<Provider<IList<Asteroid>>>().ToInstance(CreateAsteroidsProvider()).WithoutInjection();
			binder.BindInstance(_collisionDetector, nameof(Base)).WithoutInjection();
		}

		private BasePlacementContext CreateBasePlacementContext()
		{
			BasePlacementContext result = new BasePlacementContext();
			bool isComputerPlayer = !_player.IsHuman;
			result.Started.Value = isComputerPlayer;
			result.Placed.Value = isComputerPlayer;
			result.PlacementIsValid.Value = isComputerPlayer;
			return result;
		}

		private InRangeDetectorArguments CreateDetectorArguments()
		{
			return new InRangeDetectorArguments()
			{
				Distance = new BasicProvider<float>(_shipSettings.ActionRadius),
				StartPoint = new TransformPosition2DProvider(_startPoint)
			};
		}

		private Provider<IList<Asteroid>> CreateAsteroidsProvider()
		{
			BasicProvider<IList<Asteroid>> result = new BasicProvider<IList<Asteroid>>(_map.Asteroids.Value);
			return result;
		}
	}
}
