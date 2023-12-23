using SBaier.DI;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BasePlacer : MonoBehaviour, Injectable
	{
		private readonly Vector2 _baseStartPosition = new Vector2(0, 0);
		
		private Players _players;
		private Pool<BasePlacementPreview, Player> _basePreviewPool;
		private BasesPlacementContext _context;
		private Map _map;
		private int _currentPlayerIndex = 0;

		public void Inject(Resolver resolver)
		{
			_players = resolver.Resolve<Players>();
			_basePreviewPool = resolver.Resolve<Pool<BasePlacementPreview, Player>>();
			_context = resolver.Resolve<BasesPlacementContext>();
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
				_context.CurrentPlayer.Value = null;
				_context.Finished.Value = true;
				return;
			}

			Player player = _players[_currentPlayerIndex];
			_context.CurrentPlayer.Value = player;
			CreateBase(player);
		}

		private void CreateBase(Player player)
		{
			BasePlacementPreview basePreview = _basePreviewPool.Request(player);
			basePreview.transform.position = _baseStartPosition;
			_currentPlayerIndex++;
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
