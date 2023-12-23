using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AstroidsAmountContextUpdater : MonoBehaviour, Injectable
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

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
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

        private void OnSelectedAsteroidsAmountChanged()
        {
            if (_selectedAsteroidsAmount.Value == null)
            {
                return;
            }
            
            _context.SelectedAsteroidsAmountOption.Value = _selectedAsteroidsAmount.Value;
            _map.AsteroidAmountOption.Value = _selectedAsteroidsAmount.Value;
            _map.AsteroidPositions.Value = new List<Vector2>();
            _cameraZoom.Value.Value = _selectedAsteroidsAmount.Value.Zoom;
            Debug.Log($"Asteroid amount changed to: {_selectedAsteroidsAmount.Value}");
        }
    }
}
