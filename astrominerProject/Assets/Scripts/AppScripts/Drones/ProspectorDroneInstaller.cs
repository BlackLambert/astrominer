using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ProspectorDroneInstaller : MonoInstaller, Injectable
    {
        [SerializeField] private ProspectorDroneSettings _prospectorDroneSettings;
        [SerializeField] private ProspectorDrone _prospectorDrone;
        [SerializeField] private Mover _mover;

        private DroneArguments _arguments;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<DroneArguments>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(CreateMoverArguments());
            binder.Bind<ProspectorDrone>().And<Flyable>().To<ProspectorDrone>().FromInstance(_prospectorDrone)
                .WithoutInjection();
            binder.BindInstance(_mover).WithoutInjection();
            binder.BindInstance(_arguments).WithoutInjection();
            binder.BindInstance(_arguments.Player.IdentifiedAsteroids).WithoutInjection();
            binder.BindInstance(_arguments.Player).WithoutInjection();
        }

        private Mover.Arguments CreateMoverArguments()
        {
            return new Mover.Arguments()
            {
                Acceleration = _prospectorDroneSettings.Acceleration,
                BreakForce = _prospectorDroneSettings.BreakForce,
                MaximalSpeed = _prospectorDroneSettings.MaxSpeedPerSecond
            };
        }
    }
}