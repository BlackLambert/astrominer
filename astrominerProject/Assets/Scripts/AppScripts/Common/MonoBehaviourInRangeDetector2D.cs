using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MonoBehaviourInRangeDetector2D<TItem> : MonoBehaviour, Injectable where TItem : MonoBehaviour
    {
        public event Action<TItem> OnItemCameInRange;
        public event Action<TItem> OnItemCameOutOffRange;

        public Observable<List<TItem>> ItemsInRange { get; private set; }

        private Provider<List<TItem>> _provider;
        private List<TItem> _itemsInRange = new List<TItem>();
        private List<TItem> _itemsNotInRange = new List<TItem>();
        private float _distance;
        private float _distanceSqr;
        private Transform _startPoint;

        public void Inject(Resolver resolver)
        {
            Arguments arguments = resolver.Resolve<Arguments>();
            _distance = arguments.Distance;
            _distanceSqr = _distance * _distance;
            _startPoint = arguments.StartPoint;
            _provider = resolver.Resolve<Provider<List<TItem>>>();
            _itemsNotInRange.AddRange(_provider.Value.Value);
            ItemsInRange = new Observable<List<TItem>>() { Value = _itemsInRange };
        }

        private void OnEnable()
        {
            CheckItemsInRange();
        }

        private void OnDisable()
        {
            _itemsInRange.Clear();
            _itemsNotInRange.Clear();
        }

        private void Update()
        {
            CheckItemsInRange();
        }

        private void CheckItemsInRange()
        {
            CheckForItemsLeftRange();
            CheckForNewItemsInRange();
        }

        private void CheckForItemsLeftRange()
        {
            for (int i = _itemsInRange.Count - 1; i >= 0; i--)
            {
                TItem item = _itemsInRange[i];
                if (!CheckItemInRange(item))
                {
                    _itemsInRange.RemoveAt(i);
                    _itemsNotInRange.Add(item);
                    OnItemCameOutOffRange?.Invoke(item);
                }
            }
        }

        private void CheckForNewItemsInRange()
        {
            for (int i = _itemsNotInRange.Count - 1; i >= 0; i--)
            {
                TItem item = _itemsNotInRange[i];
                if (CheckItemInRange(item))
                {
                    _itemsNotInRange.RemoveAt(i);
                    _itemsInRange.Add(item);
                    OnItemCameInRange?.Invoke(item);
                }
            }
        }

        private bool CheckItemInRange(TItem item)
        {
            return ((Vector2)item.transform.position - (Vector2)_startPoint.position).sqrMagnitude <= _distanceSqr;
        }

        public struct Arguments
        {
            public float Distance;
            public Transform StartPoint;
        }
    }
}