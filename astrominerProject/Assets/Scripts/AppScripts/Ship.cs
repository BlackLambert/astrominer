using System;
using UnityEngine;

namespace Astrominer
{
	public class Ship : MonoBehaviour
	{
		private readonly Vector2 defaultPosition = Vector2.zero;
		private readonly Vector2 defaultVelocity = Vector2.zero;
		private static readonly string _shipPrefabPath = "Prefabs/Ship";

		[SerializeField]
		private Rigidbody2D _rigidbody;

		private Vector3 _target = Vector2.zero;
		private float _maxSpeed;

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

		/// <summary>
        /// Value must be greater than 0
        /// </summary>
		public float MaxSpeed
		{
			get => _maxSpeed;
			set
			{
				if (value <= 0)
					throw new NegativeSpeedValueException();
				_maxSpeed = value;
			}
		}

		
		

		public Rigidbody2D Rigidbody => _rigidbody; 

		public float SpeedPerSecond => Velocity.magnitude;

		public Vector2 FaceDirection => transform.up;

		private float _speedPerFixedUpdate => SpeedPerSecond * Time.fixedDeltaTime;

		public float MaxSpeedPerFixedUpdate => MaxSpeed * Time.fixedDeltaTime;


		private float _distanceToTarget => _distanceToTargetVector.magnitude;

        

        private Vector2 _distanceToTargetVector => _target - transform.position;
		private bool _distanceToTargetWithinThreshold => _distanceToTarget < _speedPerFixedUpdate;
		private bool _positionIsTarget => _target == transform.position;
		private bool _moving => Velocity.magnitude > 0;

        public float TimeToTarget { get; private set; }

        public static Ship New(float maxSpeed)
		{
			Ship prefab = Resources.Load<Ship>(_shipPrefabPath);
			Ship result = GameObject.Instantiate(prefab);
			result.Initialize(maxSpeed);
			return result;
		}	

		public void Destroy()
		{
            Destroy(gameObject);
		}

		private void FixedUpdate()
		{
			if(_moving)
				CheckTargetReached();
		}

		public void MoveTo(Vector2 target)
		{
			_target = target;
			updateTimeToTarget();
			updateVelocity();
			LookAtTarget();
		}

		private void Initialize(float maxSpeed)
		{
			transform.position = defaultPosition;
			_target = transform.position;
			Rigidbody.velocity = defaultVelocity;
			MaxSpeed = maxSpeed;
		}

		private void updateTimeToTarget()
		{
			TimeToTarget = _distanceToTarget / MaxSpeed;
		}

		private void updateVelocity()
		{
			Velocity = _positionIsTarget ? Vector2.zero : _distanceToTargetVector.normalized * MaxSpeed;
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

        public class NegativeSpeedValueException: ArgumentOutOfRangeException
        {
        }
    }
}