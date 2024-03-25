using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GamePauseUpdater : MonoBehaviour, Injectable
    {
        private GameTime _gameTime;

        public void Inject(Resolver resolver)
        {
            _gameTime = resolver.Resolve<GameTime>();
        }

        private void Update()
        {
            UpdatePaused();
        }

        private void UpdatePaused()
        {
            bool paused = Time.timeScale == 0;
            if (_gameTime.Paused != paused)
                _gameTime.Paused.Value = paused;
        }
    }
}
