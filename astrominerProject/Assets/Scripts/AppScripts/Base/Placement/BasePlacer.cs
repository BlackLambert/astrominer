using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BasePlacer : MonoBehaviour, Injectable, Initializable, Cleanable
	{
		[SerializeField] 
		private Transform _hook;
		
		private Players _players;
		private Pool<BasePlacementPreview, Player, PrefabInstantiationArguments> _basePreviewPool;
		private BasesPlacementContext _context;
		private int _currentPlayerIndex = 0;
		private List<BasePlacementPreview> _bases = new List<BasePlacementPreview>();
		private BasePositions _positions;
		private BasePositionGetter _basePositionGetter;
		private PointerPosition _pointerPosition;
		private Camera _camera;

		public void Inject(Resolver resolver)
		{
			_players = resolver.Resolve<Players>();
			_basePreviewPool = resolver.Resolve<Pool<BasePlacementPreview, Player, PrefabInstantiationArguments>>();
			_context = resolver.Resolve<BasesPlacementContext>();
			_positions = resolver.Resolve<BasePositions>();
			_basePositionGetter = resolver.Resolve<BasePositionGetter>();
			_pointerPosition = resolver.Resolve<PointerPosition>(0);
			_camera = resolver.Resolve<Camera>();
		}

		public void Initialize()
		{
			CreateNextBase();
			_context.Started.OnValueChanged += OnStartedChanged;
			_positions.OnItemAdded += OnBasePlaced;
		}

		public void Clean()
		{
			_context.Started.OnValueChanged -= OnStartedChanged;
			_positions.OnItemAdded -= OnBasePlaced;
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
			
			if (player.IsHuman)
			{
				CreateBase(player, _camera.ScreenToWorldPoint(_pointerPosition.CurrentPosition));
			}
			else
			{
				CreateComputerPlayerBase(player);
			}
		}

		private void CreateBase(Player player, Vector2 position)
		{
			PrefabInstantiationArguments args = CreateCreationArgs(position);
			_basePreviewPool.Request(player, args);
			_currentPlayerIndex++;
		}

		private void CreateComputerPlayerBase(Player player)
		{
			Vector2 position = _basePositionGetter.GetFor(player);
			CreateBase(player, position);
			_positions.Add(player, position);
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

		private PrefabInstantiationArguments CreateCreationArgs(Vector2 position)
		{
			return new PrefabInstantiationArguments()
			{
				Parent = _hook,
				Position = position,
				Rotation = Quaternion.identity
			};
		}
	}
}
