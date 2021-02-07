using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Astrominer;
using System;

namespace Astrominer.Test
{


    public class ShipTest
    {
        private Ship _ship;
        private readonly Vector2 _testPosition = new Vector2(4.0f, 2.0f);
        private readonly Vector2 _testTargetDelta = new Vector2(3.0f, 2.0f);
        private float _epsilon = 0.001f;
        private float _timeout = 5f;

        private Vector2 _testTarget => _testPosition + _testTargetDelta;

        [SetUp]
        public void Setup()
        {
            _ship = Ship.New();
        }

        [TearDown]
        public void Dispose()
        {
            if (_ship != null)
                _ship.Destroy();
        }

        [Test]
        public void New_ShipIsNotNull()
        {
            Assert.NotNull(_ship);
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
        public void PositionSet()
        {
            _ship.Position = _testPosition;
            Assert.AreEqual(_testPosition, _ship.Position);
        }

        [Test]
        public void ObjectPositionSet()
        {
            _ship.Position = _testPosition;
            Assert.AreEqual(_testPosition, (Vector2)_ship.transform.position);
        }

        [Test]
        public void ObjectPositionSet_PositionEqualsObjectPosition()
        {
            _ship.transform.position = _testPosition;
            Assert.AreEqual((Vector2)_ship.transform.position, _ship.Position);
        }
        [Test]
        public void FlyTo_ShipFacesTarget()
        {
            SetupMovement(_testTarget);
            Vector2 targetDirection = _testTarget - _ship.Position;
            Vector2 targetDirectionNormalized = targetDirection.normalized;
            Vector2 faceDirectionNormalized = _ship.transform.up;
            Assert.AreEqual(targetDirectionNormalized.x, faceDirectionNormalized.x, _epsilon);
            Assert.AreEqual(targetDirectionNormalized.y, faceDirectionNormalized.y, _epsilon);
        }

        [UnityTest]
        public IEnumerator FlyTo_MoverPositionEqualsTargetPositionOnTargetReached()
        {
            Time.timeScale = 20f;
            
            bool targetReached = false;
            Action onTargetReached = () => {
                Assert.AreEqual(_testTarget, (Vector2)_ship.transform.position);
                targetReached = true;
            };
            _ship.OnTargetReached += onTargetReached;

            SetupMovement(_testTarget);

            float timer = _timeout;
            while (!targetReached && timer > 0)
            {
                timer -= Time.deltaTime;
                yield return 0;
            }
            _ship.OnTargetReached -= onTargetReached;

            Assert.True(targetReached);

            Time.timeScale = 1f;
        }

        private void SetupMovement(Vector2 target)
        {
            _ship.Position = _testPosition;
            _ship.FlyTo(target);
        }

    }

}
