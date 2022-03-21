using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField]
		private Color _playerColor = Color.red;

		public override void InstallBindings(Binder binder)
		{
			Player player = new Player(_playerColor);
			binder.BindInstance(player).WithoutInjection();
			binder.BindInstance(player.IdentifiedAsteroids).WithoutInjection();
			binder.BindInstance(player.ProspectorDrones).WithoutInjection();
			ActivePlayer activePlayer = new ActivePlayer();
			activePlayer.Value = player;
			binder.Bind<ActiveItem<Player>>().To<ActivePlayer>().FromInstance(activePlayer);
		}
	}
}
