using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public abstract class AddPlayerButton : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _button;

        protected abstract bool isHuman { get; }
        private MatchmakingPlayerCreator _playerCreator;

        public void Inject(Resolver resolver)
        {
            _playerCreator = resolver.Resolve<MatchmakingPlayerCreator>();
        }

        private void OnEnable()
        {
            CheckButtonInteractable();
            _button.onClick.AddListener(CreatePlayer);
            _playerCreator.OnPlayerArgumentChanged += OnPlayerArgumentChanged;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(CreatePlayer);
            _playerCreator.OnPlayerArgumentChanged -= OnPlayerArgumentChanged;
        }

        private void OnPlayerArgumentChanged(MatchmakingPlayerCreator.Info info)
        {
            CheckButtonInteractable();
        }

        private void CheckButtonInteractable()
        {
            _button.interactable = _playerCreator.IsPlayerCreatable;
        }

        private void CreatePlayer()
        {
            _playerCreator.CreatePlayer(isHuman);
        }
    }
}
