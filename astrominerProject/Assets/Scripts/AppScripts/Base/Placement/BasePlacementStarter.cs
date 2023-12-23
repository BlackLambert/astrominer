using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasePlacementStarter : MonoBehaviour, Injectable
    {
        private BasePlacementContext _basePlacementContext;

        public void Inject(Resolver resolver)
        {
            _basePlacementContext = resolver.Resolve<BasePlacementContext>();
        }
        
        private void Start()
        {
            _basePlacementContext.Started.Value = true;
        }
    }
}
