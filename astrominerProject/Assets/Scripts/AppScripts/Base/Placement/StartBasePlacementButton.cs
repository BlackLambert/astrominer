using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class StartBasePlacementButton : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Button _button;

        private Map _map;
        private BasePlacementContext _placePlacementContext;
        private MapCreationContext _mapCreationContext;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _placePlacementContext = resolver.Resolve<BasePlacementContext>();
            _mapCreationContext = resolver.Resolve<MapCreationContext>();
        }
        
        private void Start()
        {
            _map.AsteroidPositions.OnValueChanged += OnAsteroidPositionsChanged;
            _placePlacementContext.Started.OnValueChanged += OnStartedChanged;
            _button.onClick.AddListener(StartBasePlacement);
            UpdateButtonInteractable();
        }

        private void OnDestroy()
        {
            _map.AsteroidPositions.OnValueChanged -= OnAsteroidPositionsChanged;
            _placePlacementContext.Started.OnValueChanged -= OnStartedChanged;
            _button.onClick.RemoveListener(StartBasePlacement);
        }

        private void OnStartedChanged(bool formervalue, bool newvalue)
        {
            UpdateButtonInteractable();
        }

        private void OnAsteroidPositionsChanged(List<Vector2> formervalue, List<Vector2> newvalue)
        {
            _button.interactable = newvalue?.Count > 0;
        }

        private void UpdateButtonInteractable()
        {
            _button.interactable = _map.AsteroidPositions.Value?.Count > 0 && !_placePlacementContext.Started.Value;
        }

        private void StartBasePlacement()
        {
            _placePlacementContext.Started.Value = true;
            _mapCreationContext.Finished.Value = true;
        }
    }
}
