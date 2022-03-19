using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField]
		private Ship _ship;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_ship).WithoutInjection();
			binder.BindInstance(new System.Random()).WithoutInjection();
			binder.BindToNewSelf<Selection>().AsSingle();
		}
	}
}
