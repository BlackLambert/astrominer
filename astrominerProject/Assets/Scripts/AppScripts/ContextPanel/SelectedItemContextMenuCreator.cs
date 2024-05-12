using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class SelectedItemContextMenuCreator<T, TArgument> : MonoBehaviour, Injectable, Initializable,
        Cleanable
    {
        [SerializeField] private Transform _hook;

        private Pool<ContextPanel<T>, TArgument, PrefabInstantiationArguments> _pool;
        protected ActiveItem<T> _selectedItem;

        private ContextPanel<T> _currentPanel;


        public virtual void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<ContextPanel<T>, TArgument, PrefabInstantiationArguments>>();
            _selectedItem = resolver.Resolve<ActiveItem<T>>();
        }

        public void Initialize()
        {
            TryCreateNewContextInfoPanel();
            _selectedItem.OnValueChanged += OnSelectedItemChanged;
        }

        public void Clean()
        {
            TryReturnCurrentContext();
            _selectedItem.OnValueChanged -= OnSelectedItemChanged;
        }

        protected abstract TArgument CreateArgument();

        protected virtual bool CanCreateContextPanel()
        {
            return _selectedItem.HasValue;
        }

        private void OnSelectedItemChanged(T formerValue, T newValue)
        {
            ReplaceContext();
        }

        private void ReplaceContext()
        {
            TryReturnCurrentContext();
            TryCreateNewContextInfoPanel();
        }

        private void TryCreateNewContextInfoPanel()
        {
            if (CanCreateContextPanel())
                CreateContextInfoPanel();
        }


        private void CreateContextInfoPanel()
        {
            _currentPanel = _pool.Request(CreateArgument(), PrefabInstantiationArguments.CreateFittedUIArgs(_hook));
        }

        private void TryReturnCurrentContext()
        {
            if (_currentPanel == null)
            {
                return;
            }

            ReturnCurrentContext();
        }

        private void ReturnCurrentContext()
        {
            _currentPanel.InvokeOnPooling();
            _pool.Return(_currentPanel);
            _currentPanel = null;
        }
    }

    public abstract class SelectedItemContextMenuCreator<T> : SelectedItemContextMenuCreator<T, T>
    {
        protected override T CreateArgument()
        {
            return _selectedItem.Value;
        }
    }
}