using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class Ship : FlyableObject
	{
		private ShipSettings _settings;

		public float Range => _settings.ActionRadius;
		
		public FlightGraph FlightGraph { get; set; }
		public LimitedObservableList<ExploitMachine> Machines { get; private set; }
		public Ores CollectedOres { get; private set; } = new Ores();
		public Player Player { get; private set; }
		public CosmicObjectInRangeDetector Detector { get; private set; }
		public int EmptyInventorySpace => _settings.InventorySpace - Machines.Count;
		public bool HasEmptyInventorySpace => EmptyInventorySpace > 0;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_settings = resolver.Resolve<ShipSettings>();
			Machines = new LimitedObservableList<ExploitMachine>(_settings.InventorySpace);
			Player = resolver.Resolve<Player>();
			Detector = resolver.Resolve<CosmicObjectInRangeDetector>();
		}
	}
}