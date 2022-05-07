using SBaier.DI;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class PlayerColorSelectionItem : MonoBehaviour, Injectable
    {
        [field: SerializeField]
        public RectTransform Base { get; private set; }
        [SerializeField]
        private ToggleSelectionOnClick _selectionToggler;
        [SerializeField]
        private GameObject _selectedOverlay;

        private Players _players;
        public Color Color { get; private set; }

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            Color = resolver.Resolve<Color>();
        }

        private void OnEnable()
        {
            UpdateState();
            _players.Values.OnItemsChanged += UpdateState;
        }

        private void OnDisable()
        {
            _players.Values.OnItemsChanged -= UpdateState;
        }

        private bool IsColorUsed()
        {
            return _players.Values.ToReadonly().FirstOrDefault(p => p.Color == Color) != null;
        }

        private void UpdateState()
        {
            bool isColorUsed = IsColorUsed();
            _selectionToggler.Activate(!isColorUsed);
            _selectedOverlay.SetActive(isColorUsed);
        }
    }
}
