using SBaier.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class StartMatchButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private Button _button;

		private Players _players;

		public void Inject(Resolver resolver)
		{
			_players = resolver.Resolve<Players>();
		}

		public void Initialize()
		{
			CheckInteractable();
			_players.OnItemsChanged += CheckInteractable;
		}

		public void Clean()
		{
			_players.OnItemsChanged -= CheckInteractable;
		}

		private void CheckInteractable()
		{
			_button.interactable = CanStartMatch();
		}

		private bool CanStartMatch()
		{
			return _players.ToReadonly().Count > 0;
		}
	}
}
