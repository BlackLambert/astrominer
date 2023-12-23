using SBaier.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AstroidsAmountSelectionItemsCreator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Transform _hook;

        private List<AstroidsAmountSelectionItem> _items = new List<AstroidsAmountSelectionItem>();
        private Pool<AstroidsAmountSelectionItem, AsteroidAmountOption> _pool;
        private MapCreationSettings _settings;
        private Selection _selection;

        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<AstroidsAmountSelectionItem, AsteroidAmountOption>>();
            _settings = resolver.Resolve<MapCreationSettings>();
            _selection = resolver.Resolve<Selection>();
        }

        private void OnEnable()
        {
            ClearItems();
            CreateItems();
        }

        private void CreateItems()
        {
            foreach (AsteroidAmountOption option in _settings.AsteroidAmountOptions)
            {
                AstroidsAmountSelectionItem item = _pool.Request(option);
                item.transform.SetParent(_hook, false);
                item.transform.localScale = Vector3.one;
                _items.Add(item);
            }
            _selection.Select(_items.FirstOrDefault()?.Selectable);
        }

        private void ClearItems()
        {
            foreach (AstroidsAmountSelectionItem item in _items)
            {
                _pool.Return(item);
            }

            _items.Clear();
        }
    }
}
