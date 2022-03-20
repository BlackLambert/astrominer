using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SelectedItemContextMenuCreator<T> : MonoBehaviour, Injectable
    {
		[SerializeField]
		private Transform _hook;

		private Factory<ContextPanel<T>, T> _factory;
		private ActiveItem<T> _selectedItem;

		private ContextPanel<T> _currentPanel;


		void Injectable.Inject(Resolver resolver)
		{
			_factory = resolver.Resolve<Factory<ContextPanel<T>, T>>();
			_selectedItem = resolver.Resolve<ActiveItem<T>>();
		}

		private void Start()
		{
			TryCreateNewContextInfoPanel();
			_selectedItem.OnValueChanged += ReplaceContext;
		}

		private void OnDestroy()
		{
			_selectedItem.OnValueChanged -= ReplaceContext;
		}

		private void ReplaceContext()
		{
			TryDestroyCurrentContext();
			TryCreateNewContextInfoPanel();
		}

		private void TryCreateNewContextInfoPanel()
		{
			if (_selectedItem.HasValue)
				CreateContextInfoPanel();
		}

		private void CreateContextInfoPanel()
		{
			_currentPanel = _factory.Create(_selectedItem.Value);
			_currentPanel.Base.SetParent(_hook, false);
		}

		private void TryDestroyCurrentContext()
		{
			if (_currentPanel != null)
				DestroyCurrentContext();
		}

		private void DestroyCurrentContext()
		{
			Destroy(_currentPanel.Base.gameObject);
			_currentPanel = null;
		}
	}
}
