using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SelectItemOnItemSelected<TItem, TSelectedItem> : MonoBehaviour, Injectable where TItem : TSelectedItem
    {
        private ActiveItem<TItem> _activeItem;
        private ActiveItem<TSelectedItem> _activeSelectedItem;

        public void Inject(Resolver resolver)
        {
            _activeItem = resolver.Resolve<ActiveItem<TItem>>();
            _activeSelectedItem = resolver.Resolve<ActiveItem<TSelectedItem>>();
        }

        private void OnEnable()
        {
            _activeSelectedItem.OnValueChanged += OnSelectedItemChanged;
        }

        private void OnDisable()
        {
            _activeSelectedItem.OnValueChanged -= OnSelectedItemChanged;
        }

        private void OnSelectedItemChanged(TSelectedItem formervalue, TSelectedItem newvalue)
        {
            _activeItem.Value = newvalue is not TItem item ? default : item;
        }
    }
}
