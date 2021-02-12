using System;
using UnityEngine;

namespace Astrominer
{
	public class Moon : FlyTarget
	{
		private static readonly string _moonPrefabPath = "Prefabs/Moon";

		public static Moon New()
		{
			Moon prefab = Resources.Load<Moon>(_moonPrefabPath);
			Moon result = GameObject.Instantiate(prefab);
			return result;
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}