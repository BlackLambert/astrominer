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
        private readonly Vector2 _testPosition = new Vector2(4.0f, 2.0f);
        private readonly Vector2 _testVelocity = new Vector2(3.0f, 5.0f);
        private readonly Vector2 _testTargetDelta = new Vector2(3.0f, 2.0f);
        private string _shipPrefabPath => ResourcesPaths.shipPrefab;
        private float _epsilon = 0.001f;

        private Vector2 _testTarget => _testPosition + _testTargetDelta;

        [SetUp]
        public void Setup()
        {
            Ship prefab = Resources.Load<Ship>(_shipPrefabPath);
            _ship = GameObject.Instantiate(prefab);
        }

        [TearDown]
        public void Dispose()
        {
            GameObject.Destroy(_ship.gameObject);
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
            Assert.AreEqual(_ship.defaultPosition, _ship.Position);
            Assert.AreEqual(_ship.defaultVelocity, _ship.Velocity);
            Assert.AreEqual(_ship.defaultMaxSpeedPerSecond, _ship.MaxSpeedPerSecond);
        }

        [Test]
        public void NewShipSpeedEqualsDefaultVelocityMagnitude()
        {
            Assert.AreEqual(_ship.defaultVelocity.magnitude, _ship.SpeedPerSecond);
        }


        [Test]
        public void ShipMaxSpeedSet()
        {
            _ship.MaxSpeedPerSecond = _testMaxSpeed;
            Assert.AreEqual(_testMaxSpeed, _ship.MaxSpeedPerSecond);
        }

        [Test]
        public void MoveLinearlyTo_ShipSpeedEqualsVelocityMagnitude()
        {
            SetupLinearMovement(_testTarget);
            Assert.AreEqual(_ship.SpeedPerSecond, _ship.Velocity.magnitude);
        }

        [Test]
        public void MoveLinearlyTo_ShipSpeedPerSecondEqualsMaxSpeedPerSecond()
        {
            SetupLinearMovement(_testTarget);
            Assert.AreEqual(_ship.MaxSpeedPerSecond, _ship.SpeedPerSecond);
        }

        [Test]
        public void MoveLinearlyTo_VelocityDirectionEqualsShipToTargetDirection()
        {
            SetupLinearMovement(_testTarget);
            Vector2 targetDirection = _testTarget - _ship.Position;
            Vector2 targetDirectionNormalized = targetDirection.normalized;
            Vector2 velocityNormalized = _ship.Velocity.normalized;
            bool xIdentical = Mathf.Approximately(targetDirectionNormalized.x, velocityNormalized.x);
            bool yIdentical = Mathf.Approximately(targetDirectionNormalized.y, velocityNormalized.y);
            Assert.True(xIdentical && yIdentical);
        }

        [UnityTest]
        public IEnumerator MoveLinearlyTo_MovementDistanceEqualsSpeedPerFixedUpdateAfterOneFixedUpdate()
        {
            SetupLinearMovement(_testTarget);
            yield return new WaitForFixedUpdate();
            float distance = (_ship.Position - _testPosition).magnitude;
            // Not using Mathf.Approximately because the float gap is to large causing the Assertion to fail.
            int distanceCompareValue = (int)(distance / _epsilon);
            int maxSpeedPerFixedUpdateCompareValue = (int)(_ship.MaxSpeedPerFixedUpdate / _epsilon);
            Assert.AreEqual(distanceCompareValue, maxSpeedPerFixedUpdateCompareValue);
        }

        [UnityTest]
        public IEnumerator MoveLinearlyTo_ReachesTargetAfterPredictedTime()
        {
            Time.timeScale = 20f;
            SetupLinearMovement(_testTarget);
            // Formula used: t = s / v (t: Time in seconds | s: distance | v: velocity.magnitude)
            float predictedTime = _testTargetDelta.magnitude / _testMaxSpeed;
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
        public void MoveLinearlyTo_ShipFacesTarget()
        {
            SetupLinearMovement(_testTarget);
            Vector2 targetDirection = _testTarget - _ship.Position;
            Vector2 targetDirectionNormalized = targetDirection.normalized;
            Vector2 faceDirectionNormalized = _ship.FaceDirection.normalized;
            bool xIdentical = Mathf.Approximately(targetDirectionNormalized.x, faceDirectionNormalized.x);
            bool yIdentical = Mathf.Approximately(targetDirectionNormalized.y, faceDirectionNormalized.y);
            Assert.True(xIdentical && yIdentical);
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

        private void SetupLinearMovement(Vector2 target)
        {
            _ship.Position = _testPosition;
            _ship.MaxSpeedPerSecond = _testMaxSpeed;
            _ship.MoveLinearlyTo(target);
        }
    }

}
