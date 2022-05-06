using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SelectedItemContextMenuCreator<T> : MonoBehaviour, Injectable
    {
		[SerializeField]
		private Transform _hook;

		private Pool<ContextPanel<T>, T> _pool;
		private ActiveItem<T> _selectedItem;

		private ContextPanel<T> _currentPanel;


		void Injectable.Inject(Resolver resolver)
		{
			_pool = resolver.Resolve<Pool<ContextPanel<T>, T>>();
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

		private void ReplaceContext()
		{
			TryReturnCurrentContext();
			TryCreateNewContextInfoPanel();
		}

		private void TryCreateNewContextInfoPanel()
		{
			if (_selectedItem.HasValue)
				CreateContextInfoPanel();
		}

		private void CreateContextInfoPanel()
		{
			_currentPanel = _pool.Request(_selectedItem.Value);
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
}
