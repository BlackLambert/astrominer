using SBaier.DI;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerColorSelectionItem : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [field: SerializeField]
        public RectTransform Base { get; private set; }
        [SerializeField]
        private GameObject _selectedOverlay;

        private Players _players;
        public PlayerColorOption ColorOption { get; private set; }

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            ColorOption = resolver.Resolve<PlayerColorOption>();
        }

        public void Initialize()
        {
            UpdateState();
            _players.OnItemsChanged += UpdateState;
        }

        public void Clean()
        {
            _players.OnItemsChanged -= UpdateState;
        }

        private bool IsColorUsed()
        {
            return _players.ToReadonly().FirstOrDefault(p => p.Color == ColorOption.Color) != null;
        }

        private void UpdateState()
        {
            bool isColorUsed = IsColorUsed();
            _selectedOverlay.SetActive(isColorUsed);
        }
    }
}
