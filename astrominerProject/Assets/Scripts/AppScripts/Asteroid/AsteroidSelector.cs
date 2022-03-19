using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidSelector : MonoBehaviour, Injectable
    {
        private Selectable _selectable;
        private SelectedAsteroid _selectedAsteroid;
        private Asteroid _asteroid;

        public void Inject(Resolver resolver)
        {
            _selectable = resolver.Resolve<Selectable>();
            _selectedAsteroid = resolver.Resolve<SelectedAsteroid>();
            _asteroid = resolver.Resolve<Asteroid>();
        }

        private void Start()
        {
            _selectable.OnSelected += SelectAsteroid;
            _selectable.OnDeselected += DeselectAsteroid;
            InitSelection();
        }

        private void InitSelection()
        {
            if (_selectable.IsSelected)
                SelectAsteroid();
        }

        private void OnDestroy()
        {
            _selectable.OnSelected -= SelectAsteroid;
            _selectable.OnDeselected -= DeselectAsteroid;
        }

        private void SelectAsteroid()
        {
            _selectedAsteroid.Value = _asteroid;
        }

        private void DeselectAsteroid()
        {
            _selectedAsteroid.Value = null;
        }
    }
}
