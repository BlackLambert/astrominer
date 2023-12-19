using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BasePlacementPreviewInstaller : MonoInstaller, Injectable
	{
		[SerializeField]
		private BasePlacementPreview _base;

		[SerializeField] 
		private ShipSettings _shipSettings;

		private Player _player;

		public void Inject(Resolver resolver)
		{
			_player = resolver.Resolve<Player>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<ActionRange>().ToNew<ShipActionRange>();
			binder.BindInstance(_base).WithoutInjection();
			binder.BindInstance(_player).WithoutInjection();
		}
	}
}
