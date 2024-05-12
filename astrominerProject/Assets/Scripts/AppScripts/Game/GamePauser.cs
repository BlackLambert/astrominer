using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GamePauser : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private GameTime _gameTime;

        public void Inject(Resolver resolver)
        {
            _gameTime = resolver.Resolve<GameTime>();
        }

        public void Initialize()
        {
            UpdateGameTime();
            _gameTime.Paused.OnValueChanged += OnPausedChanged;
        }

        public void Clean()
        {
            _gameTime.Paused.OnValueChanged -= OnPausedChanged;
        }

        private void OnPausedChanged(bool formervalue, bool newvalue)
        {
            UpdateGameTime();
        }

        private void UpdateGameTime()
        {
            Time.timeScale = _gameTime.Paused.Value ? 0 : 1;
        }
    }
}
