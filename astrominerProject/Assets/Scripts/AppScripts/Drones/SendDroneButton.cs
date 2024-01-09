using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public abstract class SendDroneButton<TDrone> : MonoBehaviour, Injectable where TDrone : Drone
    {
        [SerializeField]
        private Button _button;

        protected Player _player;
        protected Asteroid _target;
        
        private Drones _drones;
        private Ship _ship;
        private Base _base;
        private DroneBuyer<TDrone> _buyer;
        private DroneSettings _settings;

        public virtual void Inject(Resolver resolver)
        {
            _drones = resolver.Resolve<Drones>();
            _target = resolver.Resolve<Asteroid>();
            _ship = resolver.Resolve<Ship>();
            _base = resolver.Resolve<Base>();
            _player = resolver.Resolve<Player>();
            _buyer = resolver.Resolve<DroneBuyer<TDrone>>();
            _settings = resolver.Resolve<DroneSettings>();
        }

        protected virtual void OnEnable()
        {
            UpdateButtonActive();
            _button.onClick.AddListener(SendDrone);
            _drones.OnItemRemoved += UpdateButtonActive;
            _drones.OnItemAdded += UpdateButtonActive;
            _ship.FlyTarget.OnValueChanged += OnFlyTargetChanged;
        }

        protected virtual void OnDisable()
        {
            _button.onClick.RemoveListener(SendDrone);
            _drones.OnItemRemoved -= UpdateButtonActive;
            _drones.OnItemAdded -= UpdateButtonActive;
            _ship.FlyTarget.OnValueChanged -= OnFlyTargetChanged;
            _player.Credits.OnAmountChanged += UpdateButtonActive;
        }

        private void OnFlyTargetChanged(FlightPath formervalue, FlightPath newvalue)
        {
            UpdateButtonActive();
        }

        private void UpdateButtonActive(Drone drone)
        {
            UpdateButtonActive();
        }

        protected virtual bool GetButtonActive()
        {
            return !_ship.IsFlying &&
                   _player.Credits.Has(_settings.Price) &&
                   _ship.Location.Value as Asteroid != _target &&
                   !_drones.ContainsDroneTo<TDrone>(_target);
        }

        protected void UpdateButtonActive()
        {
            _button.interactable = GetButtonActive();
        }

        private void SendDrone()
        {
            _drones.Add(_buyer.BuyDrone(_ship, _target, _base));
        }
    }
}
