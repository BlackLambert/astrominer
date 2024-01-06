using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerColorSelectionItemCreator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Transform _hook;

        private PlayerSettings _playerSettings;
        private Pool<PlayerColorSelectionItem, PlayerColorOption> _pool;
        private ActiveItem<List<PlayerColorSelectionItem>> _items;

        public void Inject(Resolver resolver)
        {
            _playerSettings = resolver.Resolve<PlayerSettings>();
            _pool = resolver.Resolve<Pool<PlayerColorSelectionItem, PlayerColorOption>>();
            _items = resolver.Resolve<ActiveItem<List<PlayerColorSelectionItem>>>();
        }

        private void OnEnable()
        {
            CreateItems();
        }

        private void OnDisable()
        {
            ReturnItems();
        }

        private void CreateItems()
        {
            List<PlayerColorSelectionItem> items = new List<PlayerColorSelectionItem>();
            foreach (PlayerColorOption color in _playerSettings.PlayerColors)
            {
                items.Add(CreateItem(color));
            }

            _items.Value = items;
        }

        private PlayerColorSelectionItem CreateItem(PlayerColorOption color)
        {
            PlayerColorSelectionItem item = _pool.Request(color);
            item.Base.SetParent(_hook);
            return item;
        }

        private void ReturnItems()
        {
            if (!_items.HasValue)
            {
                return;
            }

            foreach (PlayerColorSelectionItem item in _items.Value)
            {
                Return(item);
            }
            
            _items.Value = null;
        }

        private void Return(PlayerColorSelectionItem item)
        {
            _pool.Return(item);
        }
    }
}
