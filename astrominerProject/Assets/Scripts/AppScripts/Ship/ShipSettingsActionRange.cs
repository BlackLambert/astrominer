using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipSettingsActionRange : ActionRange, Injectable
    {
        public float Range => _shipSettings.ActionRadius;

        private ShipSettings _shipSettings;
        
        public void Inject(Resolver resolver)
        {
            _shipSettings = resolver.Resolve<ShipSettings>();
        }
    }
}
