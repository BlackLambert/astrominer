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
        private List<PlayerColorSelectionItem> _items;

        public void Inject(Resolver resolver)
        {
            _playerSettings = resolver.Resolve<PlayerSettings>();
            _pool = resolver.Resolve<Pool<PlayerColorSelectionItem, PlayerColorOption>>();
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

            _items = items;
        }

        private PlayerColorSelectionItem CreateItem(PlayerColorOption color)
        {
            PlayerColorSelectionItem item = _pool.Request(color);
            item.Base.SetParent(_hook);
            return item;
        }

        private void ReturnItems()
        {
            if (_items == null)
            {
                return;
            }

            foreach (PlayerColorSelectionItem item in _items)
            {
                Return(item);
            }
            
            _items = null;
        }

        private void Return(PlayerColorSelectionItem item)
        {
            _pool.Return(item);
        }
    }
}
