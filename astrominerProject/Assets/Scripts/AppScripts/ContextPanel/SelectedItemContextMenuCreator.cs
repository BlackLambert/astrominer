using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class SelectedItemContextMenuCreator<T, TArgument> : MonoBehaviour, Injectable
    {
		[SerializeField]
		private Transform _hook;

		private Pool<ContextPanel<T>, TArgument> _pool;
		protected ActiveItem<T> _selectedItem;

		private ContextPanel<T> _currentPanel;


		public virtual void Inject(Resolver resolver)
		{
			_pool = resolver.Resolve<Pool<ContextPanel<T>, TArgument>>();
			_selectedItem = resolver.Resolve<ActiveItem<T>>();
		}

		private void OnEnable()
		{
			TryCreateNewContextInfoPanel();
			_selectedItem.OnValueChanged += ReplaceContext;
		}

		private void OnDisable()
		{
			TryReturnCurrentContext();
			_selectedItem.OnValueChanged -= ReplaceContext;
		}

		protected abstract TArgument CreateArgument();

		protected virtual bool CanCreateContextPanel()
		{
			return _selectedItem.HasValue;
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
			_currentPanel = _pool.Request(CreateArgument());
			_currentPanel.Base.SetParent(_hook, false);
		}

		private void TryReturnCurrentContext()
		{
			if (_currentPanel != null)
				ReturnCurrentContext();
		}

		private void ReturnCurrentContext()
		{
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
