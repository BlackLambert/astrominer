using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlightConnectionDrawer : MonoBehaviour, Injectable
    {
        private ActiveItem<IList<FlyTarget>> _activePath;
        private Pool<Connection> _connectionPool;

        private List<Connection> _currentConnections = new List<Connection>();
        
        public void Inject(Resolver resolver)
        {
            _activePath = resolver.Resolve<ActiveItem<IList<FlyTarget>>>();
            _connectionPool = resolver.Resolve<Pool<Connection>>();
        }

        private void OnEnable()
        {
            UpdateConnections();
            _activePath.OnValueChanged += OnActivePathChanged;
        }

        private void OnDisable()
        {
            _activePath.OnValueChanged -= OnActivePathChanged;
        }

        private void OnActivePathChanged(IList<FlyTarget> formervalue, IList<FlyTarget> newvalue)
        {
            UpdateConnections();
        }

        private void UpdateConnections()
        {
            ClearConnections();
            CreateConnections();
        }

        private void ClearConnections()
        {
            if (_currentConnections.Count == 0)
            {
                return;
            }

            foreach (Connection connection in _currentConnections)
            {
                _connectionPool.Return(connection);
            }
            _currentConnections.Clear();
        }

        private void CreateConnections()
        {
            if (!_activePath.HasValue)
            {
                return;
            }

            IList<FlyTarget> path = _activePath.Value;

            if (path.Count <= 1)
            {
                return;
            }

            FlyTarget start;
            FlyTarget end;

            for (int i = 1; i < path.Count; i++)
            {
                start = path[i - 1];
                end = path[i];
                Connection connection = _connectionPool.Request();
                connection.SetEndpoints(start.LandingPoint, end.LandingPoint);
                _currentConnections.Add(connection);
            }
        }
    }
}
