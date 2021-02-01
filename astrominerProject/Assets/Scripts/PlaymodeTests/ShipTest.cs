using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Astrominer;


namespace Astrominer.Test
{


    public class ShipTest
    {
        private Ship _ship;
        private float _testMaxSpeed = 2f;
        private float _testNegativeMaxSpeed = -1.0f;
        private readonly Vector2 _testPosition = new Vector2(4.0f, 2.0f);
        private readonly Vector2 _testVelocity = new Vector2(3.0f, 5.0f);
        private readonly Vector2 _testTargetDelta = new Vector2(3.0f, 2.0f);
        private float _epsilon = 0.001f;

        private Vector2 _testTarget => _testPosition + _testTargetDelta;

        [SetUp]
        public void Setup()
        {
            _ship = Ship.New(_testMaxSpeed);
        }

        [TearDown]
        public void Dispose()
        {
            if (_ship != null)
                _ship.Destroy();
        }


        [Test]
        public void ShipPositionSet()
        {
            _ship.Position = _testPosition;
            Assert.AreEqual(_testPosition, _ship.Position);
        }

        [Test]
        public void ShipObjectPositionSet()
        {
            _ship.Position = _testPosition;
            Assert.AreEqual(_testPosition, (Vector2)_ship.transform.position);
        }

        [Test]
        public void ShipObjectPositionSet_ShipPositionEqualsShipObjectPosition()
        {
            _ship.transform.position = _testPosition;
            Assert.AreEqual((Vector2)_ship.transform.position, _ship.Position);
        }

        [Test]
        public void ShipRigidBodyVelocitySet_ShipVelocityEqualsShipRigidbodyVelocity()
        {
            _ship.Rigidbody.velocity = _testVelocity;
            Assert.AreEqual((Vector2)_ship.Rigidbody.velocity, _ship.Velocity);
        }

        [Test]
        public void NewShipHasDefaultValues()
        {
            Assert.AreEqual(Vector2.zero, _ship.Position);
            Assert.AreEqual(Vector2.zero, _ship.Velocity);
            Assert.AreEqual(_testMaxSpeed, _ship.MaxSpeed);
        }



        [Test]
        public void ShipMaxSpeedSet()
        {
            _ship.MaxSpeed = _testMaxSpeed;
            Assert.AreEqual(_testMaxSpeed, _ship.MaxSpeed);
        }

        [Test]
        public void MoveTo_ShipSpeedEqualsVelocityMagnitude()
        {
            SetupMovement(_testTarget);
            Assert.AreEqual(_ship.SpeedPerSecond, _ship.Velocity.magnitude);
        }

        [Test]
        public void MoveTo_ShipSpeedPerSecondEqualsMaxSpeedPerSecond()
        {
            SetupMovement(_testTarget);
            Assert.AreEqual(_ship.MaxSpeed, _ship.SpeedPerSecond);
        }

        [Test]
        public void MoveTo_VelocityDirectionEqualsShipToTargetDirection()
        {
            SetupMovement(_testTarget);
            Vector2 targetDirection = _testTarget - _ship.Position;
            Vector2 targetDirectionNormalized = targetDirection.normalized;
            Vector2 velocityNormalized = _ship.Velocity.normalized;
            bool xIdentical = Mathf.Approximately(targetDirectionNormalized.x, velocityNormalized.x);
            bool yIdentical = Mathf.Approximately(targetDirectionNormalized.y, velocityNormalized.y);
            Assert.True(xIdentical && yIdentical);
        }

        [UnityTest]
        public IEnumerator MoveTo_MovementDistanceEqualsSpeedPerFixedUpdateAfterOneFixedUpdate()
        {
            SetupMovement(_testTarget);
            yield return new WaitForFixedUpdate();
            float distance = (_ship.Position - _testPosition).magnitude;
            // Not using Mathf.Approximately because the float gap is to large causing the Assertion to fail.
            int distanceCompareValue = (int)(distance / _epsilon);
            int maxSpeedPerFixedUpdateCompareValue = (int)(_ship.MaxSpeedPerFixedUpdate / _epsilon);
            Assert.AreEqual(distanceCompareValue, maxSpeedPerFixedUpdateCompareValue);
        }

        [UnityTest]
        public IEnumerator MoveTo_ReachesTargetAfterPredictedTime()
        {
            Time.timeScale = 20f;
            SetupMovement(_testTarget);
            // Formula used: t = s / v (t: Time in seconds | s: distance | v: velocity.magnitude)
            float predictedTime = _ship.TimeToTarget;
            float timeLeft = predictedTime;

            while (timeLeft > Time.deltaTime)
            {
                Assert.AreNotEqual(_testTarget, _ship.Position);
                timeLeft -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForFixedUpdate();
            Assert.AreEqual(_testTarget, _ship.Position);
            Time.timeScale = 1f;
        }

        [Test]
        public void MoveTo_ShipFacesTarget()
        {
            SetupMovement(_testTarget);
            Vector2 targetDirection = _testTarget - _ship.Position;
            Vector2 targetDirectionNormalized = targetDirection.normalized;
            Vector2 faceDirectionNormalized = _ship.FaceDirection.normalized;
            bool xIdentical = Mathf.Approximately(targetDirectionNormalized.x, faceDirectionNormalized.x);
            bool yIdentical = Mathf.Approximately(targetDirectionNormalized.y, faceDirectionNormalized.y);
            Assert.True(xIdentical && yIdentical);
        }


        [Test]
        public void MoveTo_TimeToTargetHasPredictedValue()
        {
            Time.timeScale = 20f;
            SetupMovement(_testTarget);
            // Formula: t = s / v (t: Time in seconds | s: distance | v: velocity.magnitude)
            float predictedTime = _testTargetDelta.magnitude / _testMaxSpeed;
            Assert.AreEqual(_ship.TimeToTarget, predictedTime);
            Time.timeScale = 1f;
        }

        [Test]
        public void RigidbodyIsNotNull()
        {
            Assert.NotNull(_ship.Rigidbody);
        }

        [Test]
        public void RigidbodyIsKinematic()
        {
            Assert.IsTrue(_ship.Rigidbody.isKinematic);
        }

        [UnityTest]
        public IEnumerator Destroy_ShipDestroyed()
        {
            GameObject shipObject = _ship.gameObject;
            _ship.Destroy();
            // ship won't be destroyed until end of frame
            yield return 0;
            // Assert.isNull does not work here, since GameObjects are not literally null after Destruction,
            // but override the == Operator so that ==null resolves to true anyway.
            // see this link: https://answers.unity.com/questions/865405/nunit-notnull-assert-strangeness.html
            Assert.True(shipObject == null);
            Assert.True(_ship == null);
        }

        [Test]
        public void Create_ShipIsNotNull()
        {
            Assert.NotNull(_ship);
        }

        [Test]
        public void MaxSpeedSet_NegativeValueThrowsException()
        {
            Assert.Throws<Ship.NegativeSpeedValueException>(() => _ship.MaxSpeed = _testNegativeMaxSpeed);
        }

        [Test]
        public void New_NegativeMaxSpeedValueThrowsException()
        {
            Dispose();
            Assert.Throws<Ship.NegativeSpeedValueException>(() => _ship = Ship.New(_testNegativeMaxSpeed));
        }

        private void SetupMovement(Vector2 target)
        {
            _ship.Position = _testPosition;
            _ship.MaxSpeed = _testMaxSpeed;
            _ship.MoveTo(target);
        }

    }

}
