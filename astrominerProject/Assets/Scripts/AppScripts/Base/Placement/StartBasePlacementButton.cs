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
        private BasesPlacementContext _basePlacementContext;
        private TargetExploitSettingContext _targetExploitContext;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _basePlacementContext = resolver.Resolve<BasesPlacementContext>();
            _targetExploitContext = resolver.Resolve<TargetExploitSettingContext>();
        }
        
        private void Start()
        {
            _map.AsteroidArguments.OnValueChanged += OnAsteroidPositionsChanged;
            _basePlacementContext.Started.OnValueChanged += OnStartedChanged;
            _button.onClick.AddListener(StartBasePlacement);
            UpdateButtonInteractable();
        }

        private void OnDestroy()
        {
            _map.AsteroidArguments.OnValueChanged -= OnAsteroidPositionsChanged;
            _basePlacementContext.Started.OnValueChanged -= OnStartedChanged;
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
            _button.interactable = _map.AsteroidArguments.Value?.Count > 0 && !_basePlacementContext.Started.Value;
        }

        private void StartBasePlacement()
        {
            _basePlacementContext.Started.Value = true;
            _targetExploitContext.Finished.Value = true;
        }
    }
}
