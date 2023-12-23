using System;
using PCGToolkit.Sampling;
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
        private AsteroidPositionsGenerator _generator;
        private MapCreationSettings _creationSettings;
        private Map _map;
        private int _retries = 0;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<MapCreationContext>();
            _generator = resolver.Resolve<AsteroidPositionsGenerator>();
            _creationSettings = resolver.Resolve<MapCreationSettings>();
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
            try
            {
                _map.AsteroidPositions.Value = _generator.GenerateMap(
                    _context.SelectedAsteroidsAmountOption, _map.CenterPoint, _creationSettings.MinimalAsteroidDistance);
                _retries = 0;
            }
            catch (PoissonDiskSampling2D.SamplingException exception)
            {
                if (_retries >= _creationSettings.SamplingRetriesOnFail)
                {
                    throw;
                }

                _retries++;
                Debug.LogWarning("Failed to create asteroid positions. Retrying...");
                CreateMap();
            }
        }
    }
}
