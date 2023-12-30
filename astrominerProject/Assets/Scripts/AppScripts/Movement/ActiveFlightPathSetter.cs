using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ActiveFlightPathSetter : MonoBehaviour, Injectable
    {
        private ActiveItem<CosmicObject> _selectedCosmicObject;
        private ActiveItem<Ship> _activeShip;
        private ActiveItem<IList<FlyTarget>> _activePath;
        private FlightPathFinder _pathFinder;

        public void Inject(Resolver resolver)
        {
            _selectedCosmicObject = resolver.Resolve<ActiveItem<CosmicObject>>();
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
            _pathFinder = resolver.Resolve<FlightPathFinder>();
            _activePath = resolver.Resolve<ActiveItem<IList<FlyTarget>>>();
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

            _activePath.Value = _pathFinder.GetPath(_activeShip.Value.FlightGraph, _activeShip.Value.Location.Value,
                _selectedCosmicObject.Value);
        }
    }
}
