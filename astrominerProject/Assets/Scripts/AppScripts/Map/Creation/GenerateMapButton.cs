using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class GenerateMapButton : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _button;

        private MapCreationContext _context;
        private MapCreator _mapCreator;
        private Map _map;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<MapCreationContext>();
            _mapCreator = resolver.Resolve<MapCreator>();
            _map = resolver.Resolve<Map>();
        }

        private void OnEnable()
        {
            UpdateButtonInteractable();
            _context.SelectedAsteroidsAmountOption.OnValueChanged += OnSelectedAsteroidsAmountOptionChanged;
            _context.Finished.OnValueChanged += OnFinishedChanged;
            _button.onClick.AddListener(CreateMap);
        }

        private void OnDisable()
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

        private void CreateMap()
        {
            _map.AsteroidArguments.Value = _mapCreator.CreateMap();
        }
    }
}
