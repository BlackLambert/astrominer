using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlightGraph
    {
        private Dictionary<FlyTarget, List<FlyTarget>> _flyTargetMap;

        public List<FlyTarget> Get(FlyTarget flyTarget)
        {
            return _flyTargetMap[flyTarget];
        }

        private FlightGraph(Dictionary<FlyTarget, List<FlyTarget>> flyTargetMap)
        {
            _flyTargetMap = flyTargetMap;
        }
        
        public static FlightGraph GenerateFor(IList<FlyTarget> targets, float range)
        {
            Dictionary<FlyTarget, List<FlyTarget>> flyTargetMap = new Dictionary<FlyTarget, List<FlyTarget>>();

            foreach (FlyTarget target in targets)
            {
                Vector2 landingPoint = target.LandingPoint;
                List<FlyTarget> targetsInRange = targets.Where(t => t.IsInRange(landingPoint, range)).ToList();
                flyTargetMap.Add(target, targetsInRange);
            }

            return new FlightGraph(flyTargetMap);
        }
    }
}
