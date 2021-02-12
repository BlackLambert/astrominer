using System.Collections;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
	public class MoonTest : FlyTargetTest
	{
		[Test]
		public void New_NotNull()
		{
			Moon moon = Moon.New();
			Assert.IsNotNull(moon);
			GameObject.Destroy(moon.gameObject);
		}

		[UnityTest]
		public IEnumerator Destroy_MoonDestroyed()
		{
			Moon moon = instantiateMoon();
			GameObject moonObject = moon.gameObject;
			moon.Destroy();
			yield return 0;
			Assert.True(moon == null);
			Assert.True(moonObject == null);
		}

		protected override FlyTarget instantiateTarget()
		{
			return instantiateMoon();
		}

		private Moon instantiateMoon()
		{
			return Moon.New();
		}
	}
}