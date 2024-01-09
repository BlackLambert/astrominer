using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ProspectorDroneInstaller : MonoInstaller, Injectable
    {
        [SerializeField] private DroneSettings _droneSettings;
        [SerializeField] private ProspectorDrone _drone;
        [SerializeField] private Mover _mover;

        private DroneArguments _arguments;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<DroneArguments>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(CreateMoverArguments());
            binder.Bind<Drone>().And<Flyable>().To<ProspectorDrone>().FromInstance(_drone)
                .WithoutInjection();
            binder.BindInstance(_mover).WithoutInjection();
            binder.BindInstance(_arguments).WithoutInjection();
            binder.BindInstance(_arguments.Player.IdentifiedAsteroids).WithoutInjection();
            binder.BindInstance(_arguments.Player).WithoutInjection();
            binder.BindToNewSelf<FlightPathMover>().AsSingle();
        }

        private Mover.Arguments CreateMoverArguments()
        {
            return new Mover.Arguments()
            {
                Acceleration = _droneSettings.Acceleration,
                BreakForce = _droneSettings.BreakForce,
                MaximalSpeed = _droneSettings.MaxSpeedPerSecond
            };
        }
    }
}