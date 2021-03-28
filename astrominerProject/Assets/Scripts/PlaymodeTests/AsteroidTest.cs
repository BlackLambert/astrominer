using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
	[TestFixture]
	public class AsteroidTest : FlyTargetTest
	{

		[UnityTest]
		public IEnumerator Destroy_AsteroidDestroyed()
        {
			Asteroid asteroid = InstantiateAsteroid();
			GameObject asteroidObject = asteroid.gameObject;
			asteroid.Destroy();
			yield return 0;
			Assert.True(asteroid == null);
			Assert.True(asteroidObject == null);
        }

		protected override FlyTarget InstantiateTarget()
		{
			return InstantiateAsteroid();
		}

		private Asteroid InstantiateAsteroid()
		{
			PreInstall();
			Container.Bind(typeof(FlyTarget), typeof(Asteroid)).To<Asteroid>().FromNewComponentOnNewGameObject().AsSingle();
			PostInstall();
			return Container.Resolve<Asteroid>();
		}
	}
}


