using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astrominer
{
    public class ShipMovementTester : MonoBehaviour
    {
        [SerializeField]
        private List<FlyTarget> _Targets = new List<FlyTarget>();
        [SerializeField]
        private Ship _ship;

		private int _currentTargetIndex = -1;

        protected virtual void Start()
		{
            _ship.OnTargetReached += onTargetReached;
			moveToNext();
		}

        protected virtual void OnDestroy()
		{
			_ship.OnTargetReached -= onTargetReached;
		}

		private void onTargetReached()
		{
			moveToNext();
		}

		private void moveToNext()
		{
			_currentTargetIndex = (_currentTargetIndex + 1) % _Targets.Count;
			_ship.FlyTo(_Targets[_currentTargetIndex].Position);
		}
	}
}