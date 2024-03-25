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

        private void CheckShowPanel()
        {
            bool setActive = Time.timeScale == 0;
            if (_panel.activeSelf != setActive)
                _panel.SetActive(setActive);
        }

        private void OnPausedChanged(bool formervalue, bool newvalue)
        {
            CheckShowPanel();
        }
    }
}
