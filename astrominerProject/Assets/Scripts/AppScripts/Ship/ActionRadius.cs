using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ActionRadius : MonoBehaviour, Injectable
	{
		[SerializeField]
		private Transform _radiusImage;
		[SerializeField]
		private SpriteRenderer _spriteRenderer;

		private Ship _ship;
		private VisualsSettings _visualSettings;

		public void Inject(Resolver resolver)
		{
			_ship = resolver.Resolve<Ship>();
			_visualSettings = resolver.Resolve<VisualsSettings>();
		}

		private void Start()
		{
			float radius = _ship.Range * 2;
			_radiusImage.localScale = new Vector3(radius, radius, radius);
			_spriteRenderer.color = _visualSettings.ActionRadiusColor;
		}
	}
}
