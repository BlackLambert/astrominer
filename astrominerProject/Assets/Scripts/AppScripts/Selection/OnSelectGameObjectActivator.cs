using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OnSelectGameObjectActivator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private GameObject _target;

        private Selectable _selectable;

        public void Inject(Resolver resolver)
        {
            _selectable = resolver.Resolve<Selectable>();
        }

        private void Start()
        {
            CheckTargetActive();
            _selectable.OnSelected += CheckTargetActive;
            _selectable.OnDeselected += CheckTargetActive;
        }

        private void OnDestroy()
        {
            _selectable.OnSelected -= CheckTargetActive;
            _selectable.OnDeselected -= CheckTargetActive;
        }

        private void CheckTargetActive()
        {
            _target.SetActive(_selectable.IsSelected);
        }
    }
}
