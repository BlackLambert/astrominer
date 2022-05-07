using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerListItem : MonoBehaviour, Injectable
    {
        public Player Player { get; private set; }
        [field: SerializeField]
        public RectTransform Base { get; private set; }

        public void Inject(Resolver resolver)
        {
            Player = resolver.Resolve<Player>();
        }
    }
}
