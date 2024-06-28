using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CarryingOresPanelCreator : Creator<CarryingOresPanel, Ship>
    {
        protected override event Action OnCanCreateChanged;

        private ActiveShip _activeShip;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _activeShip = resolver.Resolve<ActiveShip>();
        }

        public override void Initialize()
        {
            base.Initialize();
            _activeShip.OnValueChanged += OnShipChanged;
        }

        public override void Clean()
        {
            base.Clean();
            _activeShip.OnValueChanged -= OnShipChanged;
        }

        protected override Ship CreateArgument() => _activeShip.Value;
        protected override bool CanCreateItem() => _activeShip.HasValue;
        protected override PrefabInstantiationArguments CreatePrefabInstantiationArguments(Transform hook)
            => PrefabInstantiationArguments.CreateFittedUIArgs(hook);
        private void OnShipChanged(Ship formervalue, Ship newvalue) => OnCanCreateChanged?.Invoke();
    }
}