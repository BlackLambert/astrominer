using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class HideOnMapCreationFinished : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private GameObject _target;
        
        private MapCreationContext _context;
        
        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<MapCreationContext>();
        }

        private void OnEnable()
        {
            UpdateShow();
            _context.Finished.OnValueChanged += OnFinishedChanged;
        }

        private void OnDisable()
        {
            _context.Finished.OnValueChanged -= OnFinishedChanged;
        }

        private void OnFinishedChanged(bool formervalue, bool newvalue)
        {
            UpdateShow();
        }

        private void UpdateShow()
        {
            _target.SetActive(!_context.Finished.Value);
        }
    }
}
