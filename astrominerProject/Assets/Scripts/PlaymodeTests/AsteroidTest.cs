using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
	public class AsteroidTest : FlyTargetTest
	{

		[Test]
		public void New_NotNull()
		{
			Asteroid asteroid = Asteroid.New();
			Assert.IsNotNull(asteroid);
			GameObject.Destroy(asteroid.gameObject);
		}

		[UnityTest]
		public IEnumerator Destroy_AsteroidDestroyed()
        {
			Asteroid asteroid = instantiateAsteroid();
			GameObject asteroidObject = asteroid.gameObject;
			asteroid.Destroy();
			yield return 0;
			Assert.True(asteroid == null);
			Assert.True(asteroidObject == null);
        }

		protected override FlyTarget instantiateTarget()
		{
			return instantiateAsteroid();
		}

		private Asteroid instantiateAsteroid()
		{
			return Asteroid.New();
		}
	}
}


