using System;
using System.Collections.Generic;
using SBaier.DI;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class FlyToRandomAsteroidAction : AIAction, Injectable
    {
        public bool AllowsFollowAction => false;
        
        private Player _me;
        private Random _random;
        private ActiveItem<Ship> _activeShip;
        private CosmicObjectInRangeGetter _inRangeGetter;

        public void Inject(Resolver resolver)
        {
            _me = resolver.Resolve<Player>();
            _random = resolver.Resolve<Random>();
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
            _inRangeGetter = resolver.Resolve<CosmicObjectInRangeGetter>();
        }

        public int GetCurrentWeight()
        {
            return 0;
        }

        public void Execute()
        {
            Ship ship = _activeShip.Value;
            if (ship == null || ship.Player != _me)
            {
                throw new InvalidOperationException($"Player {_me.Name} does not control the active ship");
            }

            List<CosmicObject> itemsInRange = _inRangeGetter.Get(ship.Position2D, ship.Range);
            int cosmicObjectsInRange = itemsInRange.Count;
            int randomIndex = _random.Next(cosmicObjectsInRange);
            CosmicObject next = itemsInRange[randomIndex];
            
            if (ReferenceEquals(ship.Location, next))
            {
                Execute();
            }
            else
            {
                ship.FlyTo(next);
            }
        }
    }
}
