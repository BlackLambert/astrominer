using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ItemSelector<T> : MonoBehaviour, Injectable
    {
        private Selectable _selectable;
        private ActiveItem<T> _selectedItem;
        private T _item;

        public void Inject(Resolver resolver)
        {
            _selectable = resolver.Resolve<Selectable>();
            _selectedItem = resolver.Resolve<ActiveItem<T>>();
            _item = resolver.Resolve<T>();
        }

        private void Start()
        {
            _selectable.OnSelected += SelectItem;
            _selectable.OnDeselected += DeselectItem;
            InitSelection();
        }

        private void InitSelection()
        {
            if (_selectable.IsSelected)
                SelectItem();
        }

        private void OnDestroy()
        {
            _selectable.OnSelected -= SelectItem;
            _selectable.OnDeselected -= DeselectItem;
            if (_item.Equals(_selectedItem.Value))
                DeselectItem();
        }

        private void SelectItem()
        {
            _selectedItem.Value = _item;
        }

        private void DeselectItem()
        {
            _selectedItem.Value = default;
        }
    }
}
