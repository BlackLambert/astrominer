using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidConnectionDrawer : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Transform _startPoint;
        
        private Pool<Connection> _connectionPool;
        private AsteroidsInRangeDetector _asteroidsDetector;
        private Dictionary<Asteroid, Connection> _asteroidToConnection = new Dictionary<Asteroid, Connection>();

        public void Inject(Resolver resolver)
        {
            _asteroidsDetector = resolver.Resolve<AsteroidsInRangeDetector>();
            _connectionPool = resolver.Resolve<Pool<Connection>>();
        }

        private void OnEnable()
        {
            InitConnection();
            _asteroidsDetector.OnItemCameInRange += AddConnection;
            _asteroidsDetector.OnItemCameOutOffRange += RemoveConnection;
        }

        private void OnDisable()
        {
            ClearConnections();
            _asteroidsDetector.OnItemCameInRange -= AddConnection;
            _asteroidsDetector.OnItemCameOutOffRange -= RemoveConnection;
        }

        private void Update()
        {
            UpdateConnections();
        }

        private void InitConnection()
        {
            foreach (Asteroid asteroid in _asteroidsDetector.ItemsInRange.Value)
            {
                AddConnection(asteroid);
            }
        }

        private void ClearConnections()
        {
            foreach (Asteroid asteroid in _asteroidToConnection.Keys)
            {
                _connectionPool.Return(_asteroidToConnection[asteroid]);
            }
            _asteroidToConnection.Clear();
        }

        private void AddConnection(Asteroid asteroid)
        {
            Connection connection = _connectionPool.Request();
            connection.transform.position = _startPoint.position;
            connection.SetEndpoints(_startPoint.position, asteroid.Base.position);
            _asteroidToConnection.Add(asteroid, connection);
        }

        private void RemoveConnection(Asteroid asteroid)
        {
            _connectionPool.Return(_asteroidToConnection[asteroid]);
            _asteroidToConnection.Remove(asteroid);
        }

        private void UpdateConnections()
        {
            foreach (KeyValuePair<Asteroid,Connection> pair in _asteroidToConnection)
            {
                pair.Value.SetEndpoints(_startPoint.position, pair.Key.Base.position);
            }
        }
    }
}
