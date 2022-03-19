using SBaier.DI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SBaier.Astrominer
{
	public class Asteroid : MonoBehaviour, IPointerClickHandler, Injectable
	{
		[field: SerializeField]
		public Transform Base { get; private set; }
		[field: SerializeField]
		public FlyTarget FlyTarget { get; private set; }

		private SelectedAsteroid _selectedAsteroid;
		private Arguments _arguments;

		void Injectable.Inject(Resolver resolver)
		{
			_selectedAsteroid = resolver.Resolve<SelectedAsteroid>();
			_arguments = resolver.Resolve<Arguments>();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			_selectedAsteroid.Value = this;
		}

		public class Arguments
		{
			public float ResourceAmount { get; }

			public Arguments(float resourceAmount)
			{
				ResourceAmount = resourceAmount;
			}
		}
	}
}
