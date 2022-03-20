using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class BaseInstaller : MonoInstaller
	{
		[SerializeField]
		private Selectable _selectable;
		[SerializeField]
		private Base _base;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_selectable).WithoutInjection();
			binder.BindInstance(_base.FlyTarget).WithoutInjection();
			binder.BindInstance(_base).WithoutInjection();
		}
	}
}
