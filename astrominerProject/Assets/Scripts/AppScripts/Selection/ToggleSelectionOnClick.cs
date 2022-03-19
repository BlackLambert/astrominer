using SBaier.DI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SBaier.Astrominer
{
    public class ToggleSelectionOnClick : MonoBehaviour, IPointerClickHandler, Injectable
    {
        private Selectable _selectable;
        private Selection _selection;

        public void Inject(Resolver resolver)
        {
            _selectable = resolver.Resolve<Selectable>();
            _selection = resolver.Resolve<Selection>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_selection.IsSelected(_selectable))
                _selection.DeselectCurrent();
            else
                _selection.Select(_selectable);
        }
    }
}
