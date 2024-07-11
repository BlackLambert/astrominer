using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class ShipsCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private const float _mapSpawnDistanceAdditionFactor = 0.1f;

        [SerializeField] private Transform _hook;

        private Bases _bases;
        private Pool<Ship, Ship.Arguments, PrefabInstantiationArguments> _pool;
        private Ships _ships;
        private Random _random;
        private Map _map;
        private Provider<IList<FlyTarget>> _flyTargetsProvider;
        private ShipSettings _settings;

        public void Inject(Resolver resolver)
        {
            _bases = resolver.Resolve<Bases>();
            _pool = resolver.Resolve<Pool<Ship, Ship.Arguments, PrefabInstantiationArguments>>();
            _ships = resolver.Resolve<Ships>();
            _random = resolver.Resolve<Random>();
            _map = resolver.Resolve<Map>();
            _flyTargetsProvider = resolver.Resolve<Provider<IList<FlyTarget>>>();
            _settings = resolver.Resolve<ShipSettings>();
        }

        public void Initialize()
        {
            CreateShips();
            _bases.OnItemAdded += OnBaseAdded;
        }

        public void Clean()
        {
            _bases.OnItemAdded -= OnBaseAdded;
            foreach (Ship ship in _ships.Values)
            {
                _pool.Return(ship);
            }
        }

        private void OnBaseAdded(KeyValuePair<Player, Base> pair)
        {
            CreateShip(pair);
        }

        private void CreateShips()
        {
            foreach (KeyValuePair<Player, Base> pair in _bases)
            {
                CreateShip(pair);
            }
        }

        private void CreateShip(KeyValuePair<Player, Base> pair)
        {
            Player player = pair.Key;
            Base playerBase = pair.Value;
            Ship.Arguments arguments = new Ship.Arguments() { Player = player };
            IList<FlyTarget> flyTargets = _flyTargetsProvider.Value;
            arguments.FlightGraph = FlightGraph.GenerateFor(_flyTargetsProvider.Value, _settings.ActionRadius, player);
            arguments.FlightMap = new FlightMap(flyTargets);
            arguments.BaseFlightMap = new FlightMap(flyTargets);
            arguments.BaseFlightMap.UpdateFor(arguments.FlightGraph, playerBase);
            Ship ship = _pool.Request(arguments,
                new PrefabInstantiationArguments() { Parent = _hook, Position = GetPosition(playerBase) });
            player.Ship.Value = ship;
            _ships.Values.Add(ship);
            ship.FlyTo(new FlightPath(new List<FlyTarget>() { playerBase, playerBase }));
        }

        private Vector2 GetPosition(Base playerBase)
        {
            Vector2 mapSize = _map.AsteroidAmountOption.Value.MapSize;
            float maxMapSide = mapSize.x > mapSize.y ? mapSize.x : mapSize.y;
            float radius = maxMapSide + maxMapSide * _mapSpawnDistanceAdditionFactor;
            float angle = (float)_random.NextDouble() * 360f;
            Vector2 distanceVector = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * Vector2.up * radius;
            return (Vector2)playerBase.transform.position + distanceVector;
        }
    }
}