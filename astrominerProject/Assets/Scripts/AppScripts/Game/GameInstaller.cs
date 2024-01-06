using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField]
		private MiningSettings _miningSettings;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_miningSettings);
			binder.Bind<ActiveItem<Player>>().ToNew<ActivePlayer>().AsSingle();
			binder.Bind<ActiveItem<CosmicObject>>().ToNew<SelectedCosmicObject>().AsSingle();
		}
	}
}
