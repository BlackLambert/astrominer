using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BasesInstaller : MonoInstaller
	{
		[SerializeField]
		private BaseContextPanel _contextInfoPanelPrefab;
		[SerializeField]
		private Base _basePrefab;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Factory<ContextPanel<Base>, Base>>().
				ToNew<PrefabFactory<ContextPanel<Base>, Base>>().
				WithArgument<ContextPanel<Base>>(_contextInfoPanelPrefab);
			binder.Bind<Pool<ContextPanel<Base>, Base>>().
				ToNew<MonoPool<ContextPanel<Base>, Base>>().
				WithArgument<ContextPanel<Base>>(_contextInfoPanelPrefab).
				AsSingle();
			binder.Bind<Factory<Base, Player>>().
				ToNew<PrefabFactory<Base, Player>>().
				WithArgument<Base>(_basePrefab);
			binder.Bind<Pool<Base, Player>>().
				ToNew<MonoPool<Base, Player>>().
				WithArgument<Base>(_basePrefab).
				AsSingle();
			binder.Bind<ActiveItem<Base>>().ToNew<SelectedBase>().AsSingle();
			binder.BindToNewSelf<Bases>().AsSingle();
		}
	}
}
