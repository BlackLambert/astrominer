using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SetDefaultPlayerNameOnColorSelected : MonoBehaviour, Injectable
    {
        private ActiveItem<PlayerColorSelectionItem> _activeColorOption;
        private ActiveItem<string> _selectedPlayerName;
        private PlayerColorOption _currentOption = null;

        public void Inject(Resolver resolver)
        {
            _selectedPlayerName = resolver.Resolve<ActiveItem<string>>();
            _activeColorOption = resolver.Resolve<ActiveItem<PlayerColorSelectionItem>>();
        }

        private void OnEnable()
        {
            _activeColorOption.OnValueChanged += UpdatePlayerName;
        }

        private void OnDisable()
        {
            _activeColorOption.OnValueChanged -= UpdatePlayerName;
        }

        private void UpdatePlayerName(PlayerColorSelectionItem formervalue, PlayerColorSelectionItem newvalue)
        {
            if (!string.IsNullOrEmpty(_selectedPlayerName.Value) &&
                _selectedPlayerName.Value != _currentOption?.DefaultPlayerName ||
                newvalue == null)
            {
                return;
            }

            _currentOption = newvalue.ColorOption;
            _selectedPlayerName.Value = newvalue.ColorOption.DefaultPlayerName;
        }
    }
}
