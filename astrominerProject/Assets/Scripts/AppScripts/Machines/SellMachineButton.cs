using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class SellMachineButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private Button _button;
        
        private Ship _ship;
        private ActiveItem<ShipInventoryItem> _selectedExploitMachine;
        private ExploitMachineVendor _vendor;
        
        public void Inject(Resolver resolver)
        {
            _ship = resolver.Resolve<Ship>();
            _vendor = resolver.Resolve<ExploitMachineVendor>();
            _selectedExploitMachine = resolver.Resolve<ActiveItem<ShipInventoryItem>>();
        }

        public void Initialize()
        {
            _button.onClick.AddListener(OnClick);
            _ship.Location.OnValueChanged += OnFlyTargetChanged;
            _selectedExploitMachine.OnValueChanged += OnSelectedMachineChanged;
            UpdateInteractable();
        }

        public void Clean()
        {
            _button.onClick.RemoveListener(OnClick);
            _ship.Location.OnValueChanged -= OnFlyTargetChanged;
            _selectedExploitMachine.OnValueChanged -= OnSelectedMachineChanged;
        }

        private void UpdateInteractable()
        {
            _button.interactable = IsInteractable();
        }

        private bool IsInteractable()
        {
            return _selectedExploitMachine.HasValue && 
                   _ship.Location.Value is Base playerBase && 
                   playerBase.Player == _ship.Player;
        }

        private void OnClick()
        {
            ExploitMachine machine = _selectedExploitMachine.Value.Machine;
            _vendor.SellMachine(_ship, machine);
            _ship.Machines.Remove(machine);
        }

        private void OnFlyTargetChanged(FlyTarget formervalue, FlyTarget newvalue)
        {
            UpdateInteractable();
        }

        private void OnSelectedMachineChanged(ShipInventoryItem formervalue, ShipInventoryItem newvalue)
        {
            UpdateInteractable();
        }
    }
}
