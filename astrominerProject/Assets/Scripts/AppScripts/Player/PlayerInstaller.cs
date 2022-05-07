using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField]
		private PlayerSettings _settings;
		[SerializeField]
		private ShipInventoryPanel _shipInventoryPanelPrefab;
		[SerializeField]
		private CarryingOresPanel _carryingOresPanelPrefab;

		public override void InstallBindings(Binder binder)
		{
			Player player = CreatePlayer();
			binder.BindInstance(player).WithoutInjection();
			binder.BindInstance(player.IdentifiedAsteroids).WithoutInjection();
			binder.BindInstance(player.ProspectorDrones).WithoutInjection();
			ActivePlayer activePlayer = new ActivePlayer();
			activePlayer.Value = player;
			binder.Bind<ActiveItem<Player>>().To<ActivePlayer>().FromInstance(activePlayer);
			binder.Bind<Factory<ShipInventoryPanel, Ship>>().
				ToNew<PrefabFactory<ShipInventoryPanel, Ship>>().
				WithArgument(_shipInventoryPanelPrefab);
			binder.Bind<Pool<ShipInventoryPanel, Ship>>().
				ToNew<MonoPool<ShipInventoryPanel, Ship>>().
				WithArgument(_shipInventoryPanelPrefab).AsSingle();

			binder.Bind<Factory<CarryingOresPanel, Ship>>().
				ToNew<PrefabFactory<CarryingOresPanel, Ship>>().
				WithArgument(_carryingOresPanelPrefab);
			binder.Bind<Pool<CarryingOresPanel, Ship>>().
				ToNew<MonoPool<CarryingOresPanel, Ship>>().
				WithArgument(_carryingOresPanelPrefab).AsSingle();
		}

		private Player CreatePlayer()
		{
			Player result = new Player(System.Guid.NewGuid(), _settings.PlayerColors[0], "Player");
			result.Credits.Add(_settings.StartCredits);
			return result;
		}
	}
}
