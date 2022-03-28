using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ShipInventoryItem : MonoBehaviour, Injectable
	{
		public ExploitMachine Machine { get; private set; }

		public void Inject(Resolver resolver)
		{
			Machine = resolver.Resolve<ExploitMachine>();
		}
	}
}
