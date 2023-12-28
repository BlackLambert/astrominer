using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ConnectionDrawer<TItem> : MonoBehaviour, Injectable where TItem : MonoBehaviour
    {
        private Pool<Connection> _connectionPool;
        private MonoBehaviourInRangeDetector2D<TItem> _asteroidsDetector;
        private Dictionary<TItem, Connection> _asteroidToConnection = new Dictionary<TItem, Connection>();
        private Provider<Vector2> _startPoint;
        private bool _active = true;

        public void Inject(Resolver resolver)
        {
            _asteroidsDetector = resolver.Resolve<MonoBehaviourInRangeDetector2D<TItem>>();
            _connectionPool = resolver.Resolve<Pool<Connection>>();
            _startPoint = resolver.Resolve<MonoBehaviourInRangeDetector2D<TItem>.Arguments>().StartPoint;
        }

        private void OnEnable()
        {
            InitConnection();
        }

        private void OnDisable()
        {
            ClearConnections();
        }

        private void Update()
        {
            UpdateConnections();
        }

        public void Activate(bool active)
        {
            if (_active == active)
            {
                return;
            }
            
            ClearConnections();
            _active = active;
            InitConnection();
        }

        private void InitConnection()
        {
            if (!_active)
            {
                return;
            }
            
            foreach (TItem asteroid in _asteroidsDetector.ItemsInRange)
            {
                AddConnection(asteroid);
            }
            
            _asteroidsDetector.OnItemCameInRange += AddConnection;
            _asteroidsDetector.OnItemCameOutOffRange += RemoveConnection;
        }

        private void ClearConnections()
        {
            foreach (TItem asteroid in _asteroidToConnection.Keys)
            {
                _connectionPool.Return(_asteroidToConnection[asteroid]);
            }

            _asteroidToConnection.Clear();
            _asteroidsDetector.OnItemCameInRange -= AddConnection;
            _asteroidsDetector.OnItemCameOutOffRange -= RemoveConnection;
        }

        private void AddConnection(TItem asteroid)
        {
            Connection connection = _connectionPool.Request();
            Vector2 startPosition = _startPoint.Value;
            connection.transform.position = startPosition;
            connection.SetEndpoints(startPosition, asteroid.transform.position);
            _asteroidToConnection.Add(asteroid, connection);
        }

        private void RemoveConnection(TItem item)
        {
            _connectionPool.Return(_asteroidToConnection[item]);
            _asteroidToConnection.Remove(item);
        }

        private void UpdateConnections()
        {
            foreach (KeyValuePair<TItem, Connection> pair in _asteroidToConnection)
            {
                pair.Value.SetEndpoints(_startPoint.Value, pair.Key.transform.position);
            }
        }
    }
}