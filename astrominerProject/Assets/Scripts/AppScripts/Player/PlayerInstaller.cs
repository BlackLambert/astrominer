using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField]
		private PlayerSettings _settings;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Factory<Player>>().ToNew<PlayerFactory>();
			Player player = CreatePlayer();
			binder.BindInstance(player).WithoutInjection();
			binder.BindInstance(player.IdentifiedAsteroids).WithoutInjection();
			binder.BindInstance(player.ProspectorDrones).WithoutInjection();
			ActivePlayer activePlayer = new ActivePlayer();
			activePlayer.Value = player;
			binder.Bind<ActiveItem<Player>>().To<ActivePlayer>().FromInstance(activePlayer);
		}

		private Player CreatePlayer()
		{
			Player result = new Player(_settings.PlayerColor);
			result.Credits.Add(_settings.StartCredits);
			return result;
		}
	}
}
