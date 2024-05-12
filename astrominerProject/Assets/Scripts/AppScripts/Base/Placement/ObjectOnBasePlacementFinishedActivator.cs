using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ObjectOnBasePlacementFinishedActivator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private GameObject _gameObject;

        private BasesPlacementContext _placementContext;

        public void Inject(Resolver resolver)
        {
            _placementContext = resolver.Resolve<BasesPlacementContext>();
        }

        public void Initialize()
        {
            UpdateEnabledState();
            _placementContext.Finished.OnValueChanged += OnFinishedChanged;
        }

        public void Clean()
        {
            _placementContext.Finished.OnValueChanged -= OnFinishedChanged;
        }

        private void OnFinishedChanged(bool formervalue, bool newvalue)
        {
            UpdateEnabledState();
        }

        private void UpdateEnabledState()
        {
            _gameObject.SetActive(_placementContext.Finished);
        }
    }
}
