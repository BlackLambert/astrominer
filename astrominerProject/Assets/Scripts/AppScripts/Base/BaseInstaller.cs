using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BaseInstaller : MonoInstaller, Injectable
	{
		[SerializeField]
		private Selectable _selectable;
		[SerializeField]
		private Base _base;

		private Player _player;

		public void Inject(Resolver resolver)
		{
			_player = resolver.Resolve<Player>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_selectable).WithoutInjection();
			binder.BindInstance(_base.FlyTarget).WithoutInjection();
			binder.BindInstance(_base).WithoutInjection();
			binder.BindInstance(_player).WithoutInjection();
		}
	}
}
