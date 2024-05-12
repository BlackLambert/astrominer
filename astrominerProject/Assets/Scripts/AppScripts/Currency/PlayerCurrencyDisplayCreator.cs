using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerCurrencyDisplayCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private Transform _hook;

        private ActiveItem<Player> _activePlayer;
        private Pool<PlayerCurrencyDisplay, Player, PrefabInstantiationArguments> _pool;

        private PlayerCurrencyDisplay _currentDisplay;

        public void Inject(Resolver resolver)
        {
            _activePlayer = resolver.Resolve<ActiveItem<Player>>();
            _pool = resolver.Resolve<Pool<PlayerCurrencyDisplay, Player, PrefabInstantiationArguments>>();
        }

        public void Initialize()
        {
            RequestDisplay();
            _activePlayer.OnValueChanged += OnActivePlayerChanged;
        }

        public void Clean()
        {
            _activePlayer.OnValueChanged -= OnActivePlayerChanged;
        }

        private void OnActivePlayerChanged(Player formerValue, Player newValue)
        {
            ReturnCurrentDisplay();
            RequestDisplay();
        }

        private void RequestDisplay()
        {
            if (!_activePlayer.HasValue)
            {
                return;
            }

            _currentDisplay = _pool.Request(_activePlayer.Value, PrefabInstantiationArguments.CreateFittedUIArgs(_hook));
        }

        private void ReturnCurrentDisplay()
        {
            if (_currentDisplay == null)
            {
                return;
            }

            _pool.Return(_currentDisplay);
            _currentDisplay = null;
        }
    }
}
