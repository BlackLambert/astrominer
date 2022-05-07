using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class AddPlayerButton : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _button;

        private ActiveItem<PlayerColorSelectionItem> _chosenColor;
        private ActiveItem<string> _chosenName;
        private Selection _selection;
        private Players _players;
        private Factory<Player, PlayerFactory.Arguments> _playerFactory;

        private bool HasChosenColor => _chosenColor.Value != default;
        private bool HasChosenName => !string.IsNullOrEmpty(_chosenName.Value);

        public void Inject(Resolver resolver)
        {
            _chosenColor = resolver.Resolve<ActiveItem<PlayerColorSelectionItem>>();
            _chosenName = resolver.Resolve<ActiveItem<string>>();
            _selection = resolver.Resolve<Selection>();
            _players = resolver.Resolve<Players>();
            _playerFactory = resolver.Resolve<Factory<Player, PlayerFactory.Arguments>>();
        }

        private void OnEnable()
        {
            CheckButtonInteractable();
            _chosenColor.OnValueChanged += CheckButtonInteractable;
            _chosenName.OnValueChanged += CheckButtonInteractable;
            _button.onClick.AddListener(CreatePlayer);
        }

        private void OnDisable()
        {
            _chosenColor.OnValueChanged -= CheckButtonInteractable;
            _chosenName.OnValueChanged -= CheckButtonInteractable;
            _button.onClick.RemoveListener(CreatePlayer);
        }

        private void CheckButtonInteractable()
        {
            _button.interactable = HasChosenColor && HasChosenName;
        }

        private void CreatePlayer()
        {
            PlayerFactory.Arguments args = new PlayerFactory.Arguments(_chosenColor.Value.Color, _chosenName.Value);
            _players.Values.Add(_playerFactory.Create(args));
            ClearSelection();
        }

        private void ClearSelection()
        {
            _chosenName.Value = string.Empty;
            _chosenColor.Value = default;
            if(_selection.HasSelection)
                _selection.DeselectCurrent();
        }
    }
}
