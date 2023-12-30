using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class ShipsCreator : MonoBehaviour, Injectable
    {
        private const float _mapSpawnDistanceAdditionFactor = 0.1f;

        [SerializeField] 
        private Transform _hook;
        
        private Bases _bases;
        private Pool<Ship, Player> _pool;
        private Ships _ships;
        private Random _random;
        private Map _map;
        private Provider<IList<FlyTarget>> _flyTargetsProvider;

        public void Inject(Resolver resolver)
        {
            _bases = resolver.Resolve<Bases>();
            _pool = resolver.Resolve<Pool<Ship, Player>>();
            _ships = resolver.Resolve<Ships>();
            _random = resolver.Resolve<Random>();
            _map = resolver.Resolve<Map>();
            _flyTargetsProvider = resolver.Resolve<Provider<IList<FlyTarget>>>();
        }

        private void Start()
        {
            CreateShips();
            _bases.OnItemAdded += OnBaseAdded;
        }

        private void OnDestroy()
        {
            _bases.OnItemAdded -= OnBaseAdded;
        }

        private void OnBaseAdded(KeyValuePair<Player, Base> pair)
        {
            CreateShip(pair);
        }

        private void CreateShips()
        {
            foreach (KeyValuePair<Player,Base> pair in _bases)
            {
                CreateShip(pair);
            }
        }

        private void CreateShip(KeyValuePair<Player, Base> pair)
        {
            Player player = pair.Key;
            Base playerBase = pair.Value;

            Ship ship = _pool.Request(player);
            _ships.Values.Add(ship);
            Vector2 mapSize = _map.AsteroidAmountOption.Value.MapSize;
            float maxMapSide = mapSize.x > mapSize.y ? mapSize.x : mapSize.y;
            float radius = maxMapSide + maxMapSide * _mapSpawnDistanceAdditionFactor;
            float angle = (float)_random.NextDouble() * 360f;
            Vector2 distanceVector = Quaternion.AngleAxis(angle, new Vector3(0,0, 1)) * Vector2.up * radius;
            Transform shipTransform = ship.transform;
            shipTransform.SetParent(_hook, false);
            shipTransform.position = (Vector2)playerBase.transform.position + distanceVector;
            ship.FlightGraph = FlightGraph.GenerateFor(_flyTargetsProvider.Value, ship.Range);
            ship.FlyTo(playerBase);
        }
    }
}
