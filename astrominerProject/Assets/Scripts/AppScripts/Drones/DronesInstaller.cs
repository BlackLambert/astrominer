using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class DronesInstaller : MonoInstaller
	{
		[SerializeField]
		private ProspectorDrone _prospectorDronePrefab;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Factory<ProspectorDrone, DroneArguments>>().
				ToNew<PrefabFactory<ProspectorDrone, DroneArguments>>().
				WithArgument(_prospectorDronePrefab);
		}
	}
}
