using System;
using UnityEngine;

namespace Astrominer
{
	public class Ship : MonoBehaviour
	{
		private static readonly string _shipPrefabPath = "Prefabs/Ship";

		[SerializeField]
		private Mover _mover;
		[SerializeField]
		private Rotator _rotator;

		public Vector2 Position
		{
			get => transform.position;
			set => transform.position = value;
		}

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
			_rotator.Face(target);
		}

		private void onTargetReached()
		{
			OnTargetReached?.Invoke();
		}

        public class NegativeSpeedValueException: ArgumentOutOfRangeException
        {
        }
    }
}