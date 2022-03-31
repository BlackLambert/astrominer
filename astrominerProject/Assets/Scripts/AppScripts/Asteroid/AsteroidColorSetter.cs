using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidColorSetter : MonoBehaviour, Injectable
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

		private Asteroid _asteroid;

		public void Inject(Resolver resolver)
		{
			_asteroid = resolver.Resolve<Asteroid>();
		}

		private void Start()
		{
			UpdateColor();
			_asteroid.OnOwningPlayerChanged += UpdateColor;
		}

		private void OnDestroy()
		{
			_asteroid.OnOwningPlayerChanged -= UpdateColor;
		}

		private void UpdateColor()
		{
			_spriteRenderer.color = _asteroid.HasOwningPlayer ? 
				_asteroid.OwningPlayer.Color : _asteroid.BaseColor;
		}
	}
}
