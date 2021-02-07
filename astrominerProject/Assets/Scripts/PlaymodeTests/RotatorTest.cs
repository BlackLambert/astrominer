using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
    public class RotatorTest
    {
        private readonly Vector2 _testTarget = new Vector2(9.2f, 3.7f);
        private readonly float _epsilon = 0.001f;
        private readonly string _rotatorPrefabPath = "Rotator/DummyRotator";
        private Rotator _rotator;
        private readonly Vector2 _testDirection = new Vector2(4.5f, 6.6f);

        [TearDown]
        public void Dispose()
        {
            if (_rotator)
                GameObject.Destroy(_rotator.gameObject);
        }

        [Test]
        public void Face_ObjectToRotateFacesTarget()
        {
            InstantiateRotator();
            _rotator.Face(_testTarget);
            Vector2 targetDirection = _testTarget - (Vector2)_rotator.transform.position;
            Vector2 targetDirectionNormalized = targetDirection.normalized;
            Vector2 faceDirectionNormalized = _rotator.transform.up.normalized;
            Assert.AreEqual(targetDirectionNormalized.x, faceDirectionNormalized.x, _epsilon);
            Assert.AreEqual(targetDirectionNormalized.y, faceDirectionNormalized.y, _epsilon);
        }

        [Test]
        public void Facing_ValueEqualsTransformUp()
        {
            InstantiateRotator();
            _rotator.transform.up = _testDirection;
            Vector2 facing = _rotator.Facing;
            Assert.AreEqual((Vector2)_rotator.transform.up, facing);
        }

        [Test]
        public void Facing_Normalized()
        {
            InstantiateRotator();
            Assert.AreEqual(_rotator.Facing.normalized, _rotator.Facing);
            Assert.AreNotEqual(1f, _testDirection.magnitude);
            _rotator.transform.up = _testDirection;
            
            Assert.AreEqual(_rotator.Facing.normalized, _rotator.Facing);

        }

        private void InstantiateRotator()
        {
            Rotator prefab = Resources.Load<Rotator>(_rotatorPrefabPath);
            _rotator = GameObject.Instantiate(prefab);
            _rotator.transform.up = Vector2.up;
        }

    }

}

