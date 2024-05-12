using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidColorSetter : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

		private Asteroid _asteroid;

		public void Inject(Resolver resolver)
		{
			_asteroid = resolver.Resolve<Asteroid>();
		}

		public void Initialize()
		{
			UpdateColor();
			_asteroid.OnOwningPlayerChanged += UpdateColor;
			_asteroid.OnExploited += UpdateColor;
		}

		public void Clean()
		{
			_asteroid.OnOwningPlayerChanged -= UpdateColor;
			_asteroid.OnExploited -= UpdateColor;
		}

		private void UpdateColor()
		{
			Color reduction = _asteroid.Exploited ? _asteroid.ExploitedColorReduction : new Color(0,0,0,0);
			Color baseColor = _asteroid.HasOwningPlayer ? _asteroid.OwningPlayer.Color : _asteroid.BaseColor;
			_spriteRenderer.color = baseColor - reduction;
		}
	}
}
