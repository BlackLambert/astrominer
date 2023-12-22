using SBaier.DI;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BasePlacer : MonoBehaviour, Injectable
	{
		private Players _players;
		private Pool<BasePlacementPreview, Player> _basePreviewPool;
		private BasePlacementContext _context;
		private Map _map;
		private int _currentPlayerIndex = 0;

		public void Inject(Resolver resolver)
		{
			_players = resolver.Resolve<Players>();
			_basePreviewPool = resolver.Resolve<Pool<BasePlacementPreview, Player>>();
			_context = resolver.Resolve<BasePlacementContext>();
			_map = resolver.Resolve<Map>();
		}

		private void OnEnable()
		{
			CreateNextBase();
			_context.Started.OnValueChanged += OnStartedChanged;
			_context.OnBaseAdded += OnBasePlaced;
		}

		private void OnDisable()
		{
			_context.Started.OnValueChanged -= OnStartedChanged;
		}

		private void CreateNextBase()
		{
			if (!_context.Started.Value)
			{
				return;
			}

			if (_currentPlayerIndex >= _players.Count)
			{
				_map.BasePositions.Value = _context.PlayerToPosition.ToDictionary(pair => pair.Key, pair => pair.Value);
				_context.Finished.Value = true;
				return;
			}
			
			CreateBase(_players[_currentPlayerIndex]);
		}

		private void CreateBase(Player player)
		{
			_basePreviewPool.Request(player);
		}

		private void OnStartedChanged(bool formervalue, bool newvalue)
		{
			CreateNextBase();
		}

		private void OnBasePlaced(Player player)
		{
			CreateNextBase();
		}
	}
}
