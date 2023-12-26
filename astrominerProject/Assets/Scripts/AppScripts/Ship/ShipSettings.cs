using UnityEngine;

namespace SBaier.Astrominer
{
	[CreateAssetMenu(fileName = "ShipSettings", menuName = "ScriptableObjects/ShipSettings")]
	public class ShipSettings : ScriptableObject
	{
		[field: SerializeField]
		public float MaxSpeedPerSecond { get; private set; } = 4;
		
		[field: SerializeField]
		public float Acceleration { get; private set; } = 8;
		
		[field: SerializeField]
		public float BreakForce { get; private set; } = 6;

		[field: SerializeField]
		public float ActionRadius { get; private set; } = 5;

		[field: SerializeField]
		public int InventorySpace { get; private set; } = 3;
	}
}