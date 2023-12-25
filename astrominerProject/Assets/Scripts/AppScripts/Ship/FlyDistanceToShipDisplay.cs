using System;
using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyDistanceToShipDisplay : MonoBehaviour, Injectable
    {
        [SerializeField]
        private string _baseText = "Distance to ship {0}m";
        [SerializeField]
        private TextMeshProUGUI _text;

        private ActiveItem<Ship> _activeShip;
        private FlyTarget _flyTarget;

		public void Inject(Resolver resolver)
		{
			_activeShip = resolver.Resolve<ActiveItem<Ship>>();
            _flyTarget = resolver.Resolve<FlyTarget>();
        }

		private void Update()
		{
            _text.text = FormatString();
        }

		private string FormatString()
		{
            return string.Format(_baseText, GetDistanceString());
		}

		private string GetDistanceString()
		{
            return _activeShip.HasValue ? _flyTarget.DistanceTo(_activeShip.Value.Position2D).ToString("n2") : "?";
		}
	}
}
