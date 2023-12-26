using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Mover : MonoBehaviour, Injectable
    {
	    public event Action OnTargetReached;
	    
		[SerializeField]
		private Transform _moveable;
		
		public bool TargetReached => vector2Position == _movementTarget;

		private float acceleration => _settings.Acceleration;
		private float breakForce => _settings.BreakForce;
		private float maxSpeed => _settings.MaximalSpeed;
		private Vector2 vector2Position => _moveable.position;
		
		private Vector2 _movementTarget;
		private Arguments _settings;
		private float _currentSpeed;


		public void Inject(Resolver resolver)
		{
			_settings = resolver.Resolve<Arguments>();
		}

		private void Update()
		{
			Move();
		}

		public void SetMovementTarget(Vector2 target)
		{
			_movementTarget = target;
		}

		private void Move()
		{
			if (TargetReached)
			{
				return;
			}

			_moveable.LookAt(_movementTarget);
			bool hasSpeed = Mathf.Abs(_currentSpeed) > Mathf.Epsilon; 
			float breakDuration = hasSpeed ? _currentSpeed / breakForce : 0;
			Vector2 distanceVectorToTarget = _movementTarget - vector2Position;
			float distanceToTarget = distanceVectorToTarget.magnitude;
			float secondsTillTarget = hasSpeed ? distanceToTarget / _currentSpeed : float.MaxValue;
			float deltaTime = Time.deltaTime;

			if (secondsTillTarget > breakDuration)
			{
				Accelerate(deltaTime);
			}
			else
			{
				Break(deltaTime);
			}
			
			float movementDelta = _currentSpeed * deltaTime;
			
			if (movementDelta >= distanceToTarget)
				SetToTarget();
			else
				AddDelta(distanceVectorToTarget, deltaTime);
			
			if (TargetReached)
			{
				OnTargetReached?.Invoke();
			}
		}

		private void Accelerate(float deltaTime)
		{
			if (Math.Abs(_currentSpeed - maxSpeed) < Mathf.Epsilon)
			{
				return;
			}
			
			float delta = deltaTime * acceleration;
			AddToDeltaDelta(_currentSpeed, delta);
		}

		private void Break(float deltaTime)
		{
			if (_currentSpeed == 0)
			{
				return;
			}

			float delta = -deltaTime * breakForce;
			AddToDeltaDelta(_currentSpeed, delta);
		}

		private void AddToDeltaDelta(float speed, float deltaAddition)
		{
			float newSpeed = speed + deltaAddition;
			_currentSpeed = Mathf.Clamp(newSpeed, 0, maxSpeed);
		}

		private void AddDelta(Vector2 distanceVector, float deltaTime)
		{
			_moveable.position = vector2Position + distanceVector.normalized * (_currentSpeed * deltaTime);
		}

		private void SetToTarget()
		{
			_moveable.position = _movementTarget;
		}

		public class Arguments
		{
			public float Acceleration;
			public float BreakForce;
			public float MaximalSpeed;
		}
	}
}
