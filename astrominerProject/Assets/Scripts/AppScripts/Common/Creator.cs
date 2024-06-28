using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class CreatorBase<TItem> : MonoBehaviour, Initializable, Cleanable
        where TItem : MonoBehaviour
    {
        [SerializeField] private Transform _hook;
        
        protected abstract event Action OnCanCreateChanged;
        
        private TItem _item;
        
        private PrefabInstantiationArguments _prefabInstantiationArguments;

        public virtual void Initialize()
        {
            _prefabInstantiationArguments = CreatePrefabInstantiationArguments(_hook);
            TryCreateItem();
            OnCanCreateChanged += TryReplaceItem;
        }
        
        public virtual void Clean()
        {
            TryCleanItem();
            OnCanCreateChanged -= TryReplaceItem;
        }
        
        private void TryCleanItem()
        {
            if (_item != null)
            {
                CleanItem(_item);
                _item = null;
            }
        }

        private void TryCreateItem()
        {
            if (CanCreateItem())
            {
                _item = CreateItem(_prefabInstantiationArguments);
            }
        }

        private void TryReplaceItem()
        {
            TryCleanItem();
            TryCreateItem();
        }
        
        protected abstract PrefabInstantiationArguments CreatePrefabInstantiationArguments(Transform hook);
        protected abstract bool CanCreateItem();
        protected abstract TItem CreateItem(PrefabInstantiationArguments prefabInstantiationArguments);
        protected abstract void CleanItem(TItem item);
    }
    
    public abstract class Creator<TItem> : CreatorBase<TItem>, Injectable where TItem : MonoBehaviour
    {
        private Pool<TItem, PrefabInstantiationArguments> _pool;
        
        public virtual void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<TItem, PrefabInstantiationArguments>>();
        }

        protected override TItem CreateItem(PrefabInstantiationArguments prefabInstantiationArguments)
        {
            return _pool.Request(prefabInstantiationArguments);
        }

        protected override void CleanItem(TItem item)
        {
            _pool.Return(item);
        }
    }

    public abstract class Creator<TItem, TArgument> : CreatorBase<TItem>, Injectable
        where TItem : MonoBehaviour
    {
        private Pool<TItem, TArgument, PrefabInstantiationArguments> _pool;
        
        public virtual void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<TItem, TArgument, PrefabInstantiationArguments>>();
        }

        protected override TItem CreateItem(PrefabInstantiationArguments prefabInstantiationArguments)
        {
            return _pool.Request(CreateArgument(), prefabInstantiationArguments);
        }

        protected override void CleanItem(TItem item)
        {
            _pool.Return(item);
        }

        protected abstract TArgument CreateArgument();
    }
}
