using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidsAmountContextUpdater : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private MapCreationContext _context;
        private Map _map;
        private ActiveItem<AsteroidAmountOption> _selectedAsteroidsAmount;
        private CameraZoom _cameraZoom;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<MapCreationContext>();
            _selectedAsteroidsAmount = resolver.Resolve<ActiveItem<AsteroidAmountOption>>();
            _map = resolver.Resolve<Map>();
            _cameraZoom = resolver.Resolve<CameraZoom>();
        }

        public void Initialize()
        {
            UpdateContext(_selectedAsteroidsAmount.Value);
            AddListeners();
        }

        public void Clean()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            _selectedAsteroidsAmount.OnValueChanged += OnSelectedAsteroidsAmountChanged;
        }

        private void RemoveListeners()
        {
            _selectedAsteroidsAmount.OnValueChanged -= OnSelectedAsteroidsAmountChanged;
        }

        private void OnSelectedAsteroidsAmountChanged(AsteroidAmountOption formerValue, AsteroidAmountOption newValue)
        {
            UpdateContext(newValue);
        }

        private void UpdateContext(AsteroidAmountOption option)
        {
            if (option == null)
            {
                return;
            }
            
            _context.SelectedAsteroidsAmountOption.Value = option;
            _map.AsteroidAmountOption.Value = option;
            _cameraZoom.Value.Value = option.Zoom;
            Debug.Log($"Asteroid amount changed to: {option}");
        }
    }
}
