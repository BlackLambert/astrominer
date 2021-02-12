using UnityEngine;

namespace Astrominer
{
	public abstract class FlyTarget : MonoBehaviour
	{
		public Vector2 Position
		{
			get => transform.position;
			set => transform.position = value;
		}
	}
}