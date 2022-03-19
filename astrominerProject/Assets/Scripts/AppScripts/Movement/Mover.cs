using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Mover : MonoBehaviour
    {
		[SerializeField]
		private Transform _moveable;
		private Vector2 _movementTarget;

		private Vector2 _vector2Position => transform.position;
		public float SpeedPerSeconds { get; set; } = 0;
		public event Action OnTargetReached;
		public bool TargetReached => _vector2Position == _movementTarget;

		private void Update()
		{
			Move();
		}

		public void SetMovementTarget(Vector2 target)
		{
			_movementTarget = target;
			_moveable.LookAt(_movementTarget);
		}

		private void Move()
		{
			if (TargetReached)
				return;
			float maxMovementDelta = SpeedPerSeconds * Time.deltaTime;
			Vector2 distanceVectorToTarget = _movementTarget - _vector2Position;
			float distanceToTarget = distanceVectorToTarget.magnitude;
			if (maxMovementDelta >= distanceToTarget)
				SetToTarget();
			else
				AddDelta(distanceVectorToTarget.normalized * maxMovementDelta);
			CheckTargetReached();
		}

		private void CheckTargetReached()
		{
			if (!TargetReached)
				return;
			OnTargetReached?.Invoke();
		}

		private void AddDelta(Vector2 delta)
		{
			_moveable.position = _vector2Position + delta;
		}

		private void SetToTarget()
		{
			_moveable.position = _movementTarget;
		}
	}
}
