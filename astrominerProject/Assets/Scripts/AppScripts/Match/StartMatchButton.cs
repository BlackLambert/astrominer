using SBaier.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class StartMatchButton : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _button;

		private Players _players;

		public void Inject(Resolver resolver)
		{
			_players = resolver.Resolve<Players>();
		}

		private void OnEnable()
		{
			CheckInteractable();
			_players.Values.OnItemsChanged += CheckInteractable;
		}

		private void OnDisable()
		{
			_players.Values.OnItemsChanged -= CheckInteractable;
		}

		private void CheckInteractable()
		{
			_button.interactable = CanStartMatch();
		}

		private bool CanStartMatch()
		{
			return _players.Values.ToReadonly().Count > 0;
		}
	}
}
