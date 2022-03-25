using UnityEngine;

namespace SBaier.Astrominer
{
	[CreateAssetMenu(fileName = "ShipSettings", menuName = "ScriptableObjects/ShipSettings")]
	public class ShipSettings : ScriptableObject
	{
		[field: SerializeField]
		public float SpeedPerSecond { get; private set; } = 2;

		[field: SerializeField]
		public float ActionRadius { get; private set; } = 5;

		[field: SerializeField]
		public int InventorySpace { get; private set; } = 3;
	}
}