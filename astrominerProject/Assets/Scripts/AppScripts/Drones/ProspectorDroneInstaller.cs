using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ProspectorDroneInstaller : MonoInstaller, Injectable
	{
		[SerializeField]
		private ProspectorDroneSettings _prospectorDroneSettings;
		[SerializeField]
		private ProspectorDrone _prospectorDrone;
		[SerializeField]
		private Mover _mover;

		private DroneArguments _arguments;

		public void Inject(Resolver resolver)
		{
			_arguments = resolver.Resolve<DroneArguments>();
		}

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(new Mover.Arguments(_prospectorDroneSettings.SpeedPerSecond));
			binder.Bind<ProspectorDrone>().And<Flyable>().To<ProspectorDrone>().FromInstance(_prospectorDrone).WithoutInjection();
			binder.BindInstance(_mover).WithoutInjection();
			binder.BindInstance(_arguments).WithoutInjection();
		}
	}
}
