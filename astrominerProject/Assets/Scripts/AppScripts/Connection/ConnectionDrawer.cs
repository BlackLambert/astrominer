using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ConnectionDrawer<TItem> : MonoBehaviour, Injectable, Initializable, Cleanable where TItem : Location2D
    {
        private Pool<Connection> _connectionPool;
        private InRangeDetector2D<TItem> _detector;
        private Dictionary<TItem, Connection> _asteroidToConnection = new Dictionary<TItem, Connection>();
        private Provider<Vector2> _startPoint;

        public void Inject(Resolver resolver)
        {
            _detector = resolver.Resolve<InRangeDetector2D<TItem>>();
            _connectionPool = resolver.Resolve<Pool<Connection>>();
            _startPoint = _detector.StartPoint;
        }

        public void Initialize()
        {
            InitConnection();
        }

        public void Clean()
        {
            ClearConnections();
        }

        private void Update()
        {
            UpdateConnections();
        }

        private void InitConnection()
        {
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
            if (_startPoint.Value.Equals(asteroid.Position2D))
            {
                return;
            }
            
            Connection connection = _connectionPool.Request();
            Transform trans = connection.transform; 
            trans.SetParent(transform);
            Vector2 startPosition = _startPoint.Value;
            trans.position = startPosition;
            connection.SetEndpoints(startPosition, asteroid.Position2D);
            connection.SetDefaultColor();
            _asteroidToConnection.Add(asteroid, connection);
        }

        private void RemoveConnection(TItem item)
        {
            if (!_asteroidToConnection.ContainsKey(item))
            {
                return;
            }
            
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