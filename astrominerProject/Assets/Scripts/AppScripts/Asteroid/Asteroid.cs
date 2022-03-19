using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class Asteroid : MonoBehaviour, Injectable
	{
		[field: SerializeField]
		public Transform Base { get; private set; }
		[field: SerializeField]
		public FlyTarget FlyTarget { get; private set; }
		[SerializeField]
		private Transform _image;

		private Arguments _arguments;

		void Injectable.Inject(Resolver resolver)
		{
			_arguments = resolver.Resolve<Arguments>();
		}

		public void SetSize(float size)
        {
			_image.transform.localScale = new Vector3(size, size, size);
		}

		public void SetPosition(Vector2 position)
        {
			Base.position = position;
        }

		public void SetName(string name)
        {
			Base.name = name;
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
