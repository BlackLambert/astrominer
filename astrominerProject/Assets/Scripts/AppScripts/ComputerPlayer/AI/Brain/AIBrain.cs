using System;
using System.Collections.Generic;
using System.Linq;
using SBaier.DI;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class AIBrain : Injectable
    {
        public bool AnyIdentifiedUnoccupiedAsteroid => UnoccupiedAsteroidWithBestValue != null && 
                                                       ValueOfEmptyIdentifiedAsteroids > 0;
        public float Credits => Player.Credits.Amount;
        public bool HasExploitMachine => Ship.HasExploitMachine;
        public bool HasEmptyInventorySpace => Ship.HasEmptyInventorySpace;
        public FlyTarget Base => _base;
        public IReadOnlyList<Asteroid> OccupationTargets => _occupationTargets;
        public bool AnyOresStoredByAsteroids => _storedOresAmount > 0;
        public Asteroid UnoccupiedAsteroidWithBestValue => _occupationTargets.FirstOrDefault();
        public int ExploitersInInventoryAmount => Ship.Machines.Count;

        public Ship Ship { get; private set; }
        public Player Player { get; private set; }
        public float ValueOfEmptyIdentifiedAsteroids { get; private set; }
        public bool HasUnidentifiedAsteroid { get; private set; }
        public int ActiveProspectorDronesAmount { get; private set; }
        public bool AnyOwnedExploitedAsteroids { get; private set; }
        public float CostsOfExploitedAsteroids { get; private set; }
        public float ExploitedAsteroidsAmount { get; private set; }
        public Asteroid BestTakeExploiterTarget { get; private set; }
        public Ores OresToSell { get; private set; }

        private readonly Dictionary<ProspectorVesselType, Asteroid> _prospectTargets = new();
        private readonly Dictionary<CollectOresVesselType, Asteroid> _collectTargets = new();
        private readonly ExploitMachinePlacer _machinePlacer = new();
        private readonly ExploitMachineTaker _machineTake = new();
        private readonly List<Asteroid> _occupationTargets = new();

        private Dictionary<ProspectorVesselType, OptimalProspectTargetFinder> _prospectTargetFinders;
        private Dictionary<CollectOresVesselType, OptimalCollectOresTargetFinder> _collectOresTargetFinders;
        private OptimalExploitTargetFinder _exploitTargetFinder;
        private OptimalExploitersToBuyFinder _exploitersToBuyFinder;
        private OptimalTakeExploiterTargetFinder _takeExploiterTargetFinder;
        private OptimalOresToSellFinder _oresToSellFinder;
        private Map _map;
        private Base _base;
        private DroneBuyer<ProspectorDrone> _prospectorDroneBuyer;
        private DroneBuyer<CarrierDrone> _carrierDroneBuyer;
        private Random _random;
        private ExploitMachineSettings _exploitMachineSettings;
        private ExploitMachineVendor _exploitMachineVendor;
        private OreBank _oreBank;
        private float _storedOresAmount;

        public void Inject(Resolver resolver)
        {
            Ship = resolver.Resolve<Ship>();
            Player = Ship.Player;
            _map = resolver.Resolve<Map>();
            _prospectorDroneBuyer = resolver.Resolve<DroneBuyer<ProspectorDrone>>();
            _carrierDroneBuyer = resolver.Resolve<DroneBuyer<CarrierDrone>>();
            _base = resolver.Resolve<Base>();
            _random = resolver.Resolve<Random>();
            _exploitTargetFinder = resolver.Resolve<OptimalExploitTargetFinder>();
            _exploitMachineSettings = resolver.Resolve<ExploitMachineSettings>();
            _exploitMachineVendor = resolver.Resolve<ExploitMachineVendor>();
            _exploitersToBuyFinder = resolver.Resolve<OptimalExploitersToBuyFinder>();
            _takeExploiterTargetFinder = resolver.Resolve<OptimalTakeExploiterTargetFinder>();
            _oresToSellFinder = resolver.Resolve<OptimalOresToSellFinder>();
            _oreBank = resolver.Resolve<OreBank>();

            InitProspectTargetFinders(resolver);
            InitCollectTargetFinders(resolver);
        }

        public void Update()
        {
            ValueOfEmptyIdentifiedAsteroids = Ship.Player.IdentifiedAsteroids.GetValueOfEmptyAsteroids();
            HasUnidentifiedAsteroid = _map.HasUnidentifiedValuableAsteroid(Player);
            ActiveProspectorDronesAmount = Player.Drones.CountDronesOfType<ProspectorDrone>();
            AnyOwnedExploitedAsteroids = Player.OwnedAsteroids.Any(asteroid => asteroid.Exploited);
            CostsOfExploitedAsteroids = Player.OwnedAsteroids.GetExploitedCosts();
            ExploitedAsteroidsAmount = Player.OwnedAsteroids.GetExploitedAmount();
            OresToSell = _oresToSellFinder.Search();
            UpdateOccupationTargets();
            _collectTargets.Clear();
            _prospectTargets.Clear();
            _storedOresAmount = Player.OwnedAsteroids
                .Where(asteroid => !Player.Drones.ContainsDroneTo<CarrierDrone>(asteroid))
                .Sum(asteroid => asteroid.StoredMinedOres.GetTotal());
            BestTakeExploiterTarget = _takeExploiterTargetFinder.Search();
        }

        public bool CanAfford(float price)
        {
            return Player.Credits.Has(price);
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

        public Asteroid GetBestCollectTargetFor(CollectOresVesselType type)
        {
            if (_collectTargets.TryGetValue(type, out Asteroid result))
            {
                return result;
            }

            result = _collectOresTargetFinders[type].Search();
            _collectTargets[type] = result;
            return result;
        }

        public float GetTakeExploiterValueOf(Asteroid asteroid)
        {
            return _takeExploiterTargetFinder.GetTakeExploiterValueOf(asteroid);
        }

        public float GetProspectValueOf(ProspectorVesselType type, Asteroid asteroid)
        {
            return _prospectTargetFinders[type].GetProspectingValueOf(asteroid);
        }

        public float GetCollectOreValueOf(CollectOresVesselType type, Asteroid asteroid)
        {
            return _collectOresTargetFinders[type].GetCollectOreValueOf(asteroid);
        }

        public void SendDroneToBestProspectTarget()
        {
            Asteroid asteroid = GetBestProspectTargetFor(ProspectorVesselType.Drone);
            _prospectorDroneBuyer.BuyDrone(Ship, asteroid, _base);
        }

        public void SendDroneToBestCollectOresTarget()
        {
            Asteroid asteroid = GetBestCollectTargetFor(CollectOresVesselType.Drone);
            _carrierDroneBuyer.BuyDrone(Ship, asteroid, _base);
        }

        public FlyTarget GetRandomFlyTargetInRange()
        {
            IReadOnlyList<FlyTarget> flyTargetsInRange = Ship.FlightGraph.GetNeighborsOf(Ship.Location.Value);
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

        private void InitCollectTargetFinders(Resolver resolver)
        {
            _collectOresTargetFinders = new Dictionary<CollectOresVesselType, OptimalCollectOresTargetFinder>();
            _collectOresTargetFinders.Add(CollectOresVesselType.Drone,
                resolver.Resolve<OptimalCollectOresTargetFinder>(CollectOresVesselType.Drone));
            _collectOresTargetFinders.Add(CollectOresVesselType.Ship,
                resolver.Resolve<OptimalCollectOresTargetFinder>(CollectOresVesselType.Ship));
        }

        public void FlyTo(FlyTarget flyTarget)
        {
            Ship.FlyTo(flyTarget);
        }

        public float GetOccupationValueOf(Asteroid asteroid)
        {
            return _exploitTargetFinder.GetOccupationValueOf(asteroid);
        }

        public bool IsShipAtOccupationTarget()
        {
            return Ship.Location.Value as Asteroid == UnoccupiedAsteroidWithBestValue;
        }

        public bool IsShipAtBase()
        {
            return Ship.Location.Value is Base playerBase && playerBase.Player == Player;
        }

        public void PlaceExploiterOnLocation()
        {
            ExploitMachine machine = Ship.Machines.OrderByDescending(machine => machine.Power).First();
            _machinePlacer.PlaceMachine(Ship, machine);
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
                Ship.Machines.Add(_exploitMachineVendor.BuyMachine(Player, settings));
            }
        }

        public void SellOres()
        {
            Ores oresToSell = Ship.CollectedOres.Request(OresToSell);
            Player.Credits.Add(_oreBank.CalculateCreditsFor(oresToSell));
        }

        public void SellExploiter()
        {
            _exploitMachineVendor.SellMachine(Ship, Ship.Machines.First());
        }

        public float GetPendingCredits()
        {
            return Player.Drones.GetAllDronesOfType<CarrierDrone>().Sum(
                drone => drone.TargetReached
                    ? _oreBank.CalculateCreditsFor(drone.CollectedOres)
                    : _oreBank.CalculateCreditsFor(drone.Target.StoredMinedOres));
        }

        public float GetValueOfOresToSell()
        {
            return _oreBank.CalculateCreditsFor(OresToSell);
        }

        public float GetValueOfStoredAsteroidOres()
        {
            return Player.OwnedAsteroids.Where(asteroid => !Player.Drones.ContainsDroneTo<CarrierDrone>(asteroid))
                .Sum(asteroid => _oreBank.CalculateCreditsFor(asteroid.StoredMinedOres));
        }

        public float GetDistanceToBase()
        {
            return Ship.FlightMap.GetDistanceTo(_base);
        }

        public bool IsShipAt(FlyTarget flyTarget)
        {
            return Ship.Location.Value == flyTarget;
        }

        private void UpdateOccupationTargets()
        {
            _occupationTargets.Clear();
            _occupationTargets.AddRange(Ship.Player.IdentifiedAsteroids.GetEmptyValuableAsteroids());
            _occupationTargets.Sort(CompareOccupationTargets);
        }

        private int CompareOccupationTargets(Asteroid asteroid1, Asteroid asteroid2)
        {
            float value1 = _exploitTargetFinder.GetOccupationValueOf(asteroid1);
            float value2 = _exploitTargetFinder.GetOccupationValueOf(asteroid2);
            return value2.CompareTo(value1);
        }

        public void TakeMachine()
        {
            _machineTake.TakeMachine(Ship);
        }

        public float GetAllCredits()
        {
            return GetPendingCredits() + Ship.Player.Credits.Amount;
        }
    }
}