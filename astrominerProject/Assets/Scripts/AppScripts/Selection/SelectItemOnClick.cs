using SBaier.DI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SBaier.Astrominer
{
    public class SelectItemOnClick<TItem> : MonoBehaviour, IPointerClickHandler, Injectable
    {
        private ActiveItem<TItem> _activeItem;
        private TItem _item;
        private bool _active = true;

        public void Inject(Resolver resolver)
        {
            _activeItem = resolver.Resolve<ActiveItem<TItem>>();
            _item = resolver.Resolve<TItem>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_active)
            {
                return;
            }
            
            if (!_activeItem.HasValue || !_activeItem.Value.Equals(_item))
            {
                _activeItem.Value = _item;
            }
        }

        public void Activate(bool active)
        {
            _active = active;
            
            if (_activeItem.HasValue && _activeItem.Value.Equals(_item))
            {
                _activeItem.Value = default;
            }
        }
    }
}
