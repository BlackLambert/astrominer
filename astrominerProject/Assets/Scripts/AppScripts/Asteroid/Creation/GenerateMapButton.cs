using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class GenerateMapButton : MapCreationTrigger, Initializable, Cleanable
    {
        [SerializeField]
        private Button _button;

        public void Initialize()
        {
            UpdateButtonInteractable();
            _context.SelectedAsteroidsAmountOption.OnValueChanged += OnSelectedAsteroidsAmountOptionChanged;
            _context.Finished.OnValueChanged += OnFinishedChanged;
            _button.onClick.AddListener(CreateMap);
        }

        public void Clean()
        {
            _context.SelectedAsteroidsAmountOption.OnValueChanged -= OnSelectedAsteroidsAmountOptionChanged;
            _context.Finished.OnValueChanged -= OnFinishedChanged;
            _button.onClick.RemoveListener(CreateMap);
        }

        private void OnFinishedChanged(bool formervalue, bool newvalue)
        {
            UpdateButtonInteractable();
        }

        private void OnSelectedAsteroidsAmountOptionChanged(
            AsteroidAmountOption formerValue, AsteroidAmountOption newValue)
        {
            UpdateButtonInteractable();
        }

        private void UpdateButtonInteractable()
        {
            _button.interactable = _context.IsValid && !_context.Finished.Value;
        }
    }
}
