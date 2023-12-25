using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerInfoPanelCreator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Transform _hook;

        private ActiveItem<Player> _activePlayer;
        private Pool<PlayerInfoPanel, Player> _pool;

        private PlayerInfoPanel _currentPanel;

        public void Inject(Resolver resolver)
        {
            _activePlayer = resolver.Resolve<ActiveItem<Player>>();
            _pool = resolver.Resolve<Pool<PlayerInfoPanel, Player>>();
        }

        private void OnEnable()
        {
            TryCreatePanel();
            _activePlayer.OnValueChanged += UpdatePanel;
        }

        private void OnDisable()
        {
            _activePlayer.OnValueChanged -= UpdatePanel;
            TryReturnPanel();
        }

        private void UpdatePanel()
        {
            TryReturnPanel();
            TryCreatePanel();
        }

        private void TryReturnPanel()
        {
            if (_currentPanel == null)
                return;
            _pool.Return(_currentPanel);
            _currentPanel = null;
        }

        private void TryCreatePanel()
        {
            if (!_activePlayer.HasValue)
                return;
            _currentPanel = _pool.Request(_activePlayer.Value);
            _currentPanel.transform.SetParent(_hook, false);
        }
    }
}
