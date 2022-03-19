using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ActionRadius : MonoBehaviour, Injectable
	{
		[SerializeField]
		private Transform _radiusImage;

		private Ship _ship;

		public void Inject(Resolver resolver)
		{
			_ship = resolver.Resolve<Ship>();
		}

		private void Start()
		{
			float radius = _ship.Range * 2;
			_radiusImage.localScale = new Vector3(radius, radius, radius);
		}
	}
}
