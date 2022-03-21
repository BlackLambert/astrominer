using System.Linq;

namespace SBaier.Astrominer
{
    public class ProspectorDrones : ObservableList<ProspectorDrone> 
    { 
        public bool ContainsDroneTo(Asteroid asteroid)
		{
            return _items.AsReadOnly().Any(i => i.Target == asteroid);
		}
    }
}
