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
        private Pool<PlayerColorSelectionItem, Color> _pool;

        private List<PlayerColorSelectionItem> _items = new List<PlayerColorSelectionItem>();

        public void Inject(Resolver resolver)
        {
            _playerSettings = resolver.Resolve<PlayerSettings>();
            _pool = resolver.Resolve<Pool<PlayerColorSelectionItem, Color>>();
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
            foreach (Color color in _playerSettings.PlayerColors)
                CreateItem(color);
        }

        private void CreateItem(Color color)
        {
            PlayerColorSelectionItem item = _pool.Request(color);
            item.Base.SetParent(_hook);
            _items.Add(item);
        }

        private void ReturnItems()
        {
            foreach (PlayerColorSelectionItem item in _items)
                Return(item);
            _items.Clear();
        }

        private void Return(PlayerColorSelectionItem item)
        {
            _pool.Return(item);
        }
    }
}
