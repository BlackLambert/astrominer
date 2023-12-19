using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BasePlacer : MonoBehaviour, Injectable
	{
		private Players _players;
		private Pool<BasePlacementPreview> _basePreviewPool;

		public void Inject(Resolver resolver)
		{
			_players = resolver.Resolve<Players>();
			_basePreviewPool = resolver.Resolve<Pool<BasePlacementPreview>>();
		}

		private void Start()
		{
			CreateBases();
		}

		private void CreateBases()
		{
			foreach (Player player in _players)
				CreateBase(player);
		}

		private void CreateBase(Player player)
		{
			throw new NotImplementedException();
		}
	}
}
