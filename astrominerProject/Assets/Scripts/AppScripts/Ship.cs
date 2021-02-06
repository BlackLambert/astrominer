using System;
using UnityEngine;

namespace Astrominer
{
	public class Ship : MonoBehaviour
	{
		private static readonly string _shipPrefabPath = "Prefabs/Ship";

		[SerializeField]
		private Mover _mover;

		public Vector2 Position
		{
			get => transform.position;
			set => transform.position = value;
		}

		public Vector2 FaceDirection => transform.up;
		public event Action OnTargetReached;
        
        public static Ship New()
		{
			Ship prefab = Resources.Load<Ship>(_shipPrefabPath);
			Ship result = GameObject.Instantiate(prefab);
			return result;
		}

		protected virtual void Awake()
		{
			_mover.OnTargetReached += onTargetReached;
		}

		protected virtual void OnDestroy()
		{
			_mover.OnTargetReached -= onTargetReached;
		}

		public void Destroy()
		{
            Destroy(gameObject);
		}

		public void FlyTo(Vector2 target)
		{
			_mover.MoveTo(target);
			LookAtTarget(_mover.DistanceVectorToTarget);
		}

		private void onTargetReached()
		{
			OnTargetReached?.Invoke();
		}

		private void LookAtTarget(Vector2 targetDirection)
		{
			transform.up = targetDirection.normalized;
		}

        public class NegativeSpeedValueException: ArgumentOutOfRangeException
        {
        }
    }
}