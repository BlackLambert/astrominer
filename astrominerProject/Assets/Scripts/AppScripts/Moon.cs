using System;
using UnityEngine;

namespace Astrominer
{
	public class Moon : FlyTarget
	{
		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}