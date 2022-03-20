using SBaier.DI;
using System;
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

		Arguments _arguments;
		public int Quality => _arguments.Quality;
		public int Size => _arguments.Size;

		void Injectable.Inject(Resolver resolver)
		{
			_arguments = resolver.Resolve<Arguments>();
		}

		public void SetObjectSize(float size)
        {
			_image.transform.localScale = new Vector3(size, size, size);
		}

		public void SetPosition(Vector2 position)
        {
			Base.position = position;
		}

		public void SetRotation(Quaternion rotation)
		{
			_image.transform.rotation = rotation;
		}

		public void SetName(string name)
        {
			Base.name = name;
		}

		public class Arguments
		{
			public int Quality { get; }
			public int Size { get; }

			public Arguments(int quality,
				int size)
			{
				Quality = quality;
				Size = size;
			}
		}
	}
}
