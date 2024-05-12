using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerInfoPanelCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private Transform _hook;

        private ActiveItem<Player> _activePlayer;
        private Pool<PlayerInfoPanel, Player, PrefabInstantiationArguments> _pool;

        private PlayerInfoPanel _currentPanel;

        public void Inject(Resolver resolver)
        {
            _activePlayer = resolver.Resolve<ActiveItem<Player>>();
            _pool = resolver.Resolve<Pool<PlayerInfoPanel, Player, PrefabInstantiationArguments>>();
        }

        public void Initialize()
        {
            TryCreatePanel();
            _activePlayer.OnValueChanged += OnValueChanged;
        }

        public void Clean()
        {
            _activePlayer.OnValueChanged -= OnValueChanged;
            TryReturnPanel();
        }

        private void OnValueChanged(Player formervalue, Player newvalue)
        {
            UpdatePanel();
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
            _currentPanel = _pool.Request(_activePlayer.Value, PrefabInstantiationArguments.CreateUIArgs(_hook));
        }
    }
}
