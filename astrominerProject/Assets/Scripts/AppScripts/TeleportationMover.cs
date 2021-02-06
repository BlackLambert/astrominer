using System;
using System.Collections;
using UnityEngine;

namespace Astrominer
{
    public class TeleportationMover : Mover
    {
        [SerializeField]
        private Transform _objectToMove;
        private Vector2 _target;

		public override Vector2 DistanceVectorToTarget => _target - (Vector2) _objectToMove.position;

		public override event Action OnTargetReached;

        public override void MoveTo(Vector2 target)
        {
            _target = target;
            StartCoroutine(TeleportToTargetWithinNextFrame());
        }

        private IEnumerator TeleportToTargetWithinNextFrame()
		{
            yield return 0;
            _objectToMove.position = new Vector3(_target.x, _target.y, _objectToMove.position.z);
            OnTargetReached?.Invoke();
        }
    }

}


