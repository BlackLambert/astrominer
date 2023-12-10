using SBaier.DI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerListItemsCreator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private RectTransform _hook;

        private Pool<PlayerListItem, Player> _pool;
        private Players _players;

        private List<PlayerListItem> _items = new List<PlayerListItem>();

        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<PlayerListItem, Player>>();
            _players = resolver.Resolve<Players>();
        }

        private void OnEnable()
        {
            CreateItems();
            _players.OnItemAdded += AddItem;
            _players.OnItemRemoved += RemoveItem;
        }

        private void OnDisable()
        {
            ReturnItems();
            _players.OnItemAdded += AddItem;
            _players.OnItemRemoved += RemoveItem;
        }

        private void CreateItems()
        {
            foreach (Player player in _players.ToReadonly())
                AddItem(player);
        }

        private void AddItem(Player player)
        {
            PlayerListItem item = _pool.Request(player);
            item.Base.SetParent(_hook);
            item.Base.localScale = Vector3.one;
            _items.Add(item);
        }

        private void ReturnItems()
        {
            foreach (PlayerListItem item in _items)
                ReturnItem(item);
            _items.Clear();
        }

        private void RemoveItem(Player player)
        {
            ReturnItem(_items.First(i => i.Player == player));
        }

        private void ReturnItem(PlayerListItem item)
        {
            _pool.Return(item);
        }
    }
}
