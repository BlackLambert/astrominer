using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SBaier.Astrominer
{
    public class FlightMap
    {
        public ReadOnlyDictionary<FlyTarget, List<FlyTarget>> FlyTargetToPath { get; }

        private readonly Dictionary<FlyTarget, List<FlyTarget>> _flyTargetToPath;
        private FlightPathFinder _flightPathFinder;

        public FlightMap(IList<FlyTarget> flyTargets, FlightPathFinder flightPathFinder)
        {
            _flightPathFinder = flightPathFinder;
            _flyTargetToPath = flyTargets.ToDictionary(target => target, _ => new List<FlyTarget>());
            FlyTargetToPath = new ReadOnlyDictionary<FlyTarget, List<FlyTarget>>(_flyTargetToPath);
        }

        public int GetDistanceTo(FlyTarget target)
        {
            return _flyTargetToPath[target].Count;
        }

        public void UpdateFor(FlightGraph graph, FlyTarget startPoint)
        {
            foreach (KeyValuePair<FlyTarget,List<FlyTarget>> pair in _flyTargetToPath)
            {
                pair.Value.Clear();
                pair.Value.AddRange(_flightPathFinder.GetPath(graph, startPoint, pair.Key));
            }
        }
    }
}