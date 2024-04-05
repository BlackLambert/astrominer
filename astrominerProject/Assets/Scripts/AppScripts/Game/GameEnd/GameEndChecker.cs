using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GameEndChecker : MonoBehaviour, Injectable
    {
        private Game _game;
        private TargetExploit _targetExploit;
        private GameTime _gameTime;
        private PlayerValues _playerValues;

        public void Inject(Resolver resolver)
        {
            _game = resolver.Resolve<Game>();
            _targetExploit = resolver.Resolve<TargetExploit>();
            _gameTime = resolver.Resolve<GameTime>();
            _playerValues = resolver.Resolve<PlayerValues>();
        }

        private void Update()
        {
            if (!_game.Finished.Value &&
                _game.ExploitedPercentage.Value >= _targetExploit.Target.Value)
            {
                _gameTime.Paused.Value = true;
                _game.PLayerWon.Value = GetPlayerWon();
                _game.Finished.Value = true;
            }
        }

        private Player GetPlayerWon()
        {
            Player result = null;
            float max = float.MinValue;
            
            foreach (KeyValuePair<Player, PlayerValue> pair in _playerValues.Values)
            {
                if (pair.Value.TotalValue > max)
                {
                    result = pair.Key;
                    max = pair.Value.TotalValue;
                }
            }

            return result;
        }
    }
}
