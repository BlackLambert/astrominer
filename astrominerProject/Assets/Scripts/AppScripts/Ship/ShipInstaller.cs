using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{

    public class ShipInstaller : MonoInstaller, Injectable
    {
		[SerializeField]
		private Ship _ship;
		[SerializeField]
		private Mover _mover;

		private Player _player;
		private ShipSettings _settings;

		public void Inject(Resolver resolver)
		{
			_player = resolver.Resolve<Ship.Arguments>().Player;
			_settings = resolver.Resolve<ShipSettings>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<ActionRange>().ToNew<ShipActionRange>();
			binder.BindInstance(_player.IdentifiedAsteroids).WithoutInjection();
			binder.BindInstance(_settings).WithoutInjection();
			binder.BindInstance(CreateMoverArguments());
			binder.Bind<Ship>().And<Flyable>().And<FlyableObject>().To<Ship>().FromInstance(_ship).WithoutInjection();
			binder.BindInstance(_mover).WithoutInjection();
			binder.BindInstance(_player).WithoutInjection();
			binder.BindInstance(CreateDetectorArguments()).WithoutInjection();
			binder.BindToNewSelf<FlightPathMover>().AsSingle();
			binder.BindInstance(_ship.CollectedOres).WithoutInjection();
		}

		private Mover.Arguments CreateMoverArguments()
		{
			return new Mover.Arguments()
			{
				Acceleration = _settings.Acceleration,
				BreakForce = _settings.BreakForce,
				MaximalSpeed = _settings.MaxSpeedPerSecond
			};
		}

		private InRangeDetectorArguments CreateDetectorArguments()
		{
			return new InRangeDetectorArguments()
			{
				StartPoint = new TransformPosition2DProvider(_ship.transform),
				Distance = new ShipRangeProvider(_ship)
			};
		}
    }
}