using System;
using UnityEngine;

namespace Astrominer
{
	public class LinearMover : Mover
	{
		[SerializeField]
		private Transform _objectToMove;
		[SerializeField]
		private float _defaultSpeed = 1f;
		private Vector2 _target;
		private float _speed = 1.0f;

		public override event Action OnTargetReached;
		public float Speed
		{
			get => _speed;
			set
			{
				if (value <= 0)
					throw new NegativeSpeedValueException();
				_speed = value;
			}
		}
		public float TimeToTarget => _distanceToTarget / Speed;
		public override Vector2 DistanceVectorToTarget => (_target - (Vector2)_objectToMove.position);



		protected virtual void Awake()
		{
			_target = _objectToMove.position;
			Speed = _defaultSpeed;
		}


		public override void MoveTo(Vector2 target)
		{
			_target = target;
		}

		protected virtual void FixedUpdate()
		{
			checkMoveToTarget();
		}

		private void checkMoveToTarget()
		{
			if (_distanceToTarget == 0)
				return;
			moveToTarget();
		}

		private void moveToTarget()
		{
			if (_targetWithinSpeedRange)
				set2DPositionToTarget();
			else
				moveBySpeedPerFixedUpdate();
		}

		private void moveBySpeedPerFixedUpdate()
		{
			_objectToMove.position += positionFixedUpdateDelta;
		}

		private void set2DPositionToTarget()
		{
			_objectToMove.position = new Vector3(_target.x, _target.y, _objectToMove.position.z);
			OnTargetReached?.Invoke();
		}

		private float _speedPerFixedUpdate => Speed * Time.fixedDeltaTime;
		private float _distanceToTarget => DistanceVectorToTarget.magnitude;
		private Vector2 _normalizedDistanceVectorToTarget => DistanceVectorToTarget.normalized;
		private bool _targetWithinSpeedRange => DistanceVectorToTarget.magnitude <= _speedPerFixedUpdate;
		private Vector3 positionFixedUpdateDelta => (Vector3)_normalizedDistanceVectorToTarget * _speedPerFixedUpdate;

		public class NegativeSpeedValueException : ArgumentOutOfRangeException
		{
		}
	}
}