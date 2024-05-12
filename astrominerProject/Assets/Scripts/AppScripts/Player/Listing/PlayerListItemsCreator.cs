using SBaier.DI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerListItemsCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private RectTransform _hook;

        private Pool<PlayerListItem, Player, PrefabInstantiationArguments> _pool;
        private Players _players;

        private List<PlayerListItem> _items = new List<PlayerListItem>();

        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<PlayerListItem, Player, PrefabInstantiationArguments>>();
            _players = resolver.Resolve<Players>();
        }

        public void Initialize()
        {
            CreateItems();
            _players.OnItemAdded += AddItem;
            _players.OnItemRemoved += RemoveItem;
        }

        public void Clean()
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
            PlayerListItem item = _pool.Request(player, PrefabInstantiationArguments.CreateUIArgs(_hook));
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
            PlayerListItem item = _items.First(i => i.Player == player);
            _items.Remove(item);
            ReturnItem(item);
        }

        private void ReturnItem(PlayerListItem item)
        {
            _pool.Return(item);
        }
    }
}
