using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ActivateOnActivePlayer : MonoBehaviour, Injectable
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

        private void OnEnable()
        {
            UpdateActiveState();
            _activePlayer.OnValueChanged += OnPlayerChanged;
        }

        private void OnDisable()
        {
            _activePlayer.OnValueChanged -= OnPlayerChanged;
        }

        private void OnPlayerChanged()
        {
            UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            _target.SetActive(_activePlayer.Value == _player);
        }
    }
}
