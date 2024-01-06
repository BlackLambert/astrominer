using System.Linq;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerColorItemSelectionInitializer : MonoBehaviour, Injectable
    {
        private PlayerSettings _playerSettings;
        private Players _players;
        private ActiveItem<PlayerColorOption> _selectedOption;

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _selectedOption = resolver.Resolve<ActiveItem<PlayerColorOption>>();
            _playerSettings = resolver.Resolve<PlayerSettings>();
        }

        private void OnEnable()
        {
            SelectFirstAvailableItem();
            _selectedOption.OnValueChanged += OnSelectedItemChanged;
        }

        private void OnDisable()
        {
            _selectedOption.OnValueChanged -= OnSelectedItemChanged;
        }

        private void OnSelectedItemChanged(PlayerColorOption formervalue, PlayerColorOption newvalue)
        {
            SelectFirstAvailableItem();
        }

        private void SelectFirstAvailableItem()
        {
            if (_selectedOption.HasValue)
            {
                return;
            }

            foreach (PlayerColorOption item in _playerSettings.PlayerColors)
            {
                if (_players.All(player => player.Color != item.Color))
                {
                    _selectedOption.Value = item;
                    return;
                }
            }
        }
    }
}
