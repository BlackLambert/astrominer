using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class DronesInstaller : MonoInstaller
	{
		[SerializeField] 
		private ProspectorDroneSettings _settings;
		
		public override void InstallBindings(Binder binder)
		{
			binder.BindToNewSelf<ProspectorDroneBuyer>();
			binder.BindInstance(_settings).WithoutInjection();
		}
	}
}
