using System.Collections.Generic;
using System.Linq;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerColorItemSelectionInitializer : MonoBehaviour, Injectable
    {
        private Players _players;
        private ActiveItem<PlayerColorSelectionItem> _selectedItem;
        private ActiveItem<List<PlayerColorSelectionItem>> _items;
        
        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _selectedItem = resolver.Resolve<ActiveItem<PlayerColorSelectionItem>>();
            _items = resolver.Resolve<ActiveItem<List<PlayerColorSelectionItem>>>();
        }

        private void OnEnable()
        {
            SelectFirstAvailableItem();
            _selectedItem.OnValueChanged += OnSelectedItemChanged;
        }

        private void OnDisable()
        {
            _selectedItem.OnValueChanged -= OnSelectedItemChanged;
        }

        private void OnSelectedItemChanged(PlayerColorSelectionItem formervalue, PlayerColorSelectionItem newvalue)
        {
            if (formervalue == null && newvalue == null)
            {
                return;
            }
            
            SelectFirstAvailableItem();
        }

        private void SelectFirstAvailableItem()
        {
            if (_selectedItem.HasValue || !_items.HasValue)
            {
                return;
            }

            foreach (PlayerColorSelectionItem item in _items.Value)
            {
                if (_players.All(player => player.Color != item.ColorOption.Color))
                {
                    item.Selectable.Select();
                    return;
                }
            }
        }
    }
}
