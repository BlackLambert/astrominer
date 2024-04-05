using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GameEndScreenCreator : MonoBehaviour, Injectable
    {
        private Pool<GameEndScreen> _pool;
        private Game _game;
        private GameEndScreen _endScreen;
        
        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<GameEndScreen>>();
            _game = resolver.Resolve<Game>();
        }

        private void OnEnable()
        {
            _game.Finished.OnValueChanged += OnFinishedChanged;
        }

        private void OnDisable()
        {
            _game.Finished.OnValueChanged -= OnFinishedChanged;
        }

        private void OnFinishedChanged(bool formerValue, bool newValue)
        {
            CheckCreate();
        }

        private void CheckCreate()
        {
            if (_game.Finished.Value)
            {
                _endScreen = _pool.Request();
                _endScreen.transform.SetParent(null, false);
            }
        }
    }
}
