using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ConnectionDrawer<TItem> : MonoBehaviour, Injectable where TItem : Location2D
    {
        private Pool<Connection> _connectionPool;
        private InRangeDetector2D<TItem> _detector;
        private Dictionary<TItem, Connection> _asteroidToConnection = new Dictionary<TItem, Connection>();
        private Provider<Vector2> _startPoint;
        private bool _active = true;

        public void Inject(Resolver resolver)
        {
            _detector = resolver.Resolve<InRangeDetector2D<TItem>>();
            _connectionPool = resolver.Resolve<Pool<Connection>>();
            _startPoint = _detector.StartPoint;
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

        private void InitConnection()
        {
            if (!_active)
            {
                return;
            }
            
            foreach (TItem asteroid in _detector.ItemsInRange)
            {
                AddConnection(asteroid);
            }
            
            _detector.OnItemCameInRange += AddConnection;
            _detector.OnItemCameOutOffRange += RemoveConnection;
        }

        private void ClearConnections()
        {
            foreach (TItem asteroid in _asteroidToConnection.Keys)
            {
                _connectionPool.Return(_asteroidToConnection[asteroid]);
            }

            _asteroidToConnection.Clear();
            _detector.OnItemCameInRange -= AddConnection;
            _detector.OnItemCameOutOffRange -= RemoveConnection;
        }

        private void AddConnection(TItem asteroid)
        {
            Connection connection = _connectionPool.Request();
            Vector2 startPosition = _startPoint.Value;
            connection.transform.position = startPosition;
            connection.SetEndpoints(startPosition, asteroid.Position2D);
            connection.SetDefaultColor();
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
                pair.Value.SetEndpoints(_startPoint.Value, pair.Key.Position2D);
            }
        }
    }
}