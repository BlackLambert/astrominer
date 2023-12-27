using System;
using System.Linq;
using SBaier.DI;
using UnityEngine;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class BasicBasePositionGetter : BasePositionGetter, Injectable
    {
        private Map _map;
        private Arguments _arguments;
        private Random _random;
        private BasePositions _basePositions;
        private int _trys = 0;
        private ShipSettings _shipSettings;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _arguments = resolver.Resolve<Arguments>();
            _random = resolver.Resolve<Random>();
            _shipSettings = resolver.Resolve<ShipSettings>();
            _basePositions = resolver.Resolve<BasePositions>();
        }
        
        public Vector2 GetFor(Player player)
        {
            _trys = 0;
            return GetInternal(player);
        }

        private Vector2 GetInternal(Player player)
        {
            _trys++;
            Vector2 position = GetPositionOnMap();
            
            if (IsValidPosition(position))
            {
                Debug.Log($"Found valid base position for player {player.Name} after {_trys} trys.");
                return position;
            }

            if (_trys > _arguments.RetryAmount)
            {
                throw new InvalidOperationException($"Failed to find a valid base position for player {player.Name}");
            }

            return GetInternal(player);
        }

        private Vector2 GetPositionOnMap()
        {
            Vector2 mapSize = _map.AsteroidAmountOption.Value.MapSize;
            Vector2 bottomLeft = _map.BottomLeftPoint;
            float x = bottomLeft.x + (float)_random.NextDouble() * mapSize.x;
            float y = bottomLeft.y + (float)_random.NextDouble() * mapSize.y;
            return new Vector2(x, y);
        }

        private bool IsValidPosition(Vector2 position)
        {
            return _basePositions.Values.All(pos => (position - pos).magnitude >= _arguments.MinBaseDistance) &&
                   AreAsteroidsInRangeValid(position);
        }

        private bool AreAsteroidsInRangeValid(Vector2 position)
        {
            int asteroidsInRangeCount = 0;
            foreach (Asteroid asteroid in _map.Asteroids.Value)
            {
                float distance = ((Vector2)asteroid.transform.position - position).magnitude;
                if (distance < _arguments.MinDistanceToObjects)
                {
                    return false;
                }

                if (distance < _shipSettings.ActionRadius)
                {
                    asteroidsInRangeCount++;
                }
            }

            return asteroidsInRangeCount >= _arguments.MinAmountOfAsteroidsInRange;
        }

        public class Arguments
        {
            public int RetryAmount;
            public float MinDistanceToObjects;
            public float MinBaseDistance;
            public int MinAmountOfAsteroidsInRange;
        }
    }
}
