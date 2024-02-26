using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class FlightConnectionDrawer : MonoBehaviour, Injectable
    {
        private Pool<Connection> _connectionPool;

        private List<Connection> _connections = 
            new List<Connection>();
        
        public virtual void Inject(Resolver resolver)
        {
            _connectionPool = resolver.Resolve<Pool<Connection>>();
        }

        protected void UpdateConnections(FlightPath newValue)
        {
            ClearConnections();
            CreateConnections(newValue);
        }

        protected void UpdateConnections(FlightPath newValue, Color color)
        {
            ClearConnections();
            CreateConnections(newValue, color);
        }

        private void ClearConnections()
        {
            foreach (Connection connection in _connections)
            {
                _connectionPool.Return(connection);
            }
            _connections.Clear();
        }

        private void CreateConnections(FlightPath path, Color? color = null)
        {
            if (path == null || path.FlyTargets.Count <= 1)
            {
                return;
            }

            for (int i = 1; i < path.FlyTargets.Count; i++)
            {
                CreateConnection(path.FlyTargets[i - 1], path.FlyTargets[i], color);
            }
        }

        private void CreateConnection(FlyTarget start, FlyTarget end, Color? color)
        {
            Connection connection = _connectionPool.Request();
            connection.SetEndpoints(start.LandingPoint, end.LandingPoint);
            connection.SetColor(color);
            _connections.Add(connection);
        }
    }
}
