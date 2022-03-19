using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyTargetInRangeOfShipDisplay : MonoBehaviour, Injectable
    {
        private const string _inRangeText = "is";
        private const string _notInRangeText = "is not";

        [SerializeField]
        private string _baseText = "Ship {0} in range.";
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private Color _inRangeColor = Color.green;
        [SerializeField]
        private Color _notInRangeColor = Color.red;

        private Ship _ship;
        private FlyTarget _flyTarget;

		public void Inject(Resolver resolver)
		{
            _ship = resolver.Resolve<Ship>();
            _flyTarget = resolver.Resolve<FlyTarget>();
        }

		private void Update()
		{
            bool inRange = _flyTarget.IsInRange(_ship.Position2D, _ship.Range);
            _text.text = FormatString(inRange);
            _text.color = GetColor(inRange);
        }

		private string FormatString(bool inRange)
		{
            return string.Format(_baseText, GetInputText(inRange));
        }

        private string GetInputText(bool inRange)
		{
            return inRange ? _inRangeText : _notInRangeText;
        }

        private Color GetColor(bool inRange)
        {
            return inRange ? _inRangeColor : _notInRangeColor;
        }
	}
}
