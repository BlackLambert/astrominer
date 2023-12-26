using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{

    public class ShipInstaller : MonoInstaller, Injectable
    {
        [SerializeField]
        private ShipSettings _settings;
		[SerializeField]
		private Ship _ship;
		[SerializeField]
		private Mover _mover;

		private Player _player;

		public void Inject(Resolver resolver)
		{
			_player = resolver.Resolve<Player>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<ActionRange>().ToNew<ShipActionRange>();
			binder.BindInstance(_player.IdentifiedAsteroids).WithoutInjection();
			binder.BindInstance(_settings).WithoutInjection();
			binder.BindInstance(CreateMoverArguments());
			binder.Bind<Ship>().And<Flyable>().To<Ship>().FromInstance(_ship).WithoutInjection();
			binder.BindInstance(_mover).WithoutInjection();
			binder.BindInstance(_player).WithoutInjection();
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
    }
}