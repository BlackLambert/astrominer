using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ItemSelector<T> : MonoBehaviour, Injectable
    {
        private Selectable _selectable;
        private ActiveItem<T> _selectedItem;
        private T _item;

        public virtual void Inject(Resolver resolver)
        {
            _selectable = resolver.Resolve<Selectable>();
            _selectedItem = resolver.Resolve<ActiveItem<T>>();
            _item = resolver.Resolve<T>();
        }

        private void OnEnable()
        {
            _selectable.OnSelected += OnSelectableSelected;
            _selectable.OnDeselected += DeselectItem;
            InitSelection();
        }

        private void OnDisable()
        {
            _selectable.OnSelected -= OnSelectableSelected;
            _selectable.OnDeselected -= DeselectItem;

            if (_item.Equals(_selectedItem.Value))
            {
                DeselectItem();
            }
            
            if (_selectable.IsSelected)
            {
                _selectable.Deselect();
            }
        }

        private void InitSelection()
        {
            if (_selectable.IsSelected)
                SelectItem(_item);
        }

        private void OnSelectableSelected()
        {
            SelectItem(_item);
        }

        protected virtual void SelectItem(T item)
        {
            _selectedItem.Value = item;
        }

        protected virtual void DeselectItem()
        {
            _selectedItem.Value = default;
        }
    }
}
