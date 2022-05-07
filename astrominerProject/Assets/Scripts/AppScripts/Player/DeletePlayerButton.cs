using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class DeletePlayerButton : MonoBehaviour, Injectable
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

        private void OnEnable()
        {
            _button.onClick.AddListener(DeletePlayer);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(DeletePlayer);
        }

        private void DeletePlayer()
        {
            _players.Values.Remove(_player);
        }
    }
}
