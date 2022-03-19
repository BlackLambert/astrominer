using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class SelectedAsteroidContextPanelCreator : MonoBehaviour, Injectable
	{
		[SerializeField]
		private Transform _hook;

		private Factory<AsteroidContextPanel, Asteroid> _factory;
		private SelectedAsteroid _selectedAsteroid;

		private AsteroidContextPanel _currentPanel;


		void Injectable.Inject(Resolver resolver)
		{
			_factory = resolver.Resolve<Factory<AsteroidContextPanel, Asteroid>>();
			_selectedAsteroid = resolver.Resolve<SelectedAsteroid>();
		}

		private void Start()
		{
			TryCreateNewContextInfoPanel();
			_selectedAsteroid.OnValueChanged += ReplaceContext;
		}

		private void OnDestroy()
		{
			_selectedAsteroid.OnValueChanged -= ReplaceContext;
		}

		private void ReplaceContext()
		{
			TryDestroyCurrentContext();
			TryCreateNewContextInfoPanel();
		}

		private void TryCreateNewContextInfoPanel()
		{
			if (_selectedAsteroid.HasValue)
				CreateContextInfoPanel();
		}

		private void CreateContextInfoPanel()
		{
			_currentPanel = _factory.Create(_selectedAsteroid.Value);
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
