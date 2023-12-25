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
        private BasesPlacementContext _placePlacementContext;
        private MapCreationContext _mapCreationContext;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _placePlacementContext = resolver.Resolve<BasesPlacementContext>();
            _mapCreationContext = resolver.Resolve<MapCreationContext>();
        }
        
        private void Start()
        {
            _map.AsteroidArguments.OnValueChanged += OnAsteroidPositionsChanged;
            _placePlacementContext.Started.OnValueChanged += OnStartedChanged;
            _button.onClick.AddListener(StartBasePlacement);
            UpdateButtonInteractable();
        }

        private void OnDestroy()
        {
            _map.AsteroidArguments.OnValueChanged -= OnAsteroidPositionsChanged;
            _placePlacementContext.Started.OnValueChanged -= OnStartedChanged;
            _button.onClick.RemoveListener(StartBasePlacement);
        }

        private void OnStartedChanged(bool formervalue, bool newvalue)
        {
            UpdateButtonInteractable();
        }

        private void OnAsteroidPositionsChanged(List<Asteroid.Arguments> formerValue, List<Asteroid.Arguments> newValue)
        {
            _button.interactable = newValue?.Count > 0;
        }

        private void UpdateButtonInteractable()
        {
            _button.interactable = _map.AsteroidArguments.Value?.Count > 0 && !_placePlacementContext.Started.Value;
        }

        private void StartBasePlacement()
        {
            _placePlacementContext.Started.Value = true;
            _mapCreationContext.Finished.Value = true;
        }
    }
}
