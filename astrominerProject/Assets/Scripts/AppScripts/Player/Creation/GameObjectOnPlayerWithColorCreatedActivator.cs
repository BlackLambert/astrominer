using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GameObjectOnPlayerWithColorCreatedActivator : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private GameObject _target;

        private Players _players;
        private PlayerColorOption _item;

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _item = resolver.Resolve<PlayerColorOption>();
        }

        private void OnEnable()
        {
            UpdateSelectorActiveState();
            _players.OnItemsChanged += UpdateSelectorActiveState;
        }

        private void OnDisable()
        {
            _players.OnItemsChanged -= UpdateSelectorActiveState;
        }

        private void UpdateSelectorActiveState()
        {
            _target.SetActive(_players.FirstOrDefault(player => player.Color == _item.Color) != null);
        }
    }
}
