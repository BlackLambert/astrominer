using System;
using UnityEngine;

namespace Astrominer
{
	public class Asteroid : FlyTarget
	{
        public void Destroy()
        {
			Destroy(gameObject);
        }
    }
}