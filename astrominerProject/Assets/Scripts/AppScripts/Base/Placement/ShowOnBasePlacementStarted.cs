using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShowOnBasePlacementStarted : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private GameObject _target;
        
        private BasesPlacementContext _context;
        
        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<BasesPlacementContext>();
        }

        private void OnEnable()
        {
            UpdateShow();
            _context.Started.OnValueChanged += OnStartedChanged;
        }

        private void OnDisable()
        {
            _context.Started.OnValueChanged -= OnStartedChanged;
        }

        private void OnStartedChanged(bool formervalue, bool newvalue)
        {
            UpdateShow();
        }

        private void UpdateShow()
        {
            _target.SetActive(_context.Started.Value);
        }
    }
}
