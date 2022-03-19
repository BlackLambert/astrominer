using SBaier.DI;
using System;
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

        private Ship _ship;
        private FlyTarget _flyTarget;

		public void Inject(Resolver resolver)
		{
            _ship = resolver.Resolve<Ship>();
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
            return _flyTarget.DistanceTo(_ship.Position2D).ToString("n2");
		}
	}
}
