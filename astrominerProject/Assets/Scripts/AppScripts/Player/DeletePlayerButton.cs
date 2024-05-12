using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class DeletePlayerButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private Button _button;

        private Player _player;
        private Players _players;

        public void Inject(Resolver resolver)
        {
            _player = resolver.Resolve<Player>();
            _players = resolver.Resolve<Players>();
        }

        public void Initialize()
        {
            _button.onClick.AddListener(DeletePlayer);
        }

        public void Clean()
        {
            _button.onClick.RemoveListener(DeletePlayer);
        }

        private void DeletePlayer()
        {
            _players.Remove(_player);
        }
    }
}
