using SBaier.DI;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerColorSelectionItem : MonoBehaviour, Injectable
    {
        [field: SerializeField]
        public RectTransform Base { get; private set; }
        [field: SerializeField] 
        public Selectable Selectable { get; private set; }
        [SerializeField]
        private ToggleSelectionOnClick _selectionToggler;
        [SerializeField]
        private GameObject _selectedOverlay;

        private Players _players;
        public PlayerColorOption ColorOption { get; private set; }

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            ColorOption = resolver.Resolve<PlayerColorOption>();
        }

        private void OnEnable()
        {
            UpdateState();
            _players.OnItemsChanged += UpdateState;
        }

        private void OnDisable()
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
            _selectionToggler.Activate(!isColorUsed);
            _selectedOverlay.SetActive(isColorUsed);
        }
    }
}
