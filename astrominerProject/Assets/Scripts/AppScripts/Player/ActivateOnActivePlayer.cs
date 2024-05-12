using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ActivateOnActivePlayer : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private GameObject _target;

        private Player _player;
        private ActiveItem<Player> _activePlayer;

        public void Inject(Resolver resolver)
        {
            _player = resolver.Resolve<Player>();
            _activePlayer = resolver.Resolve<ActiveItem<Player>>();
        }

        public void Initialize()
        {
            UpdateActiveState();
            _activePlayer.OnValueChanged += OnPlayerChanged;
        }

        public void Clean()
        {
            _activePlayer.OnValueChanged -= OnPlayerChanged;
        }

        private void OnPlayerChanged(Player formerValue, Player newValue)
        {
            UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            _target.SetActive(_activePlayer.Value == _player);
        }
    }
}
