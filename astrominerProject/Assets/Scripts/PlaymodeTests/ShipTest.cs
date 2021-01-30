using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Astrominer;

public class ShipTest
{
    private Ship _ship;
	private float _testMaxSpeed = 1f;
	private readonly Vector2 _testPosition = new Vector2(4.0f, 2.0f);
    private readonly Vector2 _testVelocity = new Vector2(3.0f, 5.0f);
    private readonly Vector2 _testTargetDelta = new Vector2(5.0f, 0f);
    private const string shipPrefabPath = "Prefabs/Ship";

    private Vector2 _testTarget => _testPosition + _testTargetDelta;

    [SetUp]
    public void Setup()
	{
        Ship prefab = Resources.Load<Ship>(shipPrefabPath);
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
        Vector2 directionVector = _testTarget - _ship.Position;
        Vector2 directionVectorNormalized = directionVector.normalized;
        Vector2 velocityNormalized = _ship.Velocity.normalized;
        bool xIdentical = Mathf.Approximately(directionVectorNormalized.x, velocityNormalized.x);
        bool yIdentical = Mathf.Approximately(directionVectorNormalized.y, velocityNormalized.y);
        Assert.That(xIdentical && yIdentical);
    }

    [UnityTest]
    public IEnumerator MoveLinearlyTo_MovementDistanceEqualsSpeedPerFixedUpdateAfterOneFixedUpdate()
	{
        SetupLinearMovement(_testTarget);
        yield return new WaitForFixedUpdate();
        float distance = (_ship.Position - _testPosition).magnitude;
        Assert.That(Mathf.Approximately(distance, _ship.MaxSpeedPerFixedUpdate));
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

        Assert.AreEqual(_testTarget, _ship.Position);
        Time.timeScale = 1f;
    }

    private void SetupLinearMovement(Vector2 target)
    {
        _ship.Position = _testPosition;
        _ship.MaxSpeedPerSecond = _testMaxSpeed;
        _ship.MoveLinearlyTo(target);
    }
}
