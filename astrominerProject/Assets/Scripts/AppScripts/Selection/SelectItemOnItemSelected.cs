using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SelectItemOnItemSelected<TItem, TSelectedItem> : MonoBehaviour, Injectable, Initializable, Cleanable
        where TItem : TSelectedItem
    {
        private ActiveItem<TItem> _activeItem;
        private ActiveItem<TSelectedItem> _activeSelectedItem;

        public void Inject(Resolver resolver)
        {
            _activeItem = resolver.Resolve<ActiveItem<TItem>>();
            _activeSelectedItem = resolver.Resolve<ActiveItem<TSelectedItem>>();
        }

        public void Initialize()
        {
            _activeSelectedItem.OnValueChanged += OnSelectedItemChanged;
        }

        public void Clean()
        {
            _activeSelectedItem.OnValueChanged -= OnSelectedItemChanged;
        }

        private void OnSelectedItemChanged(TSelectedItem formervalue, TSelectedItem newvalue)
        {
            _activeItem.Value = newvalue is not TItem item ? default : item;
        }
    }
}