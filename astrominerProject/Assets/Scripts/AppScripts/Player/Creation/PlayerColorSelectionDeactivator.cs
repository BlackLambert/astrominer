using System.Linq;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerColorSelectionDeactivator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private SelectPlayerColorOnClick _selector;

        private Players _players;
        private PlayerColorOption _item;

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _item = resolver.Resolve<PlayerColorOption>();
        }

        public void Initialize()
        {
            UpdateSelectorActiveState();
            _players.OnItemsChanged += UpdateSelectorActiveState;
        }

        public void Clean()
        {
            _players.OnItemsChanged -= UpdateSelectorActiveState;
        }

        private void UpdateSelectorActiveState()
        {
            _selector.Activate(_players.All(player => player.Color != _item.Color));
        }
    }
}
