using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField]
		private Base _base;
		[SerializeField]
		private MiningSettings _miningSettings;

		public override void InstallBindings(Binder binder)
		{
			binder.BindToNewSelf<Selection>().AsSingle();
			binder.BindInstance(_base).WithoutInjection();
			binder.BindInstance(_miningSettings);
		}
	}
}
