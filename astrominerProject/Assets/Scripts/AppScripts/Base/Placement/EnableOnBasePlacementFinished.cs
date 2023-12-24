using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class EnableOnBasePlacementFinished : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Button _button;

        private BasesPlacementContext _placementContext;

        public void Inject(Resolver resolver)
        {
            _placementContext = resolver.Resolve<BasesPlacementContext>();
        }

        private void OnEnable()
        {
            UpdateEnabledState();
            _placementContext.Finished.OnValueChanged += OnFinishedChanged;
        }

        private void OnDisable()
        {
            _placementContext.Finished.OnValueChanged -= OnFinishedChanged;
        }

        private void OnFinishedChanged(bool formervalue, bool newvalue)
        {
            UpdateEnabledState();
        }

        private void UpdateEnabledState()
        {
            _button.enabled = _placementContext.Finished;
        }
    }
}
