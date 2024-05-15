using SBaier.DI;
using UnityEngine;
using TMPro;

namespace SBaier.Astrominer
{
    public class BasePlacementText : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private BasesPlacementContext _context;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<BasesPlacementContext>();
        }

        public void Initialize()
        {
            UpdateText();
            _context.CurrentPlayer.OnValueChanged += OnCurrentPlayerChanged;
        }

        public void Clean()
        {
            _context.CurrentPlayer.OnValueChanged -= OnCurrentPlayerChanged;
        }

        private void OnCurrentPlayerChanged(Player formervalue, Player newvalue)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            if (_context.CurrentPlayer.Value == null)
            {
                return;
            }

            string color = ColorUtility.ToHtmlStringRGB(_context.CurrentPlayer.Value.Color);
            string playerName = _context.CurrentPlayer.Value.Name;
            _text.text = $"Please place your base player <color=#{color}>{playerName}</color>";
        }
    }
}
