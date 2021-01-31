using System;
using UnityEngine;

namespace Astrominer
{
	public class Ship : MonoBehaviour
	{
		public readonly Vector2 defaultPosition = Vector2.zero;
		public readonly Vector2 defaultVelocity = Vector2.zero;
		public readonly float defaultMaxSpeedPerSecond = 0f;

		private Vector3 _target = Vector2.zero;

		public Vector2 Position 
		{ 
			get => transform.position;
			set => transform.position = value;
		}

		public Vector2 Velocity 
		{
			get => _rigidbody.velocity;
			private set => _rigidbody.velocity = value;
		}

		[SerializeField]
		private Rigidbody2D _rigidbody;
		

		public Rigidbody2D Rigidbody => _rigidbody; 

		public float SpeedPerSecond => Velocity.magnitude;

		public Vector2 FaceDirection => transform.up;

		private float _speedPerFixedUpdate => SpeedPerSecond * Time.fixedDeltaTime;

		public float MaxSpeedPerFixedUpdate => MaxSpeedPerSecond * Time.fixedDeltaTime;

		public float MaxSpeedPerSecond { get; set; }

		private float _distanceToTarget => _distanceToTargetVector.magnitude;
		private Vector2 _distanceToTargetVector => _target - transform.position;
		private bool _distanceToTargetWithinThreshold => _distanceToTarget < _speedPerFixedUpdate;
		private bool _positionIsTarget => _target == transform.position;
		private bool _moving => Velocity.magnitude > 0;

		

		private void Awake()
		{
			Initialize();
		}

		private void FixedUpdate()
		{
			if(_moving)
				CheckTargetReached();
		}

		public void MoveLinearlyTo(Vector2 target)
		{
			_target = target;
			updateVelocity();
			LookAtTarget();
		}

		private void Initialize()
		{
			transform.position = defaultPosition;
			_target = transform.position;
			Rigidbody.velocity = defaultVelocity;
			MaxSpeedPerSecond = defaultMaxSpeedPerSecond;
		}

		private void updateVelocity()
		{
			Velocity = _positionIsTarget ? Vector2.zero : _distanceToTargetVector.normalized * MaxSpeedPerSecond;
		}

		private void LookAtTarget()
		{
			transform.up = _distanceToTargetVector;
		}

		private void CheckTargetReached()
		{
			if (_distanceToTargetWithinThreshold && !_positionIsTarget)
				SetPositionToTarget();
		}

		private void SetPositionToTarget()
		{
			transform.position = _target;
			updateVelocity();
		}
	}
}