using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CarrierDroneInstaller : MonoInstaller, Injectable
    {
        [SerializeField] private DroneSettings _carrierDroneSettings;
        [SerializeField] private CarrierDrone _drone;
        [SerializeField] private Mover _mover;

        private DroneArguments _arguments;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<DroneArguments>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(CreateMoverArguments());
            binder.Bind<Drone>().And<Flyable>().And<FlyableObject>().To<CarrierDrone>().FromInstance(_drone)
                .WithoutInjection();
            binder.BindInstance(_mover).WithoutInjection();
            binder.BindInstance(_arguments).WithoutInjection();
            binder.BindInstance(_arguments.Player).WithoutInjection();
            binder.BindToNewSelf<FlightPathMover>().AsSingle();
            binder.BindToNewSelf<Ores>().AsSingle();
        }

        private Mover.Arguments CreateMoverArguments()
        {
            return new Mover.Arguments()
            {
                Acceleration = _carrierDroneSettings.Acceleration,
                BreakForce = _carrierDroneSettings.BreakForce,
                MaximalSpeed = _carrierDroneSettings.MaxSpeedPerSecond
            };
        }
    }
}