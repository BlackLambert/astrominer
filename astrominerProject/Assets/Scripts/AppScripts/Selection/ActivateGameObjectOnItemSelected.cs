using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ActivateGameObjectOnItemSelected<TItem> : MonoBehaviour, Injectable
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _reverse = false;

        private ActiveItem<TItem> _selectedItem;
        private TItem _item;
        
        public void Inject(Resolver resolver)
        {
            _selectedItem = resolver.Resolve<ActiveItem<TItem>>();
            _item = resolver.Resolve<TItem>();
        }

        private void OnEnable()
        {
            UpdateActiveState();
            _selectedItem.OnValueChanged += OnSelectionChanged;
        }

        private void OnDisable()
        {
            _selectedItem.OnValueChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged(TItem formervalue, TItem newvalue)
        {
            UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            bool equals = _selectedItem.HasValue && _selectedItem.Value.Equals(_item);
            _target.SetActive(_reverse && !equals || !_reverse && equals);
        }
    }
}