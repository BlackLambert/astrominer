using UnityEngine;

namespace Astrominer
{
	public class Asteroid : MonoBehaviour
	{
		public readonly Vector2 defaultPosition = Vector2.zero;

		public Vector2 Position 
		{ 
			get => transform.position;
			set => transform.position = value;
		}

		public void Awake()
		{
			Position = defaultPosition;
		}
	}
}