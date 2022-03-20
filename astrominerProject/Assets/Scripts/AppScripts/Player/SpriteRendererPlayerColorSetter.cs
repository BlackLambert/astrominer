using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class SpriteRendererPlayerColorSetter : MonoBehaviour, Injectable
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

		private Player _player;

		public void Inject(Resolver resolver)
		{
			_player = resolver.Resolve<Player>();
		}

		private void Start()
		{
			_spriteRenderer.color = _player.Color;
		}
	}
}
