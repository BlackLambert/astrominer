using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField]
		private Base _base;
		[SerializeField]
		private VisualsSettings _visualsSettings;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(new System.Random()).WithoutInjection();
			binder.BindToNewSelf<Selection>().AsSingle();
			binder.BindInstance(_base).WithoutInjection();
			binder.BindInstance(_visualsSettings).WithoutInjection();
		}
	}
}
