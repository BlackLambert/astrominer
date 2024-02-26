using System.Collections.Generic;
using System.Linq;

namespace SBaier.Astrominer
{
    public class FlightPathFinder
    {
        private List<KeyValuePair<FlyTarget, float>> _openList = new List<KeyValuePair<FlyTarget, float>>();
        private Dictionary<FlyTarget, FlyTarget> _cameFrom = new Dictionary<FlyTarget, FlyTarget>();
        private Dictionary<FlyTarget, float> _costsSoFar = new Dictionary<FlyTarget, float>();

        public List<FlyTarget> GetPath(FlightGraph graph, FlyTarget from, FlyTarget to)
        {
            if (!graph.ContainsKey(from) || !graph.ContainsKey(to))
            {
                return new List<FlyTarget>();
            }
            
            List<FlyTarget> result = new List<FlyTarget>();
            FlyTarget current = null;
            _openList.Add(new KeyValuePair<FlyTarget, float>(from, 0));
            _cameFrom.Add(from, null);
            _costsSoFar.Add(from, 0);

            while (_openList.Count > 0)
            {
                current = _openList.First().Key;
                _openList.RemoveAt(0);
                
                if(current == to)
                {
                    break;
                }

                List<FlyTarget> neighbors = graph.Get(current);
                foreach (FlyTarget neighbor in neighbors)
                {
                    float newCosts = _costsSoFar[current] + current.DistanceTo(neighbor.LandingPoint);

                    if (!_costsSoFar.ContainsKey(neighbor) || newCosts < _costsSoFar[neighbor])
                    {
                        _costsSoFar[neighbor] = newCosts;
                        float priority = newCosts + neighbor.DistanceTo(to.LandingPoint);
                        _openList.Add(new KeyValuePair<FlyTarget, float>(neighbor, priority));
                        _openList.Sort(Compare);
                        _cameFrom[neighbor] = current;
                    }
                }
            }

            FlyTarget previous = current;
            while (previous != null)
            {
                result.Add(previous);
                previous = _cameFrom[previous];
            }

            result.Reverse();
            Clear();
            return result;
        }

        private int Compare(KeyValuePair<FlyTarget, float> first, KeyValuePair<FlyTarget, float> second)
        {
            if (first.Value > second.Value)
            {
                return 1;
            }

            if (first.Value < second.Value)
            {
                return -1;
            }
            
            return 0;
        }

        private void Clear()
        {
            _openList.Clear();
            _cameFrom.Clear();
            _costsSoFar.Clear();
        }
    }
}
