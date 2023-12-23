using SBaier.DI;
using UnityEngine;
using TMPro;

namespace SBaier.Astrominer
{
    public class BasePlacementText : MonoBehaviour, Injectable
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private BasesPlacementContext _context;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<BasesPlacementContext>();
        }

        private void OnEnable()
        {
            UpdateText();
            _context.Started.OnValueChanged += OnBasePlacementStarted;
            _context.CurrentPlayer.OnValueChanged += OnCurrentPlayerChanged;
        }

        private void OnDisable()
        {
            _context.Started.OnValueChanged -= OnBasePlacementStarted;
            _context.CurrentPlayer.OnValueChanged -= OnCurrentPlayerChanged;
        }

        private void UpdateText()
        {
            if (!_context.Started.Value || _context.CurrentPlayer.Value == null)
            {
                return;
            }

            string color = ColorUtility.ToHtmlStringRGB(_context.CurrentPlayer.Value.Color);
            string playerName = _context.CurrentPlayer.Value.Name;
            _text.text = $"Please place your base player <color=#{color}>{playerName}</color>";
        }

        private void OnBasePlacementStarted(bool formervalue, bool newvalue)
        {
            UpdateText();
        }

        private void OnCurrentPlayerChanged(Player formervalue, Player newvalue)
        {
            UpdateText();
        }
    }
}
