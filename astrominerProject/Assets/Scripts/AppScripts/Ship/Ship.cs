using System.Collections.Generic;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class Ship : FlyableObject
    {
        private ShipSettings _settings;

        public float Range => _settings.ActionRadius;

        public FlightGraph FlightGraph { get; private set; }
        public FlightMap FlightMap { get; private set; }
        public FlightMap BaseFlightMap { get; private set; }
        public LimitedObservableList<ExploitMachine> Machines { get; private set; }
        public Ores CollectedOres { get; private set; } = new Ores();
        public Player Player { get; private set; }
        public int EmptyInventorySpace => _settings.InventorySpace - Machines.Count;
        public bool HasExploitMachine => Machines.Count > 0;
        public bool HasEmptyInventorySpace => EmptyInventorySpace > 0;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _settings = resolver.Resolve<ShipSettings>();
            Machines = new LimitedObservableList<ExploitMachine>(_settings.InventorySpace);

            Arguments arguments = resolver.Resolve<Arguments>();
            Player = arguments.Player;
            FlightMap = arguments.FlightMap;
            BaseFlightMap = arguments.BaseFlightMap;
            FlightGraph = arguments.FlightGraph;
        }

        public override void Clean()
        {
            base.Clean();
            CollectedOres.RequestAll();
            Machines.Clear();
        }

        public void FlyTo(FlyTarget flyTarget)
        {
            FlyTo(new FlightPath(FlightMap.FlyTargetToPath[flyTarget]));
        }

        protected override void OnTargetReached()
        {
            FlyTarget flyTarget = FlyTarget.Value.LastTarget;
            FlightMap.UpdateFor(FlightGraph, flyTarget);
            base.OnTargetReached();
        }

        public class Arguments
        {
            public Player Player { get; set; }
            public FlightMap FlightMap { get; set; }
            public FlightMap BaseFlightMap { get; set; }
            public FlightGraph FlightGraph { get; set; }
        }
    }
}