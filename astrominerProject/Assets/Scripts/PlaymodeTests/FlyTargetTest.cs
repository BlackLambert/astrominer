using UnityEngine;
using NUnit.Framework;
using Zenject;

namespace Astrominer.Test
{
    public abstract class FlyTargetTest : ZenjectIntegrationTestFixture
    {
		private Vector2 _testPosition = new Vector2(5.0f, 13.5f);

		[Test]
		public void PositionSet_SetsPosition()
		{
			FlyTarget flyTarget = InstantiateTarget();
			flyTarget.Position = _testPosition;
			Assert.AreEqual(_testPosition, flyTarget.Position);
			GameObject.Destroy(flyTarget.gameObject);
		}

		[Test]
		public void PositionSet_TransformPositionEqualsPosition()
		{
			FlyTarget flyTarget = InstantiateTarget();
			flyTarget.Position = _testPosition;
			Assert.AreEqual(flyTarget.Position, (Vector2)flyTarget.transform.position);
			GameObject.Destroy(flyTarget.gameObject);
		}

		protected abstract FlyTarget InstantiateTarget();
	}
}