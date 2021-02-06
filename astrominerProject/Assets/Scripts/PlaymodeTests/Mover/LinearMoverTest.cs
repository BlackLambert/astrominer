using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
	public class LinearMoverTest : MoverTest
	{
		private const string _moverPrefabPath = "Mover/DummyLinearMover";
		private const string _uninitializedMoverPrefabPath = "Mover/UninitializedLinearMover";
		private const string _negativeSpeedMoverPrefabPath = "Mover/NegativeSpeedLinearMover";
		private float _epsilon = 0.001f;
		private float _testSpeed = 3f;
		private Vector2 _target = new Vector2(5.1f, 1.7f);
		private Vector3 _startPosition = new Vector3(2.5f, -3.2f, -1.2f);
		private LinearMover _mover;

		private Vector2 _initialDistanceVector => _target - (Vector2)_startPosition;

		private float _testSpeedPerFixedUpdate => _testSpeed * Time.fixedDeltaTime;

		[TearDown]
		public void Dispose()
		{
			if(_mover != null)
				GameObject.Destroy(_mover.gameObject);
		}


		[UnityTest]
		public IEnumerator MoveTo_MovedBySpeedAfterFixedUpdate()
		{
			_mover = instantiateMover() as LinearMover;

			_mover.MoveTo(_target);
			yield return new WaitForFixedUpdate();

			float predictedRemainingDistanceToTarget = _initialDistanceVector.magnitude - _testSpeedPerFixedUpdate;
			float actualRemainingDistaceToTarget = (_target - (Vector2)_mover.transform.position).magnitude;
			Assert.AreEqual(predictedRemainingDistanceToTarget, actualRemainingDistaceToTarget, _epsilon);
		}

		[Test]
		public void Awake_NegativeMaxSpeedValueThrowsException()
		{
			string expectedLog = "NegativeSpeedValueException: Specified argument was out of the range of valid values.";
			LogAssert.Expect(LogType.Exception, expectedLog);
			_mover = instantiateNegativeSpeedMover() as LinearMover;
		}

		[Test]
		public void MaxSpeedSet()
		{
			_mover = instantiateMover() as LinearMover;
			_mover.Speed = _testSpeed;
			Assert.AreEqual(_testSpeed, _mover.Speed);
		}

		[Test]
		public void MaxSpeedSet_NegativeValueThrowsException()
		{
			_mover = instantiateMover() as LinearMover;
			Assert.Throws<LinearMover.NegativeSpeedValueException>(() => _mover.Speed = -1f);
		}

		[UnityTest]
		public IEnumerator MoveTo_ReachesTargetAfterPredictedTime()
		{
			Time.timeScale = 20f;
			_mover = instantiateMover() as LinearMover;

			_mover.MoveTo(_target);
			float predictedTime = _mover.TimeToTarget;
			float timeLeft = predictedTime;
			while (timeLeft > Time.deltaTime)
			{
				Assert.AreNotEqual(_target, (Vector2) _mover.transform.position);
				timeLeft -= Time.fixedDeltaTime;
				yield return new WaitForFixedUpdate();
			}

			yield return new WaitForFixedUpdate();
			Assert.AreEqual(_target, (Vector2)_mover.transform.position);
			Time.timeScale = 1f;
		}

		[Test]
		public void MoveTo_TimeToTargetHasPredictedValue()
		{
			_mover = instantiateMover() as LinearMover;
			// Formula: t = s / v (t: Time in seconds | s: distance | v: velocity.magnitude)
			float predictedTime = _initialDistanceVector.magnitude / _testSpeed;

			_mover.MoveTo(_target);

			Assert.AreEqual(_mover.TimeToTarget, predictedTime);
		}

		protected override Mover instantiateMover()
		{
			LinearMover prefab = Resources.Load<LinearMover>(_moverPrefabPath);
			LinearMover result = GameObject.Instantiate(prefab);
			result.transform.position = _startPosition;
			result.Speed = _testSpeed;
			return result;
		}

		protected override Mover instantiateUninitializedMover()
		{
			LinearMover prefab = Resources.Load<LinearMover>(_uninitializedMoverPrefabPath);
			return GameObject.Instantiate(prefab);
		}

		private Mover instantiateNegativeSpeedMover()
		{
			LinearMover prefab = Resources.Load<LinearMover>(_negativeSpeedMoverPrefabPath);
			return GameObject.Instantiate(prefab);
		}
	}
}