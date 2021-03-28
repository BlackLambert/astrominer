using System.Collections;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
	public class MoonTest : FlyTargetTest
	{
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

		protected override FlyTarget InstantiateTarget()
		{
			return instantiateMoon();
		}

		private Moon instantiateMoon()
		{
			PreInstall();
			Container.Bind(typeof(FlyTarget), typeof(Moon)).To<Moon>().FromNewComponentOnNewGameObject().AsSingle();
			PostInstall();
			return Container.Resolve<Moon>();
		}
	}
}