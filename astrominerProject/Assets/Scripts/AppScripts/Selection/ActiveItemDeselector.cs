using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class ActiveItemDeselector<T> : MonoBehaviour, Injectable
    {
        private ActiveItem<T> _activeItem;
        private T _item;
        
        public void Inject(Resolver resolver)
        {
            _activeItem = resolver.Resolve<ActiveItem<T>>();
            _item = resolver.Resolve<T>();
        }

        private void OnDisable()
        {
            if (_activeItem.HasValue && _activeItem.Value.Equals(_item))
            {
                _activeItem.Value = default;
            }
        }
    }
}
