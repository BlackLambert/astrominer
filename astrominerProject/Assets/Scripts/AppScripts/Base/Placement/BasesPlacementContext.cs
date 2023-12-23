using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasesPlacementContext
    {
        public event Action<Player> OnBaseAdded;
        
        public Observable<bool> Started { get; } = new Observable<bool>() { Value = false };
        public Observable<bool> Finished { get; } = new Observable<bool>() { Value = false };
        public Observable<Player> CurrentPlayer { get; } = new Observable<Player>() { Value = null };
        public IReadOnlyDictionary<Player, Vector2> PlayerToPosition => _playerToPosition;

        private Dictionary<Player, Vector2> _playerToPosition = new Dictionary<Player, Vector2>();

        public void AddBasePosition(Player player, Vector2 position)
        {
            _playerToPosition.Add(player, position);
            OnBaseAdded?.Invoke(player);
        }
    }
}
