using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SpriteRendererPlayerColorSetter : MonoBehaviour, Injectable, Initializable
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

		private Player _player;

		public void Inject(Resolver resolver)
		{
			_player = resolver.Resolve<Player>();
		}

		public void Initialize()
		{
			_spriteRenderer.color = _player.Color;
		}
	}
}
