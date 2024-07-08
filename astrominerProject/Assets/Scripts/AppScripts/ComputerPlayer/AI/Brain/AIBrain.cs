using System;
using System.Collections.Generic;
using System.Linq;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AIBrain : Injectable
    {
        public bool AnyIdentifiedUnoccupiedAsteroid => ValueOfEmptyIdentifiedAsteroids > 0;
        public float Credits => _player.Credits.Amount;
        public bool HasExploitMachine => _ship.HasExploitMachine;
        public bool HasEmptyInventorySpace => _ship.HasEmptyInventorySpace;
        public FlyTarget Base => _base;
        public IReadOnlyList<Asteroid> OccupationTargets => _occupationTargets;

        public float ValueOfEmptyIdentifiedAsteroids { get; private set; }
        public bool HasUnidentifiedAsteroid { get; private set; }
        public int ActiveProspectorDronesAmount { get; private set; }

        private readonly Dictionary<ProspectorVesselType, Asteroid> _prospectTargets = new();
        private readonly ExploitMachinePlacer _machinePlacer = new();
        private readonly List<Asteroid> _occupationTargets = new();

        private Dictionary<ProspectorVesselType, OptimalProspectTargetFinder> _prospectTargetFinders;
        private OptimalExploitTargetFinder _exploitTargetFinder;
        private OptimalExploitersToBuyFinder _exploitersToBuyFinder;
        private Ship _ship;
        private Player _player;
        private Map _map;
        private Base _base;
        private DroneBuyer<ProspectorDrone> _buyer;
        private Random _random;
        private Asteroid _unoccupiedAsteroidWithBestValue;
        private ExploitMachineSettings _exploitMachineSettings;
        private ExploitMachineVendor _exploitMachineVendor;

        public void Inject(Resolver resolver)
        {
            _ship = resolver.Resolve<Ship>();
            _player = _ship.Player;
            _map = resolver.Resolve<Map>();
            _buyer = resolver.Resolve<DroneBuyer<ProspectorDrone>>();
            _base = resolver.Resolve<Base>();
            _random = resolver.Resolve<Random>();
            _exploitTargetFinder = resolver.Resolve<OptimalExploitTargetFinder>();
            _exploitMachineSettings = resolver.Resolve<ExploitMachineSettings>();
            _exploitMachineVendor = resolver.Resolve<ExploitMachineVendor>();
            _exploitersToBuyFinder = resolver.Resolve<OptimalExploitersToBuyFinder>();

            InitProspectTargetFinders(resolver);
        }

        public void Update()
        {
            ValueOfEmptyIdentifiedAsteroids = _ship.Player.IdentifiedAsteroids.GetValueOfEmptyAsteroids();
            HasUnidentifiedAsteroid = _map.HasUnidentifiedAsteroid(_player);
            ActiveProspectorDronesAmount = _player.Drones.CountDronesOfType<ProspectorDrone>();
            UpdateOccupationTargets();
            _prospectTargets.Clear();
            _unoccupiedAsteroidWithBestValue = null;
        }

        public bool CanAfford(float price)
        {
            return _player.Credits.Has(price);
        }

        public Asteroid GetBestProspectTargetFor(ProspectorVesselType type)
        {
            if (_prospectTargets.TryGetValue(type, out Asteroid result))
            {
                return result;
            }

            result = _prospectTargetFinders[type].Search();
            _prospectTargets[type] = result;
            return result;
        }

        public float GetProspectValueOf(ProspectorVesselType type, Asteroid asteroid)
        {
            return _prospectTargetFinders[type].GetProspectingValueOf(asteroid);
        }

        public void SendDroneToBestProspectTarget()
        {
            Asteroid asteroid = GetBestProspectTargetFor(ProspectorVesselType.Drone);
            _ship.Player.Drones.Add(_buyer.BuyDrone(_ship, asteroid, _base));
        }

        public FlyTarget GetRandomFlyTargetInRange()
        {
            IReadOnlyList<FlyTarget> flyTargetsInRange = _ship.FlightGraph.GetNeighborsOf(_ship.Location.Value);
            int randomIndex = _random.Next(flyTargetsInRange.Count);
            return flyTargetsInRange[randomIndex];
        }

        private void InitProspectTargetFinders(Resolver resolver)
        {
            _prospectTargetFinders = new Dictionary<ProspectorVesselType, OptimalProspectTargetFinder>();
            _prospectTargetFinders.Add(ProspectorVesselType.Drone,
                resolver.Resolve<OptimalProspectTargetFinder>(ProspectorVesselType.Drone));
            _prospectTargetFinders.Add(ProspectorVesselType.Ship,
                resolver.Resolve<OptimalProspectTargetFinder>(ProspectorVesselType.Ship));
        }

        public void FlyTo(FlyTarget flyTarget)
        {
            _ship.FlyTo(flyTarget);
        }

        public Asteroid GetUnoccupiedAsteroidWithBestValue()
        {
            return _unoccupiedAsteroidWithBestValue ??= _exploitTargetFinder.Search();
        }

        public float GetOccupationValueOf(Asteroid asteroid)
        {
            return _exploitTargetFinder.GetOccupationValueOf(asteroid);
        }

        public bool IsShipAtOccupationTarget()
        {
            return _ship.Location.Value as Asteroid == GetUnoccupiedAsteroidWithBestValue();
        }

        public bool IsShipAtBase()
        {
            return _ship.Location.Value is Base playerBase && playerBase.Player == _player;
        }

        public void PlaceExploiterOnLocation()
        {
            ExploitMachine machine = _ship.Machines.OrderByDescending(machine => machine.Power).First();
            _machinePlacer.PlaceMachine(_ship, machine);
        }

        public float GetPriceOfLeastExpensiveExploiter()
        {
            return _exploitMachineSettings.Levels.Min(level => level.Price);
        }

        public void BuyExploiter()
        {
            List<ExploitMachineLevelSettings> machinesToBuy = _exploitersToBuyFinder.Search(_occupationTargets);

            if (machinesToBuy.Count == 0)
            {
                throw new InvalidOperationException("Failed to by exploiters");
            }
            
            foreach (ExploitMachineLevelSettings settings in machinesToBuy)
            {
                _ship.Machines.Add(_exploitMachineVendor.BuyMachine(_player, settings));
            }
        }

        private void UpdateOccupationTargets()
        {
            _occupationTargets.Clear();
            _occupationTargets.AddRange(_ship.Player.IdentifiedAsteroids.GetEmptyValuableAsteroids());
            _occupationTargets.Sort(CompareOccupationTargets);
        }

        private int CompareOccupationTargets(Asteroid asteroid1, Asteroid asteroid2)
        {
            float value1 = _exploitTargetFinder.GetOccupationValueOf(asteroid1);
            float value2 = _exploitTargetFinder.GetOccupationValueOf(asteroid2);
            return value1.CompareTo(value2);
        }
    }
}