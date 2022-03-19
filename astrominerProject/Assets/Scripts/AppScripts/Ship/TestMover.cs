using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class TestMover : MonoBehaviour, Injectable
	{
		[SerializeField]
		private Vector2 _minTarget = new Vector2(-5, -5);
		[SerializeField]
		private Vector2 _maxTarget = new Vector2(5, 5);

		private Mover _mover;

		public void Inject(Resolver resolver)
		{
			_mover = resolver.Resolve<Mover>();
		}

		private void Start()
		{
			SetRandomTarget();
			_mover.OnTargetReached += SetRandomTarget;
		}

		private void OnDestroy()
		{
			_mover.OnTargetReached -= SetRandomTarget;
		}

		private void SetRandomTarget()
		{
			_mover.SetMovementTarget(CalculateRandomTarget());
		}

		private Vector2 CalculateRandomTarget()
		{
			float x = UnityEngine.Random.Range(_minTarget.x, _maxTarget.x);
			float y = UnityEngine.Random.Range(_minTarget.y, _maxTarget.y);
			return new Vector2(x, y);
		}
	}
}
