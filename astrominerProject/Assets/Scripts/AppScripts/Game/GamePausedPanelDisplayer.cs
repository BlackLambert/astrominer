using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GamePausedPanelDisplayer : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private GameObject _panel;

        private GameTime _gameTime;
        
        public void Inject(Resolver resolver)
        {
            _gameTime = resolver.Resolve<GameTime>();
        }
        
        public void Initialize()
        {
            CheckShowPanel();
            _gameTime.Paused.OnValueChanged += OnPausedChanged;
        }

        public void Clean()
        {
            _gameTime.Paused.OnValueChanged -= OnPausedChanged;
        }

        private void OnPausedChanged(bool formervalue, bool newvalue)
        {
            CheckShowPanel();
        }

        private void CheckShowPanel()
        {
            _panel.SetActive(_gameTime.Paused.Value);
        }
    }
}
