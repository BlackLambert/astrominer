using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CosmicObjectInRangeGetter : Injectable
    {
        private Provider<IList<CosmicObject>> _provider;

        public void Inject(Resolver resolver)
        {
            _provider = resolver.Resolve<Provider<IList<CosmicObject>>>();
        }

        public List<CosmicObject> Get(Vector2 comparePosition, float range)
        {
            float rangeSqr = range * range;
            List<CosmicObject> result = new List<CosmicObject>();

            foreach (CosmicObject cosmicObject in _provider.Value)
            {
                float distanceSqt = ((Vector2)cosmicObject.transform.position - comparePosition).sqrMagnitude;

                if (distanceSqt <= rangeSqr)
                {
                    result.Add(cosmicObject);
                }
            }

            return result;
        }
    }
}
