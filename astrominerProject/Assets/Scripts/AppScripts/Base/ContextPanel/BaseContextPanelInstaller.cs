using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BaseContextPanelInstaller : MonoInstaller, Injectable
	{
		[SerializeField] 
		private BaseContextPanel _contextPanel;
		
		private Base _base;
		private ActiveShip _activeShip;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<ContextPanel<Base>>().ToInstance(_contextPanel).WithoutInjection();
			binder.Bind<Base>().And<FlyTarget>().To<Base>().FromInstance(_base).WithoutInjection();
			binder.BindInstance(_base.Player).WithoutInjection();
			binder.BindInstance(_activeShip.Value).WithoutInjection();
		}

		void Injectable.Inject(Resolver resolver)
		{
			_base = resolver.Resolve<Base>();
			_activeShip = resolver.Resolve<ActiveShip>();
		}
	}
}
