using System.Collections.Generic;
using SBaier.AI;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class FlyToRandomCosmicObjectAction : Node
    {
        private readonly Ship _ship;
        private Random _random;
        private CosmicObjectInRangeGetter _inRangeGetter;
        private readonly Observable<bool> _allowsFollowupAction;

        public FlyToRandomCosmicObjectAction(
            Ship ship,
            Random random,
            CosmicObjectInRangeGetter inRangeGetter,
            Observable<bool> allowsFollowupAction)
        {
            _ship = ship;
            _random = random;
            _inRangeGetter = inRangeGetter;
            _allowsFollowupAction = allowsFollowupAction;
        }

        public override bool Execute()
        {
            List<CosmicObject> itemsInRange = _inRangeGetter.Get(_ship.Position2D, _ship.Range);
            int cosmicObjectsInRange = itemsInRange.Count;
            int randomIndex = _random.Next(cosmicObjectsInRange);
            CosmicObject next = itemsInRange[randomIndex];

            if (!next.IsValidFlightTargetFor(_ship))
            {
                return Execute();
            }
            
            _ship.FlyTo(new FlightPath(new List<FlyTarget>() { _ship.Location.Value, next }));
            _allowsFollowupAction.Value = false;
            return true;
        }
    }
}