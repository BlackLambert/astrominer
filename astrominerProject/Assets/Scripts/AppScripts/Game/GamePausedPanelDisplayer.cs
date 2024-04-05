using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GamePausedPanelDisplayer : MonoBehaviour, Injectable
    {
        [SerializeField]
        private GameObject _panel;

        private GameTime _gameTime;
        
        public void Inject(Resolver resolver)
        {
            _gameTime = resolver.Resolve<GameTime>();
        }
        
        private void OnEnable()
        {
            CheckShowPanel();
            _gameTime.Paused.OnValueChanged += OnPausedChanged;
        }

        private void OnDisable()
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
