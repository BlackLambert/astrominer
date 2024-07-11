using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SBaier.Astrominer
{
    public class FlightMap
    {
        public ReadOnlyDictionary<FlyTarget, List<FlyTarget>> FlyTargetToPath { get; }

        private readonly Dictionary<FlyTarget, List<FlyTarget>> _flyTargetToPath;
        private FlyTargetComparer _comparer;

        public FlightMap(IList<FlyTarget> flyTargets)
        {
            _comparer = new FlyTargetComparer();
            _flyTargetToPath = flyTargets.ToDictionary(target => target, _ => new List<FlyTarget>(), _comparer);
            FlyTargetToPath = new ReadOnlyDictionary<FlyTarget, List<FlyTarget>>(_flyTargetToPath);
        }

        public int GetDistanceTo(FlyTarget target)
        {
            return _flyTargetToPath[target].Count - 1;
        }

        public void UpdateFor(FlightGraph graph, FlyTarget startPoint)
        {
            Queue<OpenEntry> openQueue = new Queue<OpenEntry>();
            HashSet<FlyTarget> close = new HashSet<FlyTarget>(_comparer);
            openQueue.Enqueue(new OpenEntry(){Node = startPoint, Path = new List<FlyTarget>()});
            close.Add(startPoint);
            while (openQueue.Count > 0)
            {
                UpdateFor(graph, openQueue, close);
            }
        }

        private void UpdateFor(FlightGraph graph, Queue<OpenEntry> open, HashSet<FlyTarget> close)
        {
            OpenEntry openEntry = open.Dequeue();
            FlyTarget currentNode = openEntry.Node;
            List<FlyTarget> path = _flyTargetToPath[currentNode];
            path.Clear();
            path.AddRange(openEntry.Path);
            path.Add(currentNode);

            IReadOnlyList<FlyTarget> neighbors = graph.GetNeighborsOf(currentNode);

            foreach (FlyTarget neighbor in neighbors)
            {
                if (!close.Contains(neighbor))
                {
                    close.Add(neighbor);
                    open.Enqueue(new OpenEntry(){Node = neighbor, Path = path});
                }
            }
        }
        
        private class OpenEntry
        {
            public FlyTarget Node;
            public List<FlyTarget> Path;
        }
    }
}