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
		private string _asteroidPrefabPath => ResourcesPaths.asteroidPrefab;

        [SetUp]
		public void Initialize()
		{
			Asteroid prefab = Resources.Load<Asteroid>(_asteroidPrefabPath);
			_asteroid = GameObject.Instantiate(prefab);
		}

		[TearDown]
		public void Dispose()
		{
			GameObject.Destroy(_asteroid.gameObject);
		}

		[Test]
		public void NewAsteroidHasDefaultPosition()
		{
			Assert.AreEqual(_asteroid.defaultPosition, _asteroid.Position);
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
	}
}


