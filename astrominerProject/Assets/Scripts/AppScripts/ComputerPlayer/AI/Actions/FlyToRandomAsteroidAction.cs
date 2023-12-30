using System.Collections.Generic;
using SBaier.DI;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class FlyToRandomAsteroidAction : AIAction, Injectable
    {
        public bool AllowsFollowAction => false;
        
        private Random _random;
        private CosmicObjectInRangeGetter _inRangeGetter;

        public void Inject(Resolver resolver)
        {
            _random = resolver.Resolve<Random>();
            _inRangeGetter = resolver.Resolve<CosmicObjectInRangeGetter>();
        }

        public float GetCurrentWeight(Ship ship)
        {
            return 0;
        }

        public void Execute(Ship ship)
        {
            List<CosmicObject> itemsInRange = _inRangeGetter.Get(ship.Position2D, ship.Range);
            int cosmicObjectsInRange = itemsInRange.Count;
            int randomIndex = _random.Next(cosmicObjectsInRange);
            CosmicObject next = itemsInRange[randomIndex];
            
            if (ReferenceEquals(ship.Location.Value, next))
            {
                Execute(ship);
            }
            else
            {
                ship.FlyTo(next);
            }
        }
    }
}
