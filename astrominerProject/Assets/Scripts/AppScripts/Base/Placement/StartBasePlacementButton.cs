using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class StartBasePlacementButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private Button _button;

        private Map _map;
        private Observable<MapCreationState> _state;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _state = resolver.Resolve<Observable<MapCreationState>>();
        }
        
        public void Initialize()
        {
            _map.AsteroidArguments.OnValueChanged += OnAsteroidPositionsChanged;
            _state.OnValueChanged += OnStateChanged;
            _button.onClick.AddListener(StartBasePlacement);
            UpdateButtonInteractable();
        }

        public void Clean()
        {
            _map.AsteroidArguments.OnValueChanged -= OnAsteroidPositionsChanged;
            _state.OnValueChanged -= OnStateChanged;
            _button.onClick.RemoveListener(StartBasePlacement);
        }

        private void OnStateChanged(MapCreationState formervalue, MapCreationState newvalue)
        {
            UpdateButtonInteractable();
        }

        private void OnAsteroidPositionsChanged(List<Asteroid.Arguments> formerValue, List<Asteroid.Arguments> newValue)
        {
            UpdateButtonInteractable();
        }

        private void UpdateButtonInteractable()
        {
            _button.interactable = _map.AsteroidArguments.Value?.Count > 0 && 
                                   _state.Value != MapCreationState.BasePlacement;
        }

        private void StartBasePlacement()
        {
            _state.Value = MapCreationState.BasePlacement;
        }
    }
}
