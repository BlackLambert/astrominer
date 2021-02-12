using System;
using UnityEngine;

namespace Astrominer
{
	public class Asteroid : FlyTarget
	{
		private static readonly string _asteroidPrefabPath = "Prefabs/Asteroid";

		
		public static Asteroid New()
		{
			Asteroid prefab = Resources.Load<Asteroid>(_asteroidPrefabPath);
			Asteroid result = GameObject.Instantiate(prefab);
			return result;
		}

        public void Destroy()
        {
			Destroy(gameObject);
        }
    }
}