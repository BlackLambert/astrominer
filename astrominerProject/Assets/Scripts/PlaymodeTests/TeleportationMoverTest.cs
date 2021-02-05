using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

namespace Astrominer.Test
{
    public class TeleportationMoverTest
    {
        private TeleportationMover _mover;
        private Vector2 _testTarget = new Vector2(4f, 2f);
        private Vector2 _testStartingPosition = Vector2.zero;
        private Moveable _testMoveable;
        private float _timeout = 1f;

        [SetUp]
        public void Initialize()
        {
            _mover = new TeleportationMover();
            _testMoveable = new DummyMoveable(_testStartingPosition);
        }

        [Test]
        public void MoveTo_InvocationBeforeInitThrowsException()
        {
            Assert.Throws<TeleportationMover.MoveableNullException>(() =>
            {
                _mover.MoveTo(_testTarget);
            });
        }

        [Test]
        public void MoveTo_InvocationAfterInitThrowsNoException()
        {
            Assert.DoesNotThrow(() =>
            {
                _mover.Initialize(_testMoveable);
                _mover.MoveTo(_testTarget);
            });
        }
        [Test]
        public void Initialize_ThrowsExceptionOnMoveableArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _mover.Initialize(null);
            });
        }

        [UnityTest]
        public IEnumerator MoveTo_MoverPositionEqualsTargetPositionOnTargetReached()
        {
            _mover.Initialize(_testMoveable);
            bool targetReached = false;
            
            Action onTargetReached = () => {
                Assert.AreEqual(_testTarget, _testMoveable.Position);
                targetReached = true;
            };
            _mover.OnTargetReached += onTargetReached;
            _mover.MoveTo(_testTarget);
            float timer = _timeout;
            while (!targetReached && timer > 0)
            {
                timer -= Time.deltaTime;
                yield return 0;
            }   
            _mover.OnTargetReached -= onTargetReached;
            Assert.True(targetReached);
        }
    }
}


