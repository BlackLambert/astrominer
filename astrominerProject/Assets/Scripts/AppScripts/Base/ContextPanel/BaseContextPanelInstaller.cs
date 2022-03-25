using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BaseContextPanelInstaller : MonoInstaller, Injectable
	{
		[SerializeField]
		private ExploitMachineShop _shopPrefab;

		private Base _base;
		private ActiveShip _activeShip;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Base>().And<FlyTarget>().To<Base>().FromInstance(_base).WithoutInjection();
			binder.BindInstance(_activeShip.Value).WithoutInjection();
			binder.Bind<Factory<ExploitMachineShop>>().
				ToNew<PrefabFactory<ExploitMachineShop>>().
				WithArgument(_shopPrefab);
		}

		void Injectable.Inject(Resolver resolver)
		{
			_base = resolver.Resolve<Base>();
			_activeShip = resolver.Resolve<ActiveShip>();
		}
	}
}
