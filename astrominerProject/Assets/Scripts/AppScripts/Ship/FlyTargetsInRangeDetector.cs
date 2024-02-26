using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyTargetsInRangeDetector : MonoBehaviour, Injectable, InRangeDetector2D<FlyTarget>
    {
        public event Action<FlyTarget> OnItemCameInRange;
        public event Action<FlyTarget> OnItemCameOutOffRange;
        public IReadOnlyList<FlyTarget> ItemsInRange => _itemsInRange;
        public Provider<Vector2> StartPoint => _startPoint;

        private Arguments _arguments;
        private List<FlyTarget> _itemsInRange = new List<FlyTarget>();
        private Provider<Vector2> _startPoint;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<Arguments>();
            _startPoint = new BasicProvider<Vector2>(_arguments.Origin.Position2D);
        }

        private void OnEnable()
        {
            FindNeighbors();
        }

        private void OnDisable()
        {
            Clean();
        }

        private void FindNeighbors()
        {
            if (!_arguments.FlightGraph.ContainsKey(_arguments.Origin))
            {
                return;
            }
            
            foreach (FlyTarget flyTarget in _arguments.FlightGraph.Get(_arguments.Origin))
            {
                _itemsInRange.Add(flyTarget);
                OnItemCameInRange?.Invoke(flyTarget);
            }
        }

        private void Clean()
        {
            foreach (FlyTarget target in _itemsInRange)
            {
                OnItemCameOutOffRange?.Invoke(target);
            }
            
            _itemsInRange.Clear();
        }

        public class Arguments
        {
            public FlyTarget Origin;
            public FlightGraph FlightGraph;
        }
    }
}
