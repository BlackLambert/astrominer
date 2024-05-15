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
		private bool _tiggered = false;

		public void Inject(Resolver resolver)
		{
			_players = resolver.Resolve<Players>();
		}

		public void Initialize()
		{
			CheckInteractable();
			_tiggered = false;
			_players.OnItemsChanged += CheckInteractable;
			_button.onClick.AddListener(OnButtonClicked);
		}

		public void Clean()
		{
			_tiggered = false;
			_players.OnItemsChanged -= CheckInteractable;
			_button.onClick.RemoveListener(OnButtonClicked);
		}

		private void CheckInteractable()
		{
			_button.interactable = CanStartMatch() && !_tiggered;
		}

		private bool CanStartMatch()
		{
			return _players.ToReadonly().Count > 0;
		}
		
		private void OnButtonClicked()
		{
			_tiggered = true;
			CheckInteractable();
		}
	}
}
