using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MachineCostsApplier : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private float _applyFrequency = 1f;
        
        private Players _players;
        private GameTime _gameTime;

        private float _nextApplyTime = 0;
        
        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _gameTime = resolver.Resolve<GameTime>();
        }

        private void Update()
        {
            if (_gameTime.Paused)
            {
                return;
            }

            while (_nextApplyTime <= _gameTime.Value)
            {
                _nextApplyTime += _applyFrequency;
                ApplyCosts();
            }
            
        }

        private void ApplyCosts()
        {
            foreach (Player player in _players)
            {
                ApplyCosts(player);
            }
        }

        private void ApplyCosts(Player player)
        {
            float costs = 0;
            
            foreach (Asteroid asteroid in player.OwnedAsteroids)
            {
                costs += asteroid.GetTotalCosts();
            }

            if (costs > 0)
            {
                Debug.Log($"Applying costs of {costs} to the player '{player.Name}'.");
                player.Credits.RequestAllowNegative(costs);
            }
        }
    }
}
