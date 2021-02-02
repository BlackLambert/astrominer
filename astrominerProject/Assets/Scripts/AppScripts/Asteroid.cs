using System;
using UnityEngine;

namespace Astrominer
{
	public class Asteroid : MonoBehaviour
	{
		private readonly Vector2 _defaultPosition = Vector2.zero;
		private static readonly string _asteroidPrefabPath = "Prefabs/Asteroid";

		public Vector2 Position 
		{ 
			get => transform.position;
			set => transform.position = value;
		}

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