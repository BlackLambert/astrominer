using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BasePlacer : MonoBehaviour, Injectable
	{
		private Players _players;
		private Pool<Base> _basePool;

		public void Inject(Resolver resolver)
		{
			_players = resolver.Resolve<Players>();
			_basePool = resolver.Resolve<Pool<Base>>();
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
