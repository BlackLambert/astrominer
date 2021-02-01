using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
	public class AsteroidTest
	{
		private Asteroid _asteroid;
		private Vector2 _testPosition = new Vector2(5.0f, 13.5f);


        [SetUp]
		public void Initialize()
		{
			_asteroid = Asteroid.New();
		}

		[TearDown]
		public void Dispose()
		{
			if (_asteroid != null)
				_asteroid.Destroy();
		}

		[Test]
		public void NewAsteroidHasDefaultPosition()
		{
			Assert.AreEqual(Vector2.zero, _asteroid.Position);
		}

		[Test]
		public void PositionSet_AsteroidPositionSet()
		{
			_asteroid.Position = _testPosition;
			Assert.AreEqual(_testPosition, _asteroid.Position);
		}

		[Test]
		public void PositionSet_AsteroidObjectPositionEqualsAsteroidPosition()
		{
			_asteroid.Position = _testPosition;
			Assert.AreEqual(_asteroid.Position, (Vector2)_asteroid.transform.position);
		}

		[UnityTest]
		public IEnumerator Destroy_AsteroidDestroyed()
        {
			GameObject asteroidObject = _asteroid.gameObject;
			_asteroid.Destroy();
			yield return 0;
			Assert.True(_asteroid == null);
			Assert.True(asteroidObject == null);
        }
			
	}
}


