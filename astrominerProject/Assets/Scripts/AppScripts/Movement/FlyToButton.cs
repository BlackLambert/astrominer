using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class FlyToButton : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _button;

		private FlyTarget _target;
		private Ship _ship;

		public void Inject(Resolver resolver)
		{
			_target = resolver.Resolve<FlyTarget>();
			_ship = resolver.Resolve<Ship>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(MoveToTarget);
		}

		private void Update()
		{
			_button.interactable = IsInteractable();
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(MoveToTarget);
		}

		private void MoveToTarget()
		{
			_ship.FlyTo(_target);
		}

		private bool IsInteractable()
		{
			bool inRange = _target.IsInRange(_ship.Position2D, _ship.Range);
			bool sameTarget = _target == _ship.Location;
			return inRange && !sameTarget;
		}
	}
}
