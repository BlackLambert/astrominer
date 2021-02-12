using UnityEngine;
using NUnit.Framework;

namespace Astrominer.Test
{
    public abstract class FlyTargetTest
    {
		private Vector2 _testPosition = new Vector2(5.0f, 13.5f);

		[Test]
		public void PositionSet_SetsPosition()
		{
			FlyTarget flyTarget = instantiateTarget();
			flyTarget.Position = _testPosition;
			Assert.AreEqual(_testPosition, flyTarget.Position);
			GameObject.Destroy(flyTarget.gameObject);
		}

		[Test]
		public void PositionSet_TransformPositionEqualsPosition()
		{
			FlyTarget flyTarget = instantiateTarget();
			flyTarget.Position = _testPosition;
			Assert.AreEqual(flyTarget.Position, (Vector2)flyTarget.transform.position);
			GameObject.Destroy(flyTarget.gameObject);
		}

		protected abstract FlyTarget instantiateTarget();
	}
}