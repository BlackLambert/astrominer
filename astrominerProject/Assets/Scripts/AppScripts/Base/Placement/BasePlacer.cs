using System;
using System.Collections.Generic;
using SBaier.DI;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BasePlacer : MonoBehaviour, Injectable
	{
		private readonly Vector2 _baseStartPosition = new Vector2(0, 0);

		[SerializeField] 
		private Transform _hook;
		
		private Players _players;
		private Pool<BasePlacementPreview, Player> _basePreviewPool;
		private BasesPlacementContext _context;
		private int _currentPlayerIndex = 0;
		private List<BasePlacementPreview> _bases = new List<BasePlacementPreview>();
		private BasePositions _positions;

		public void Inject(Resolver resolver)
		{
			_players = resolver.Resolve<Players>();
			_basePreviewPool = resolver.Resolve<Pool<BasePlacementPreview, Player>>();
			_context = resolver.Resolve<BasesPlacementContext>();
			_positions = resolver.Resolve<BasePositions>();
		}

		private void OnEnable()
		{
			CreateNextBase();
			_context.Started.OnValueChanged += OnStartedChanged;
			_positions.OnItemAdded += OnBasePlaced;
		}

		private void OnDisable()
		{
			_context.Started.OnValueChanged -= OnStartedChanged;
			_positions.OnItemAdded -= OnBasePlaced;
		}

		private void OnDestroy()
		{
			ClearBases();
		}

		private void CreateNextBase()
		{
			if (!_context.Started.Value)
			{
				return;
			}

			if (_currentPlayerIndex >= _players.Count)
			{
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
			Transform baseTransform = basePreview.transform;
			baseTransform.SetParent(_hook);
			baseTransform.position = _baseStartPosition;
			_currentPlayerIndex++;
		}

		private void ClearBases()
		{
			foreach (BasePlacementPreview preview in _bases)
			{
				_basePreviewPool.Return(preview);
			}
		}

		private void OnStartedChanged(bool formervalue, bool newvalue)
		{
			CreateNextBase();
		}

		private void OnBasePlaced(KeyValuePair<Player, Vector2> keyValuePair)
		{
			CreateNextBase();
		}
	}
}
