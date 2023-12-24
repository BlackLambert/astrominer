using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class ShipsCreator : MonoBehaviour, Injectable
    {
        private const float _mapSpawnDistanceAdditionFactor = 0.5f;

        [SerializeField] 
        private Transform _hook;
        
        private Map _map;
        private Pool<Ship, Player> _pool;
        private Ships _ships;
        private Random _random;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _pool = resolver.Resolve<Pool<Ship, Player>>();
            _ships = resolver.Resolve<Ships>();
            _random = resolver.Resolve<Random>();
        }

        private void Start()
        {
            CreateShips();
            _map.Bases.OnValueChanged += OnBasesChanged;
        }

        private void OnDestroy()
        {
            _map.Bases.OnValueChanged -= OnBasesChanged;
        }

        private void OnBasesChanged(Dictionary<Player, Base> formervalue, Dictionary<Player, Base> newvalue)
        {
            CreateShips();
        }

        private void CreateShips()
        {
            if (_map.Bases.Value == null)
            {
                return;
            }

            foreach (KeyValuePair<Player,Base> pair in _map.Bases.Value)
            {
                Player player = pair.Key;
                Base playerBase = pair.Value;

                Ship ship = _pool.Request(player);
                ship.FlyTo(playerBase);
                _ships.Values.Add(ship);
                Vector2 mapSize = _map.AsteroidAmountOption.Value.MapSize;
                float maxMapSide = mapSize.x > mapSize.y ? mapSize.x : mapSize.y;
                float radius = maxMapSide + maxMapSide * _mapSpawnDistanceAdditionFactor;
                float angle = (float)_random.NextDouble() * 360f;
                Vector2 distanceVector = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.up * radius;
                Transform shipTransform = ship.transform;
                shipTransform.SetParent(_hook, false);
                shipTransform.position = (Vector2)playerBase.transform.position + distanceVector;
            }
        }
    }
}
