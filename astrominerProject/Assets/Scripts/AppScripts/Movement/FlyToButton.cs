using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class FlyToButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private Button _button;

		private Ship _ship;
		private ActiveItem<FlightPath> _selectedFlightPath;

		public void Inject(Resolver resolver)
		{
			_ship = resolver.Resolve<Ship>();
			_selectedFlightPath = resolver.Resolve<ActiveItem<FlightPath>>();
		}

		public void Initialize()
		{
			UpdateInteractable();
			_button.onClick.AddListener(MoveToTarget);
			_selectedFlightPath.OnValueChanged += OnSelectedFlightPathChanged;
		}

		public void Clean()
		{
			_selectedFlightPath.OnValueChanged -= OnSelectedFlightPathChanged;
			_button.onClick.RemoveListener(MoveToTarget);
		}

		private void OnSelectedFlightPathChanged(FlightPath formervalue, FlightPath newvalue)
		{
			UpdateInteractable();
		}

		private void MoveToTarget()
		{
			_ship.FlyTo(_selectedFlightPath.Value);
		}

		private void UpdateInteractable()
		{
			_button.interactable = IsInteractable();
		}

		private bool IsInteractable()
		{
			return _selectedFlightPath.HasValue && _selectedFlightPath.Value.FlyTargets.Count > 1;
		}
	}
}
