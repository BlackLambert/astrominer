using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{

    public class ShipInstaller : MonoInstaller
    {
        [SerializeField]
        private ShipSettings _settings;
		[SerializeField]
		private Ship _ship;
		[SerializeField]
		private Mover _mover;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_settings).WithoutInjection();
			binder.BindInstance(new Mover.Arguments(_settings.SpeedPerSecond));
			binder.Bind<Ship>().And<Flyable>().To<Ship>().FromInstance(_ship).WithoutInjection();
			binder.BindInstance(_mover).WithoutInjection();
		}
	}
}