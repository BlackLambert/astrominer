using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SetDefaultPlayerNameOnColorSelected : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private ActiveItem<PlayerColorOption> _activeColorOption;
        private ActiveItem<string> _selectedPlayerName;
        private PlayerColorOption _currentOption = null;

        public void Inject(Resolver resolver)
        {
            _selectedPlayerName = resolver.Resolve<ActiveItem<string>>();
            _activeColorOption = resolver.Resolve<ActiveItem<PlayerColorOption>>();
        }

        public void Initialize()
        {
            UpdatePlayerName(_activeColorOption.Value);
            _activeColorOption.OnValueChanged += OnColorChanged;
        }

        public void Clean()
        {
            _activeColorOption.OnValueChanged -= OnColorChanged;
        }

        private void OnColorChanged(PlayerColorOption formervalue, PlayerColorOption newvalue)
        {
            UpdatePlayerName(newvalue);
        }

        private void UpdatePlayerName(PlayerColorOption newvalue)
        {
            if (!string.IsNullOrEmpty(_selectedPlayerName.Value) &&
                _selectedPlayerName.Value != _currentOption?.DefaultPlayerName ||
                newvalue == null)
            {
                return;
            }

            _currentOption = newvalue;
            _selectedPlayerName.Value = newvalue.DefaultPlayerName;
        }
    }
}
