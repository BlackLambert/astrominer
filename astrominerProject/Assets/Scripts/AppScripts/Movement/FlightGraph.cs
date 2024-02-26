using System.Collections.Generic;
using System.Linq;

namespace SBaier.Astrominer
{
    public class FlightGraph
    {
        private Dictionary<FlyTarget, List<FlyTarget>> _flyTargetMap;

        public bool ContainsKey(FlyTarget flyTarget)
        {
            return _flyTargetMap.ContainsKey(flyTarget);
        }
        
        public List<FlyTarget> Get(FlyTarget flyTarget)
        {
            return _flyTargetMap[flyTarget];
        }

        private FlightGraph(Dictionary<FlyTarget, List<FlyTarget>> flyTargetMap)
        {
            _flyTargetMap = flyTargetMap;
        }
        
        public static FlightGraph GenerateFor(IList<FlyTarget> targets, 
            float range, Player player)
        {
            Dictionary<FlyTarget, List<FlyTarget>> flyTargetMap = new Dictionary<FlyTarget, List<FlyTarget>>();

            foreach (FlyTarget target in targets)
            {
                if (!target.IsAllowedFlightTargetFor(player))
                {
                    continue;
                }
                
                List<FlyTarget> targetsInRange = targets.Where(
                    t => IsNeighbor(t, target, range, player)).ToList();
                flyTargetMap.Add(target, targetsInRange);
            }

            return new FlightGraph(flyTargetMap);
        }

        private static bool IsNeighbor(FlyTarget destination, FlyTarget origin, float range, Player player)
        {
            return destination.IsInRange(origin.LandingPoint, range) 
                   && destination.IsAllowedFlightTargetFor(player);
        }
    }
}
