using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BaseInstaller : MonoInstaller, Injectable
	{
		[SerializeField]
		private Base _base;

		private Player _player;

		public void Inject(Resolver resolver)
		{
			_player = resolver.Resolve<Player>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Base>()
				.And<FlyTarget>()
				.And<CosmicObject>()
				.To<Base>()
				.FromInstance(_base)
				.WithoutInjection();
			
			binder.BindInstance(_player)
				.WithoutInjection();
		}
	}
}
