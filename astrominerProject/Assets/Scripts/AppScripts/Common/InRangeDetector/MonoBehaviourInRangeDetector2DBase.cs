using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MonoBehaviourInRangeDetector2DBase<TItem> : 
        MonoBehaviour, Injectable, InRangeDetector2D<TItem> 
        where TItem : MonoBehaviour
    {
        public event Action<TItem> OnItemCameInRange;
        public event Action<TItem> OnItemCameOutOffRange;

        public IReadOnlyList<TItem> ItemsInRange => _itemsInRange;
        public Provider<Vector2> StartPoint => _arguments.StartPoint;

        private float distanceSqr => _arguments.Distance.Value * _arguments.Distance.Value;
        private Vector2 startPoint => _arguments.StartPoint.Value;
        
        private Provider<IList<TItem>> _provider;
        private List<TItem> _itemsInRange = new List<TItem>();
        private List<TItem> _itemsNotInRange = new List<TItem>();
        private InRangeDetectorArguments _arguments;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<InRangeDetectorArguments>();
            _provider = resolver.Resolve<Provider<IList<TItem>>>();
            _itemsNotInRange.AddRange(_provider.Value);
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
            return ((Vector2)item.transform.position - startPoint).sqrMagnitude <= distanceSqr;
        }
    }
}