using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class Ship : FlyableObject
	{
		private ShipSettings _settings;

		public float Range => _settings.ActionRadius;
		public LimitedObservableList<ExploitMachine> Machines { get; private set; }
		public Ores CollectedOres { get; private set; } = new Ores();
		public Player Player { get; private set; }

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_settings = resolver.Resolve<ShipSettings>();
			Machines = new LimitedObservableList<ExploitMachine>(_settings.InventorySpace);
			Player = resolver.Resolve<Player>();
		}

	}
}