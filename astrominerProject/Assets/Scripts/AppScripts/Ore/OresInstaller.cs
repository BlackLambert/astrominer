using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class OresInstaller : MonoInstaller
	{
		[SerializeField]
		private OresSettings _oresSettings;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_oresSettings);
		}
	}
}
