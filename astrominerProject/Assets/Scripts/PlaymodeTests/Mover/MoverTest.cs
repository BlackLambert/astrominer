using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
    public abstract class MoverTest
    {
        private Vector2 _testTarget = new Vector2(4f, 2f);
        private float _timeout = 5f;
        private float _epsilon = 0.001f;

		[UnityTest]
        public IEnumerator MoveTo_MoverPositionEqualsTargetPositionOnTargetReached()
        {
            Time.timeScale = 20f;
            Mover _mover = instantiateMover();
            bool targetReached = false;
            Action onTargetReached = () => {
                Assert.AreEqual(_testTarget, (Vector2)_mover.transform.position);
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

            GameObject.Destroy(_mover.gameObject);
            Time.timeScale = 1f;
        }

        [UnityTest]
        public IEnumerator MoveTo_ZValueHasNotChanged()
		{
            Time.timeScale = 20f;
            Mover _mover = instantiateMover();
            float _startZValue = _mover.transform.position.z;

            _mover.MoveTo(_testTarget);
            yield return new WaitForSeconds(1f);

            Assert.AreEqual(_startZValue, _mover.transform.position.z, _epsilon);

            GameObject.Destroy(_mover.gameObject);
            Time.timeScale = 1f;
        }

        [Test]
        public void MoveTo_DistanceToTargetEqualsPredictedValue()
		{
            Mover _mover = instantiateMover();
            _mover.MoveTo(_testTarget);
            Assert.AreEqual(_testTarget - (Vector2) _mover.transform.position, _mover.DistanceVectorToTarget);
            GameObject.Destroy(_mover.gameObject);
        }

        protected abstract Mover instantiateMover();
        protected abstract Mover instantiateUninitializedMover();
    }
}