using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class DronesInstaller : MonoInstaller
	{
		[SerializeField] 
		private DroneSettings _prospectorDroneSettings;
		[SerializeField] 
		private DroneSettings _carrierDroneSettings;
		
		public override void InstallBindings(Binder binder)
		{
			binder.BindToNewSelf<DroneBuyer<ProspectorDrone>>().WithArgument(_prospectorDroneSettings);
			binder.BindToNewSelf<DroneBuyer<CarrierDrone>>().WithArgument(_carrierDroneSettings);
		}
	}
}
