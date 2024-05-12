using System.Collections;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasePlacementStarter : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private BasePlacementContext _basePlacementContext;

        public void Inject(Resolver resolver)
        {
            _basePlacementContext = resolver.Resolve<BasePlacementContext>();
        }

        public void Initialize()
        {
            StartCoroutine(StartPlacement());
        }

        public void Clean()
        {
            StopAllCoroutines();
        }

        private IEnumerator StartPlacement()
        {
            yield return new WaitForEndOfFrame();
            _basePlacementContext.Started.Value = true;
        }
    }
}