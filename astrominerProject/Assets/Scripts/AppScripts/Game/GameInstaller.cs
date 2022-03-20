using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField]
		private Ship _shipPrefab;
		[SerializeField]
		private Base _base;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Factory<Ship>>().ToNew<PrefabFactory<Ship>>().WithArgument(_shipPrefab);
			binder.BindInstance(new System.Random()).WithoutInjection();
			binder.BindToNewSelf<Selection>().AsSingle();
			binder.BindToNewSelf<ActiveShip>().AsSingle();
			binder.BindInstance(_base).WithoutInjection();
		}
	}
}
