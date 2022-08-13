using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Base : CosmicObject, Injectable
    {
        public Player Player { get; private set; }

		public void Inject(Resolver resolver)
		{
			Player = resolver.Resolve<Player>();
		}
	}
}
