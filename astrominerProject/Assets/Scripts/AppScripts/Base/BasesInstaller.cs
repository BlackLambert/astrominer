using SBaier.DI;

namespace SBaier.Astrominer
{
	public class BasesInstaller : MonoInstaller
	{
		public override void InstallBindings(Binder binder)
		{
			binder.Bind<ActiveItem<Base>>().ToNew<SelectedBase>().AsSingle();
			binder.BindToNewSelf<Bases>().AsSingle();
		}
	}
}
