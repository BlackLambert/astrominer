using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GameTimeUpdater : MonoBehaviour, Injectable
    {
        private GameTime _gameTime;
        
        public void Inject(Resolver resolver)
        {
            _gameTime = resolver.Resolve<GameTime>();
        }

        private void Update()
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            if (_gameTime.Paused)
                return;
            _gameTime.Value.Value += Time.deltaTime;
        }
    }
}
