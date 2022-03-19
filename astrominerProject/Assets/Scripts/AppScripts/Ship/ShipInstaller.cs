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
			binder.BindInstance(_settings);
			binder.BindInstance(_ship).WithoutInjection();
			binder.BindInstance(_mover).WithoutInjection();
		}
	}
}