using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ActiveFlightPathSetter : MonoBehaviour, Injectable
    {
        private ActiveItem<CosmicObject> _selectedCosmicObject;
        private ActiveItem<Ship> _activeShip;
        private ActiveItem<FlightPath> _activePath;
        private FlightPathFinder _pathFinder;

        public void Inject(Resolver resolver)
        {
            _selectedCosmicObject = resolver.Resolve<ActiveItem<CosmicObject>>();
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
            _pathFinder = resolver.Resolve<FlightPathFinder>();
            _activePath = resolver.Resolve<ActiveItem<FlightPath>>();
        }

        private void OnEnable()
        {
            UpdateFlightPath();
            _selectedCosmicObject.OnValueChanged += OnSelectedCosmicObjectChanged;
            _activeShip.OnValueChanged += OnActiveShipChanged;
        }

        private void OnDisable()
        {
            _selectedCosmicObject.OnValueChanged -= OnSelectedCosmicObjectChanged;
            _activeShip.OnValueChanged -= OnActiveShipChanged;
        }

        private void OnSelectedCosmicObjectChanged(CosmicObject formervalue, CosmicObject newvalue)
        {
            UpdateFlightPath();
        }

        private void OnActiveShipChanged(Ship formervalue, Ship newvalue)
        {
            UpdateFlightPath();
        }

        private void UpdateFlightPath()
        {
            if (!_activeShip.HasValue || !_selectedCosmicObject.HasValue || _activeShip.Value.Location.Value == null)
            {
                _activePath.Value = null;
                return;
            }

            List<FlyTarget> targets = _pathFinder.GetPath(_activeShip.Value.FlightGraph,
                _activeShip.Value.Location.Value,
                _selectedCosmicObject.Value);
            _activePath.Value = targets.Count > 0 ? new FlightPath(targets) : null;
        }
    }
}
